using Project.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendRequestController : MonoBehaviour
{
    private FriendRequest friendRequestModel = new FriendRequest();

    [SerializeField]
    private FriendRequestView view;

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
        view.ChangeText(friendRequestModel.username);
    }
    public string Username
    {
        get { return friendRequestModel.username; }
        set { friendRequestModel.username =  value; }
    }
    public void AcceptFriend()
    {
        GameObject.Find("MenuManager").GetComponent<MenuManager>().AcceptFriendRequest(friendRequestModel.username);
        GameObject.Find("MenuManager").GetComponent<FriendListController>().SetFriendUpdateType("Friend");
        GameObject.Find("MenuManager").GetComponent<MenuManager>().CheckFriendRequest();
        gameObject.SetActive(false);
    }
    public void UnFriend()
    {
        GameObject.Find("MenuManager").GetComponent<MenuManager>().UnFriend(friendRequestModel.username);
        GameObject.Find("MenuManager").GetComponent<FriendListController>().SetFriendUpdateType("Friend");
        GameObject.Find("MenuManager").GetComponent<MenuManager>().CheckFriendRequest();
        gameObject.SetActive(false);
    }
}
