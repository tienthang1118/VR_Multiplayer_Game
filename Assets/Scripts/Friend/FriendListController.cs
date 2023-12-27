using Project.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendListController : MonoBehaviour
{
    public GameObject friendRequestPrefab;
    private List<GameObject> friendRequests = new List<GameObject>();
    public Transform friendRequestContainer;

    [SerializeField]
    private FriendUpdateType friendUpdateType;

    public GameObject friendPrefab;
    private List<GameObject> friends = new List<GameObject>();
    public Transform friendsContainer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateFriendData(List<string> friendsData)
    {
        if(friendUpdateType == FriendUpdateType.FriendRequest)
        {
            UpdateFriendRequest(friendsData);
        }
        else if (friendUpdateType == FriendUpdateType.Friend)
        {
            UpdateFriend(friendsData);
        }
    }
    public void UpdateFriendRequest(List<string> friendsData)
    {
        List<string> friendRequestsRecived = new List<string>();
        foreach (var friendData in friendsData)
        {
            string[] data = friendData.Split(": ");
            string[] status = data[1].Split("- ");
            string friendstatus = status[0];

            if (friendstatus == "Friend request received")
            {
                friendRequestsRecived.Add(friendData);
            }
        }
        
        foreach (var friendRequest in friendRequests)
        {
            friendRequest.SetActive(false);
        }

        for (int i = 0; i < friendRequestsRecived.Count; i++)
        {
            string[] friendRequestRecived = friendRequestsRecived[i].Split(": ");
            string username = friendRequestRecived[0];
            GameObject go = i >= friendRequests.Count ? CreateFriendRequest() : friendRequests[i];
            go.SetActive(true);
            go.GetComponent<FriendRequestController>().Username = username;
            go.GetComponent<FriendRequestController>().SetUsername();
        }
    }
    public void UpdateFriend(List<string> friendsData)
    {
        List<string> friendsRecived = new List<string>();
        foreach (var friendData in friendsData)
        {
            string[] data = friendData.Split(": ");
            string[] status = data[1].Split("- ");
            string friendstatus = status[0];
            if (friendstatus == "Accepted")
            {
                friendsRecived.Add(friendData);
            }
        }

        foreach (var friend in friends)
        {
            friend.SetActive(false);
        }

        for (int i = 0; i < friendsRecived.Count; i++)
        {
            string[] friendRecived = friendsRecived[i].Split(": ");
            string[] status = friendRecived[1].Split("- ");
            string friendOnlineStatus = status[1];
            Debug.Log(friendOnlineStatus);
            string username = friendRecived[0];
            GameObject go = i >= friends.Count ? CreateFriend() : friends[i];
            go.SetActive(true);
            go.GetComponent<FriendController>().Username = username;
            go.GetComponent<FriendController>().SetUsername();
            go.GetComponent<FriendController>().Status = friendOnlineStatus;
            if(friendOnlineStatus == "1")
            {
                go.GetComponent<FriendController>().ActiveOnlineStatus(true);
            }
            else if(friendOnlineStatus == "0")
            {
                go.GetComponent<FriendController>().ActiveOnlineStatus(false);
            }
        }
    }
    GameObject CreateFriendRequest()
    {
        GameObject go = Instantiate(friendRequestPrefab, friendRequestContainer);
        friendRequests.Add(go);
        return go;
    }
    GameObject CreateFriend()
    {
        GameObject go = Instantiate(friendPrefab, friendsContainer);
        friends.Add(go);
        return go;
    }
    public void SetFriendUpdateType(string type)
    {
        Enum.TryParse(type, out friendUpdateType);
    }
    public void UnFriends(bool isDisplay)
    {
        foreach(var friend in friends)
        {
            friend.GetComponent<FriendController>().DisplayDeleteButton(isDisplay);
        }
    }
    
}
public enum FriendUpdateType
{
    Friend,
    FriendRequest,
    None
}
