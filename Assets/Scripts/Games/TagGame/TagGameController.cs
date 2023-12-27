using Project.Networking;
using Project.Player;
using SocketIO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagGameController : MonoBehaviour
{
    public GameObject roleMarkerPrefab;
    private GameObject roleMarkerInstance;

    [SerializeField]
    private Transform eliminatedZonePos1;
    [SerializeField]
    private Transform eliminatedZonePos2;
    private AudioController audioController;
    private SocketIOComponent socketReference;

    public SocketIOComponent SocketReference
    {
        get
        {
            return socketReference = (socketReference == null) ? FindObjectOfType<NetworkClient>() : socketReference;
        }
    }
    private void Awake()
    {

    }
    void Start()
    {
        GameObject.FindWithTag("Player").GetComponent<TagGamePlayerController>().enabled = true;
        GameObject.Find("vfx_confetti").GetComponent<ParticleSystem>().Play();
        audioController = GameObject.Find("Singleton").GetComponent<AudioController>();
        audioController.PlaySE(4);
        audioController.PlaySE(9);
    }

    void Update()
    {

    }
    public void SetChaser(GameObject selectedPlayer)
    {
        selectedPlayer.GetComponent<TagGamePlayerController>().IsChaser = true;
        Debug.Log("SetChaserTrue");
        roleMarkerInstance = Instantiate(roleMarkerPrefab, selectedPlayer.transform.position, roleMarkerPrefab.transform.rotation);
        Debug.Log("Create");
        roleMarkerInstance.transform.parent = selectedPlayer.transform;
        roleMarkerInstance.transform.position = selectedPlayer.transform.position;
    }

    public void SendEliminatePlayerToServer(GameObject eliminatedPlayer)
    {
        
        SocketReference.Emit("isEliminated", new JSONObject(JsonUtility.ToJson(new TagGameData()
        {
            id = eliminatedPlayer.GetComponent<PlayerController>().GetId()
        })));
    }
    public void EliminatePlayer(GameObject eliminatedPlayer)
    {
        Debug.Log("Eliminate");
        eliminatedPlayer.GetComponent<PlayerController>().Eliminate(eliminatedZonePos1, eliminatedZonePos2);
    }
}
