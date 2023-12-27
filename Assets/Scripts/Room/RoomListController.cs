using Project.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomListController : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> roomButtons;

    [SerializeField]
    private GameObject roomPrefab;

    [SerializeField]
    private Transform roomContainer;

    // Start is called before the first frame update
    void Start()
    {
        roomButtons = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateRoomList(List<RoomController> rooms)
    {
        foreach (var roomButton in roomButtons)
        {
            roomButton.SetActive(false);
        }

        for (int i = 0; i < rooms.Count; i++)
        {
            GameObject go = i >= roomButtons.Count ? CreateRoomButton() : roomButtons[i];
            go.SetActive(true);
            go.GetComponent<RoomController>().Id = rooms[i].Id;
            Debug.Log(rooms[i].Id);
            go.GetComponent<RoomController>().GameMode = rooms[i].GameMode;
            go.GetComponent<RoomController>().RoomName = rooms[i].GameMode;
            go.GetComponent<RoomController>().SetName();
            Debug.Log(rooms[i].GameMode);
        }
    }
    GameObject CreateRoomButton()
    {
        GameObject go = Instantiate(roomPrefab, roomContainer);
        roomButtons.Add(go);
        return go;
    }
}
