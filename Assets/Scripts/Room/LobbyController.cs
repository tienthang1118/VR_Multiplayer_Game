using Project.Networking;
using Project.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> lobbyCharacters;

    [SerializeField]
    private GameObject characterPrefab;

    [SerializeField]
    private Transform characterContainer;
    // Start is called before the first frame update
    void Start()
    {
        
        lobbyCharacters = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateCharacterList(List<GameObject> playerList)
    {
        Debug.Log("UpdateCharacterList" + playerList.Count);
        /*List<string> playerIdList = GameObject.Find("Networking").GetComponent<NetworkClient>().playerIdList;*/
        foreach (var lobbyCharacter in lobbyCharacters)
        {
            lobbyCharacter.SetActive(false);
        }

        for (int i = 0; i < playerList.Count; i++)
        {
            Debug.Log("UpdateCharacter");
            GameObject go = i >= lobbyCharacters.Count ? CreateCharacter() : lobbyCharacters[i];

            go.SetActive(true);

            go.GetComponent<LobbyCharacterController>().SetCharacter(playerList[i].GetComponent<PlayerInfoController>().GetPlayername(), playerList[i].GetComponent<PlayerInfoController>().GetCharacterId());

        }

        GameObject CreateCharacter()
        {
            GameObject go = Instantiate(characterPrefab, characterContainer);
            lobbyCharacters.Add(go);
            return go;
        }
        
    }
}
