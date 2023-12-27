using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Player;
using Project.Networking;
using SocketIO;

public class TagGameController2 : MonoBehaviour
{
    /*private TagGameView view;*/
    public GameObject roleMarkerPrefab;
    private GameObject roleMarkerInstance;

    [SerializeField]
    private Transform eliminatedZonePos1;
    [SerializeField]
    private Transform eliminatedZonePos2;

    private SocketIOComponent socketReference;

    public SocketIOComponent SocketReference
    {
        get
        {
            return socketReference = (socketReference == null) ? FindObjectOfType<NetworkClient>() : socketReference;
        }
    }
    void Start()
    {
        GameObject go = roleMarkerPrefab;
        SetChaser(go);
    }

    void Update()
    {

    }
    public void SetChaser(GameObject selectedPlayer)
    {
        Debug.Log("SetChaser");
        /*selectedPlayer.GetComponent<TagGamePlayerController>().IsChaser = true;
        Debug.Log("SetChaserTrue");
        roleMarkerInstance = Instantiate(roleMarkerPrefab, selectedPlayer.transform.position, selectedPlayer.transform.rotation);
        Debug.Log("Create");
        roleMarkerInstance.transform.parent = selectedPlayer.transform;*/
        /*roleMarkerInstance.transform.position = selectedPlayer.transform.position;*/
    }
    public void SendEliminatePlayerToServer()
    {
        SocketReference.Emit("isEliminated");
    }
    public void EliminatePlayer(GameObject eliminatedPlayer)
    {
        eliminatedPlayer.GetComponent<PlayerController>().Eliminate(eliminatedZonePos1, eliminatedZonePos2);
    }
}
