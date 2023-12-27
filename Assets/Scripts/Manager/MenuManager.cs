using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using Project.Networking;
using Project.Player;
using Project.Utility;
using SocketIO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Managers {
    public class MenuManager : MonoBehaviour {

        [Header("Join Now")]
        [SerializeField]
        private GameObject joinContainer;
        
        /*[SerializeField]
        private Button queueButton;*/

        public GameObject tagGameTitle;
/*        public GameObject fruitMemoryTitle;*/

        [Header("Sign In")]
        [SerializeField]
        private GameObject signInContainer;

        [SerializeField]
        private GameObject lobbyScreen;

        [SerializeField]
        private GameObject startGameButton;

        private string username;
        private string password;
        private string newPassword;
        private string playername;
        private string email;
        private string gameMode;
        private string friendUsername;
        private string message;
        private string playerUsername;
        public GameObject muteImg;
        public GameObject unMuteImg;
        public GameObject loginMuteImg;
        public GameObject loginUnMuteImg;
        public GameObject quitChat;
        public TMP_InputField chatInputField;

        //Room
        [SerializeField]
        private RoomListController roomListController;

        private SocketIOComponent socketReference;

        public SocketIOComponent SocketReference {
            get {
                return socketReference = (socketReference == null) ? FindObjectOfType<NetworkClient>() : socketReference;
            }
        }

        public void Start() {
            /*queueButton.interactable = false;*/
            signInContainer.SetActive(false);
            joinContainer.SetActive(false);

            NetworkClient.OnSignInComplete += OnSignInComplete;

            SceneManagementManager.Instance.LoadLevel(SceneList.ONLINE, (levelName) =>
            {
                signInContainer.SetActive(true);
                joinContainer.SetActive(false);
                /*queueButton.interactable = true;*/
            });

        }
        public void Update()
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                /*StopBgm();*/
                /*GetMessage();*/
                SocketReference.Emit("getUserProfile", new JSONObject(JsonUtility.ToJson(new AccountData()
                {
                    username = "Tuan"
                })));
            }
        }
        public void OnCreateRoom()
        {
            GameObject go = GameObject.Find("MenuManager");

            SocketReference.Emit("createNewLobby", new JSONObject(JsonUtility.ToJson(new Data()
            {
                gameMode = gameMode,
                characterId = go.GetComponent<CharacterSelectController>().GetCharacterId().ToString()
            })));

        }

        public void ChangeDisplayName()
        {
            SocketReference.Emit("updateDisplayName", playername);
        }

        public void LoadLobby()
        {
            SocketReference.Emit("loadLobby");

        }
        public void JoinLobby(string id)
        {
            GameObject go = GameObject.Find("MenuManager");

            Debug.Log(id);
            Debug.Log(id.Substring(1, id.Length - 2));
            Debug.Log(go.GetComponent<CharacterSelectController>().GetCharacterId().ToString());
            SocketReference.Emit("joinLobby", new JSONObject(JsonUtility.ToJson(new Data()
            {
                lobbyId = id,
                characterId = go.GetComponent<CharacterSelectController>().GetCharacterId().ToString()
            })));
           
            //Ve nha sua doan nay
            lobbyScreen.SetActive(true);
        }
        public void StartGame()
        {
            StopBgm();
            SocketReference.Emit("startGame");
        }
        public void OnSignIn() {
            playerUsername = username;
            SocketReference.Emit("signIn", new JSONObject(JsonUtility.ToJson(new AccountData() {
                username = username,
                password = password
            })));
            Debug.Log("Login");
        }
        public void OnSelectGame(string gameType)
        {
            PlayerPrefs.SetString("GameMode", gameType);
            gameMode = gameType;
            LoadLobby();
        }
        public void OnSignInComplete() {
            signInContainer.SetActive(false);
            joinContainer.SetActive(true);
            /*queueButton.interactable = true;*/
        }

        public void OnJoinRoomComplete()
        {
            lobbyScreen.SetActive(true);
        }

        public void OnCreateAccount() {
            SocketReference.Emit("createAccount", new JSONObject(JsonUtility.ToJson(new AccountData() {
                username = username,
                password = password,
                email = email
            })));
        }

        public void EditUsername(string text) {
            username = text;
        }

        public void EditEmail(string text)
        {
            email = text;
        }

        public void EditPassword(string text) {
            password = text;
        }
        public void EditNewPassword(string text)
        {
            newPassword = text;
        }

        public void EditPlayername(string text)
        {
            playername = text;
        }
        public void EditMessage(string text)
        {
            message = text;
        }
        public string GetGameMode()
        {
            return gameMode;
        }
        public void QuitLobby()
        {
            GameObject.Find("Networking").GetComponent<NetworkClient>().QuitLobby();
        }
        public void ActiveLobbyScreen()
        {
            lobbyScreen.SetActive(true);
        }
        public void ChangePassword()
        {
            SocketReference.Emit("resetPassword", new JSONObject(JsonUtility.ToJson(new AccountData()
            {
                username = username,
                password = password,
                newPassword = newPassword
            })));
            Debug.Log("Change");
        }
        public void ForgotPassword()
        {
            Debug.Log(email);
            SocketReference.Emit("forgotPassword", new JSONObject(JsonUtility.ToJson(new AccountData()
            {
                email = email
            })));
        }

        public void SendFriendRequest()
        {
            Debug.Log(username);
            SocketReference.Emit("sendFriendRequest", new JSONObject(JsonUtility.ToJson(new AccountData()
            {
                username = username
            })));
        }
        public void CheckFriendRequest()
        {
            SocketReference.Emit("checkFriendRequest");
        }
        public void AcceptFriendRequest(string username)
        {
            SocketReference.Emit("acceptFriendRequest", new JSONObject(JsonUtility.ToJson(new AccountData()
            {
                username = username
            })));
        }
        public void UnFriend(string username)
        {
            SocketReference.Emit("unFriend", new JSONObject(JsonUtility.ToJson(new AccountData()
            {
                username = username
            })));
        }
        public void ChangeSound()
        {
            if(AudioListener.volume == 1)
            {
                AudioListener.volume = 0;
                unMuteImg.SetActive(false);
                muteImg.SetActive(true);
            }
            else if(AudioListener.volume == 0)
            {
                AudioListener.volume = 1;
                muteImg.SetActive(false);
                unMuteImg.SetActive(true);
            }
        }
        public void ChangeSoundLogin()
        {
            if (AudioListener.volume == 1)
            {
                AudioListener.volume = 0;
                loginUnMuteImg.SetActive(false);
                loginMuteImg.SetActive(true);
            }
            else if (AudioListener.volume == 0)
            {
                AudioListener.volume = 1;
                loginMuteImg.SetActive(false);
                loginUnMuteImg.SetActive(true);
            }
        }
        public void ShowLeaderBoard(string gameMode)
        {
            this.gameMode = gameMode;
            SocketReference.Emit("showLeaderboard", new JSONObject(JsonUtility.ToJson(new GameData()
            {
                gameMode = gameMode
            })));
        }
       
        public void ChangeLeaderBoard()
        {
            if(gameMode == "FruitMemory")
            {
                ShowLeaderBoard("TagGame");
                tagGameTitle.SetActive(true);
            }
            else if(gameMode == "TagGame")
            {
                ShowLeaderBoard("FruitMemory");
                tagGameTitle.SetActive(false);
            }
            SocketReference.Emit("showLeaderboard", new JSONObject(JsonUtility.ToJson(new GameData()
            {
                gameMode = gameMode
            })));
        }
        public void SendMessageToFriend()
        {
            SocketReference.Emit("sendMessage", new JSONObject(JsonUtility.ToJson(new MessageData()
            {
                username = friendUsername,
                message = message
            })));
            chatInputField.text = "";
            /*UpdateMessage();*/
        }
        public void SendQuickMessageToFriend(string quickMessage)
        {
            Debug.Log(quickMessage);
            SocketReference.Emit("sendMessage", new JSONObject(JsonUtility.ToJson(new MessageData()
            {
                username = friendUsername,
                message = quickMessage
            })));
            /*UpdateMessage();*/
        }
        public void GetMessage(string friendUsername)
        {
            this.friendUsername = friendUsername;
            SocketReference.Emit("getMessage", new JSONObject(JsonUtility.ToJson(new MessageData()
            {
                username = friendUsername
            })));
        }
        public void ShowQuitChat()
        {
            if(quitChat.activeSelf)
            {
                quitChat.SetActive(false);
            }
            else
            {
                quitChat.SetActive(true);
            }
        }
        public void SetActiveStartGameButton(bool isActive)
        {
            if(isActive)
            {
                startGameButton.SetActive(true);
            }
            else
            {
                startGameButton.SetActive(false);
            }
        }
        public void PlayTutorialVoice()
        {
            GameObject.Find("Singleton").GetComponent<AudioController>().PlaySE(11);
        }
        public void StopBgm()
        {
            GameObject.Find("Singleton").GetComponent<AudioController>().StopBGM();
        }
        public void ShowPlayerProfile()
        {
            SocketReference.Emit("getUserProfile", new JSONObject(JsonUtility.ToJson(new AccountData()
            {
                username = playerUsername
            })));
        }
        public void UpdateMessage()
        {
            GetMessage(friendUsername);
        }
        public void QuitGame()
        {
            Application.Quit();
        }
    }

    [Serializable]
    public class AccountData {
        public string username;
        public string password;
        public string email;
        public string newPassword;
    }
/*    public class ResetPasswordData
    {
        public string username;
        public string newPassword;
        public string currentPassword;
    }*/
    [Serializable]
    public class UserInfo
    {
        public string playername;
    }
    [Serializable]
    public class GameData
    {
        public string gameMode;
    }
    public class MessageData
    {
        public string username;
        public string message;
    }
}
