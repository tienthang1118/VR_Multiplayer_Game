using Project.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendController : MonoBehaviour
{
    private Friend friendModel = new Friend();

    [SerializeField]
    private FriendView view;

    public GameObject closeButton;

    public GameObject onlineStatus;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetUsername()
    {
        view.ChangeText(friendModel.username);
    }
    public string Username
    {
        get { return friendModel.username; }
        set { friendModel.username = value; }
    }
    public string Status
    {
        get { return friendModel.onlineStatus; }
        set { friendModel.onlineStatus = value; }
    }
    public void ActiveOnlineStatus(bool isOnline)
    {
        onlineStatus.SetActive(isOnline);
    }
    public void DisplayDeleteButton(bool isDisplay)
    {
        if(isDisplay)
        {
            closeButton.SetActive(true);
        }
        else
        {
            closeButton.SetActive(false);
        }
    }
    public void UnFriend()
    {
        GameObject.Find("MenuManager").GetComponent<MenuManager>().UnFriend(friendModel.username);
        gameObject.SetActive(false);
    }
    public void ShowChat()
    {
        GameObject.Find("MenuManager").GetComponent<MenuManager>().GetMessage(friendModel.username);
    }
    /*public void SendChat()
    {
        GameObject.Find("MenuManager").GetComponent<MenuManager>().SendMessageToFriend(friendModel.username);
    }*/
}
