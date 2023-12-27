using Project.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    private RoomModel model;
    [SerializeField]
    private RoomView view;

    public RoomController()
    {

    }
    private void Initialize()
    {
        model = new RoomModel();
    }
    public void Awake()
    {
        Initialize();
    }
    public void SetName()
    {
        view.ChangeText(model.roomName);
    }
    public string Id
    {
        get{ return model.id; }
        set{ model.id = value; }
    }
    public string GameMode
    {
        get { return model.gameMode; }
        set { model.gameMode = value; }
    }
    public string RoomName
    {
        get { return model.roomName; }
        set { model.roomName = value; }
    }
    public string State
    {
        get { return model.state; }
        set { model.state = value; }
    }
    public void RemoveRoom()
    {
        Destroy(gameObject);
    }
    public void OnClick()
    {
        GameObject go = GameObject.Find("MenuManager");
        Debug.Log(model.id);
        go.GetComponent<MenuManager>().JoinLobby(model.id);
        Debug.Log("1" + model.id + "1");
    }
}
