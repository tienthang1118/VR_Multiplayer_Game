using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using Project.Utility;
using Project.Player;
using System;
using Project.Managers;
using System.Linq;

namespace Project.Networking
{
    public class NetworkClient : SocketIOComponent
    {
        public const float SERVER_UPDATE_TIME = 10;

        public static Action<SocketIOEvent> OnGameStateChange = (E) => { };

        public static Action OnSignInComplete = () => { };
        public static Action OnJoinRoomComplete = () => { };
        [SerializeField]
        private Transform networkContainer;
        [SerializeField]
        private Transform playerInfoContainer;

        [SerializeField]
        private GameObject playerPrefab;
        [SerializeField]
        private GameObject playerInfoPrefab;
        [SerializeField]
        private Transform roomTempContainer;
        [SerializeField]
        private Transform leaderboardTempContainer;
        [SerializeField]
        private Transform messageTempContainer;


        /*[SerializeField]
        private ServerObjects serverSpawnables;*/

        public static string ClientID
        {
            get;
            private set;
        }

        public static bool IsConnected
        {
            get;
            private set;
        }

        private static bool OldIsConnected
        {
            get;
            set;
        }

        private Dictionary<string, NetworkIdentity> serverObjects;
        //danh sach nguoi choi
        [SerializeField]
        private List<GameObject> playerInfoList;
        public List<GameObject> playerList;
        public List<string> playerIdList;
        /*public List<Player> playerInfoList;*/

        //mang chua so thu tu cac loai hoa qua
        public string[] fruits;
        public string requiredFruit;

        //thu nghiem
        public GameObject menuManager;
        public GameObject gameManager;

        //gameobject nguoi choi
        public GameObject player;
        //list phòng
        public List<string> rooms = new();


        /*public static string ClientID { get; private set; }*/
        // Start is called before the first frame update
        public override void Start()
        {
            base.Start();
            Initialize();
            SetupEvents();
        }

        // Update is called once per frame
        public override void Update()
        {
            base.Update();
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OnQuit();
            }
            /*if(Input.GetKeyDown(KeyCode.L))
            {
                SceneManagementManager.Instance.LoadLevel(SceneList.TAG_GAME, (levelName) =>
                {
                    SceneManagementManager.Instance.UnLoadLevel(SceneList.MAIN_MENU);
                });
            }*/
            /*if (Input.GetKeyDown(KeyCode.Space))
            {
                menuManager = GameObject.Find("MenuManager");
                *//*test = GameObject.Find("MenuManager");*//*
                if (menuManager != null)
                {
                    Debug.Log("CallUpdateCharacterList");
                    menuManager.GetComponent<LobbyController>().Test();
                }
                gameManager = GameObject.Find("GameManager");
                if (gameManager != null)
                {
                    gameManager.GetComponent<TagGameController>().SetChaser(gameManager);
                }
            }*/

        }
        private void Initialize()
        {
            serverObjects = new Dictionary<string, NetworkIdentity>();
            playerList = new List<GameObject>();
            playerIdList = new List<string>();
            playerInfoList = new List<GameObject>();
        }
        private void SetupEvents()
        {
            On("open", (E) =>
            {
                Debug.Log("Connection made to the server");
            });
            On("close", (E) =>
            {
                NetworkClient.IsConnected = false;
            });

            On("register", (E) =>
            {
                ClientID = E.data["id"].ToString().RemoveQuotes();
                Debug.LogFormat("Our Client's ID ({0})", ClientID);

                //Turn on our connected flags
                NetworkClient.IsConnected = true;
                NetworkClient.OldIsConnected = true;
            });
            On("checkFriendRequest", (E) =>
            {
                Debug.Log(E.data["friendNames"].ToString().RemoveQuotes());
                List<string> friendRequestStatus = FriendRequestStatus(E.data["friendNames"].ToString().RemoveQuotes());
                Debug.Log(friendRequestStatus);
                GameObject.Find("MenuManager").GetComponent<FriendListController>().UpdateFriendData(friendRequestStatus);

            });
            On("getUserProfile", (E) =>
            {
                Debug.Log(E.data.ToString().RemoveQuotes());

                GameObject go = GameObject.Find("MenuManager");
                if (go != null)
                {
                    string username = "Username: " + E.data["username"].ToString().RemoveQuotes();
                    string displayName = E.data["displayName"].ToString().RemoveQuotes();
                    string email = E.data["email"].ToString().RemoveQuotes();
                    string games = E.data["games"].ToString().RemoveQuotes();
                    games.TrimStart('[').TrimEnd(']');
                    string[] gamesProperties = games.TrimStart('{').TrimEnd('}').Split("},{");
                    string fruitScore = "";
                    string tagScore = "";
                    foreach (string gameProperties in gamesProperties)
                    {
                        string[] properties = gameProperties.Split(",");
                        foreach (string property in properties)
                        {
                            string[] keyValue = property.Split(":");
                            if (keyValue[0].Trim('\'') == "FruitMemory")
                            {
                                fruitScore = keyValue[1].Trim('\'');
                            }
                            else if (keyValue[0].Trim('\'') == "TagGame")
                            {
                                tagScore = keyValue[1].Trim('\'');
                            }
                        }
                        
                    }
                    go.GetComponent<ProfileController>().SetProfileInfo(displayName, username, "", fruitScore, tagScore);
                }

            });
            On("getMessage", (E) =>
            {
                ClearMessageTemp();
                Debug.Log(E.data.ToString().RemoveQuotes());
                string data = E.data["messages"].ToString().RemoveQuotes();
                if (data != "[]")
                {
                    List<MessageController> messageList = new List<MessageController>();
                    messageList = MessageList(data);
                    GameObject go = GameObject.Find("MenuManager");
                    if (go != null)
                    {
                        go.GetComponent<MessageListController>().UpdateMessageList(messageList);
                    }
                }
            });
            On("showLeaderboard", (E) =>
            {
                ClearLeaderboardTemp();
                Debug.Log("Show");
                Debug.Log(E.data["leaderboard"].ToString().RemoveQuotes());
                string data = E.data["leaderboard"].ToString().RemoveQuotes();
                if (data != "[]")
                {
                    List<PlayerLeaderBoardController> leaderBoard = new List<PlayerLeaderBoardController>();
                    leaderBoard = LeaderBoard(data);
                    GameObject go = GameObject.Find("MenuManager");
                    if (go != null)
                    {
                        Debug.Log(leaderBoard.Count);
                        go.GetComponent<LeaderBoardController>().UpdateLeaderBoard(leaderBoard);
                    }
                }

            });
            On("loadPlayerInfo", (E) =>
            {
                Debug.Log("Data 0" + E.data);
                string id = E.data["id"].ToString().RemoveQuotes();
                string characterIdString = E.data["characterId"].ToString().RemoveQuotes();
                string playername = E.data["playername"].ToString().RemoveQuotes();
                Debug.Log(id);
                if (!playerIdList.Contains(id))
                {
                    GameObject.Find("MenuManager").GetComponent<MenuManager>().SetActiveStartGameButton(false);
                    Debug.Log(id);
                    GameObject playerInfo = Instantiate(playerInfoPrefab, playerInfoContainer);
                    playerIdList.Add(id);
                    playerInfo.GetComponent<PlayerInfoController>().SetId(id);
                    playerInfo.GetComponent<PlayerInfoController>().SetPlayername(playername);
                    int characterId;
                    int.TryParse(characterIdString, out characterId);
                    playerInfo.GetComponent<PlayerInfoController>().SetCharacterId(characterId);
                    playerInfoList.Add(playerInfo);
                    /*playerList.Add(player);
                    playerInfos.Add(id, playerInfo.GetComponent<PlayerInfoController>());
                    player.GetComponent<PlayerInfoController>().SetId(id);
                    player.GetComponent<PlayerInfoController>().SetPlayername(playername);
                    int characterId;
                    int.TryParse(characterIdString, out characterId);
                    player.GetComponent<PlayerInfoController>().SetCharacterId(characterId);*/

                    playerInfo.name = string.Format("PlayerInfo ({0})", playerInfo.GetComponent<PlayerInfoController>().GetId());

                    /*NetworkIdentity ni = player.GetComponent<NetworkIdentity>();
                    ni.SetControllerID(id);
                    ni.SetSocketReference(this);
                    serverObjects.Add(id, ni);*/

                }
                GameObject go = GameObject.Find("MenuManager");
                /*test = GameObject.Find("MenuManager");*/
                if (go != null)
                {
                    Debug.Log("CallUpdateCharacterList");
                    go.GetComponent<LobbyController>().UpdateCharacterList(playerInfoList);
                }
                if(playerInfoList[0].GetComponent<PlayerInfoController>().GetId() == ClientID && playerInfoList.Count > 1)
                {
                    go.GetComponent<MenuManager>().SetActiveStartGameButton(true);
                }
            });
            On("spawn", (E) =>
            {
                Debug.Log("Spawn");
                string id = E.data["id"].ToString().RemoveQuotes();
                float x = E.data["position"]["x"].f;
                float y = E.data["position"]["y"].f;
                float z = E.data["position"]["z"].f;
                int characterId;
                int.TryParse(E.data["characterId"].ToString().RemoveQuotes(), out characterId);
                /*foreach(var player in playerList)
                {
                    if(player.GetComponent<PlayerController>().GetId() == id)
                    {
                        Debug.Log("Spawn");
                        player.transform.position = new Vector3(x, 1.25f, z);
                        player.GetComponent<PlayerController>().SetCharacterId(characterId);
                        player.GetComponent<PlayerController>().SetPlayerCharacter(characterId);
                        
                        *//*player.GetComponent<PlayerController>().EnableRagdoll();*//*
                        break;
                    }
                }*/
                if (!serverObjects.ContainsKey(id))
                {
                    GameObject go = Instantiate(playerPrefab, networkContainer);
                    NetworkIdentity ni = go.GetComponent<NetworkIdentity>();
                    ni.SetControllerID(id);
                    ni.SetSocketReference(this);
                    serverObjects.Add(id, ni);
                    playerList.Add(go);
                    go.name = string.Format("Player ({0})", id);
                    go.transform.position = new Vector3(x, 1.25f, z);
                    go.GetComponent<PlayerController>().SetPlayerCharacter(characterId);
                    go.GetComponent<PlayerController>().SetId(id);
                    foreach(var info in playerInfoList)
                    {
                        if(id == info.GetComponent<PlayerInfoController>().GetId())
                        {
                            go.GetComponent<PlayerController>().SetName(info.GetComponent<PlayerInfoController>().GetPlayername());
                            break;
                        }
                    }
                }

            });
            On("disconnected", (E) =>
            {
                
                string id = E.data["id"].ToString().RemoveQuotes();
                Debug.Log("Disconnected id " + id);
                GameObject go = serverObjects[id].gameObject;
                playerList.Remove(go);
                Destroy(go);
                serverObjects.Remove(id);
                playerIdList.Remove(id);
                /*foreach (var playerInfo in playerInfoList)
                {
                    if(playerInfo.GetComponent<PlayerInfoController>().GetId() == id)
                    {
                        playerInfoList.Remove(playerInfo);
                        Destroy(playerInfo);
                    }
                }*/
                for (int i = playerInfoList.Count - 1; i >= 0; i--)
                {
                    var playerInfo = playerInfoList[i];
                    if (playerInfo.GetComponent<PlayerInfoController>().GetId() == id)
                    {
                        playerInfoList.RemoveAt(i);
                        Destroy(playerInfo);
                    }
                    else
                    {
                        Emit("loadPlayerInfo");
                    }
                }
            });
            On("updatePosition", (E) =>
            {
                string id = E.data["id"].ToString().RemoveQuotes();
                float x = E.data["position"]["x"].f;
                float y = E.data["position"]["y"].f;
                float z = E.data["position"]["z"].f;

                NetworkIdentity ni = serverObjects[id];

                ni.transform.position = new Vector3(x, y, z);
            });
            On("updateRotation", (E) =>
            {
                string id = E.data["id"].ToString().RemoveQuotes();
                float rotation = E.data["rotation"].f;
                NetworkIdentity ni = serverObjects[id];
                ni.transform.localEulerAngles = new Vector3(0, rotation, 0);
            });
            On("signIn", (E) =>
            {
                OnSignInComplete.Invoke();
            });
            On("loadGame", (E) =>
            {
                Debug.Log("Switching to game");
                Destroy(GameObject.Find("Gaze Pointer"));
                Destroy(GameObject.Find("EventSystem"));
                Destroy(Camera.main.gameObject);
                /*string gameMode = GameObject.Find("MenuManager").GetComponent<MenuManager>().GetGameMode();*/
                string gameMode = PlayerPrefs.GetString("GameMode");
                //Sau sua
                if (gameMode == SceneList.FRUIT_MEMORY)
                {
                    SceneManagementManager.Instance.LoadLevel(SceneList.FRUIT_MEMORY, (levelName) =>
                    {
                        SceneManagementManager.Instance.UnLoadLevel(SceneList.MAIN_MENU);
                    });
                }
                if (gameMode == SceneList.TAG_GAME)
                {
                    SceneManagementManager.Instance.LoadLevel(SceneList.TAG_GAME, (levelName) =>
                    {
                        SceneManagementManager.Instance.UnLoadLevel(SceneList.MAIN_MENU);
                    });
                }

            });
            On("unloadGame", (E) =>
            {
                ReturnToMainMenu();
            });

            On("lobbyUpdate", (E) =>
            {
                OnGameStateChange.Invoke(E);
            });
            On("showFruits", (E) =>
            {
                
                requiredFruit = E.data["requiredFruit"].ToString().RemoveQuotes();
                string fruitsString = E.data["fruits"].ToString().RemoveQuotes();
                fruits = fruitsString.Split(',');
                Debug.Log("ShowFruit");
                Debug.Log(fruitsString);
                Debug.Log(requiredFruit);
            });
            //test
            On("getChaserPlayer", (E) =>
            {
                Debug.Log("Get Chaser Player");
                string chaserId = E.data.ToString().RemoveQuotes();
                Debug.Log("New Chaser Player ID: " + chaserId);
                StartCoroutine(SetChaser(chaserId));
                
            });
            /*On("getNewChaserPlayer", (E) =>
            {
                string chaserId = E.data.ToString().RemoveQuotes();
                Debug.Log("New Chaser Player ID: " + chaserId);
                StartCoroutine(SetChaser(chaserId));

            });*/

            On("loadLobby", (E) =>
            {
                ClearRoomTemp();
                string roomsReceive = E.data["data"].ToString().RemoveQuotes();
                Debug.Log(roomsReceive);
                if (roomsReceive != "[]")
                {
                    List<RoomController> data = new List<RoomController>();
                    Debug.Log(data.Count);
                    data = ParseJson(roomsReceive);
                    Debug.Log(data.Count);
                    /*rooms = new List<string>(roomsReceive.Split(','));*/
                    GameObject go = GameObject.Find("MenuManager");
                    if (go != null)
                    {
                        go.GetComponent<RoomListController>().UpdateRoomList(data);
                    }
                }
            });

            On("roundResult", (E) =>
            {
                Debug.Log(E.data);
                /*string round = E.data["round"].ToString().RemoveQuotes();
                Debug.Log(round);*/
                
                /*string points = E.data["points"].ToString().RemoveQuotes();
                Debug.Log(points);*/

                string rankings = E.data["rankings"].ToString().RemoveQuotes();
                Debug.Log(rankings);
                rankings = rankings.Substring(1, rankings.Length - 2);
                string[] rankingsArray = rankings.Split(',');

                Dictionary<string, int> scoreDict = new Dictionary<string, int>();
                foreach (string item in rankingsArray)
                {
                    string[] elements = item.Split(" - ");
                   
                    foreach (var player in playerList)
                    {
                        if (elements[0] == player.GetComponent<PlayerController>().GetId())
                        {
                            Debug.Log(bool.Parse(elements[1]));
                            /*scoreDict.Add(player.GetComponent<PlayerController>().GetPlayername(), int.Parse(elements[1]));*/
                            player.GetComponent<PlayerController>().SetEliminated(bool.Parse(elements[1]));
                            if (GameObject.Find("GameManager").GetComponent<TagGameController>() != null)
                            {
                                Debug.Log("OutPlayer " + elements[0]);
                                GameObject.Find("GameManager").GetComponent<TagGameController>().EliminatePlayer(player);
                                break;
                            }
                        }
                    }
                }

                if (GameObject.Find("GameManager").GetComponent<FruitMemoryGameController>() != null)
                {
                    GameObject.Find("GameManager").GetComponent<FruitMemoryGameController>().EliminatedPlayer(playerList);
                    /*go.GetComponent<FruitMemoryGameController>().SetRound(int.Parse(round));*/
                }
                
            });

            On("endGame", (E) =>
            {
                Debug.Log("End Game data");
                PlayerPrefs.SetFloat("EndGame", 1);
                StartCoroutine(QuitToLobby());
            });
            //Neu nhan duoc end game thi show bang diem
        }

        /*        public void AttemptToJoinLobby()
                {
                    Emit("joinGame");
                }*/
        private IEnumerator SetChaser(string chaserId)
        {
            yield return new WaitForSeconds(1f);

            foreach (var player in playerList)
            {
                if (player.GetComponent<PlayerController>().GetId() == chaserId)
                {
                    gameManager = GameObject.Find("GameManager");
                    if (gameManager != null)
                    {
                        gameManager.GetComponent<TagGameController>().SetChaser(player);
                    }

                    break;
                }
            }
        }
        public void QuitToMainMenu()
        {
            Emit("quitGame");
            OnQuit();
            ReturnToMainMenu();
        }
        public void QuitLobby()
        {
            Debug.Log("Quit lobby");
            Emit("quitGame");
            for (int i = playerInfoList.Count - 1; i >= 0; i--)
            {
                var playerInfo = playerInfoList[i];
                if (playerInfo != null)
                {
                    GameObject objectToDestroy = playerInfo;
                    playerInfoList.RemoveAt(i);
                    Destroy(objectToDestroy);
                }
            }
            playerIdList = new List<string>();
        }
        IEnumerator QuitToLobby()
        {
            yield return new WaitForSeconds(1f);
            if (PlayerPrefs.GetInt("isEliminated") == 0)
            {
                Emit("updateUserLeaderboard", new JSONObject(JsonUtility.ToJson(new GameData()
                {
                    gameMode = PlayerPrefs.GetString("GameMode")
                }))); ;
                GameObject.Find("vfx_firework").GetComponent<ParticleSystem>().Play();
                GameObject.Find("Singleton").GetComponent<AudioController>().PlaySE(10);
                GameObject.Find("Singleton").GetComponent<AudioController>().PlaySE(1);
            }
            yield return new WaitForSeconds(5f);
            foreach (var keyValuePair in serverObjects)
            {
                if (keyValuePair.Value != null)
                {
                    Destroy(keyValuePair.Value.gameObject);
                }
            }
            serverObjects.Clear();
            playerList = new List<GameObject>();
            ReturnToLobby();
        }

        public void OnQuit()
        {
            Emit("quitGame");
            foreach (var keyValuePair in serverObjects)
            {
                if (keyValuePair.Value != null)
                {
                    Destroy(keyValuePair.Value.gameObject);
                }
            }
            serverObjects.Clear();
            playerIdList = new List<string>();
            playerList = new List<GameObject>();
        }
        private void ReturnToLobby()
        {  
            SceneManagementManager.Instance.LoadLevel(SceneList.MAIN_MENU, (levelName) =>
            {
                if (PlayerPrefs.GetString("GameMode") == SceneList.FRUIT_MEMORY)
                {
                    SceneManagementManager.Instance.UnLoadLevel(SceneList.FRUIT_MEMORY);
                }
                else if (PlayerPrefs.GetString("GameMode") == SceneList.TAG_GAME)
                {
                    SceneManagementManager.Instance.UnLoadLevel(SceneList.TAG_GAME);
                }
                
                FindObjectOfType<MenuManager>().OnSignInComplete();
                GameObject.Find("MenuManager").GetComponent<MenuManager>().ActiveLobbyScreen();
                Emit("loadPlayerInfo");
            });

        }
        private void ReturnToMainMenu()
        {
            SceneManagementManager.Instance.LoadLevel(SceneList.MAIN_MENU, (levelName) =>
            {
                SceneManagementManager.Instance.UnLoadLevel(SceneList.FRUIT_MEMORY);
                FindObjectOfType<MenuManager>().OnSignInComplete();
            });
        }
        public List<string> FriendRequestStatus(string text)
        {
            List<string> friendRequestStatus = new List<string>();
            string[] items = text.TrimStart('[').TrimEnd(']').Split(",");
            foreach(string item in items)
            {
                friendRequestStatus.Add(item);
                Debug.Log(item);
            }
            return friendRequestStatus;
        }
        public List<RoomController> ParseJson(string json)
        {
            List<RoomController> roomControllerList = new List<RoomController>();
            string[] items = json.TrimStart('[').TrimEnd(']').Split("},{");

            foreach (string item in items)
            {
                GameObject gameObject = new GameObject();
                gameObject.transform.SetParent(roomTempContainer);
                RoomController roomController = gameObject.AddComponent<RoomController>();
                string[] properties = item.TrimStart('{').TrimEnd('}').Split(",");

                foreach (string property in properties)
                {
                    string[] keyValue = property.Split(":");
                    if (keyValue[0].Trim('\'') == "id")
                    {
                        roomController.Id = keyValue[1].Trim('\'');
                        Debug.Log(roomController.Id);
                    }
                    else if (keyValue[0].Trim('\'') == "gameMode")
                    {
                        roomController.GameMode = keyValue[1].Trim('\'');
                        Debug.Log(roomController.GameMode);
                        Debug.Log(PlayerPrefs.GetString("GameMode"));
                    }
                    else if (keyValue[0].Trim('\'') == "currentState")
                    {
                        roomController.State = keyValue[1].Trim('\'');
                        Debug.Log(roomController.State);
                    }
                }
                if(roomController.GameMode == PlayerPrefs.GetString("GameMode") && roomController.State == "Lobby")
                {
                    roomControllerList.Add(roomController);
                    Debug.Log("Add");
                }
            }
            return roomControllerList;
        }
        public List<PlayerLeaderBoardController> LeaderBoard(string json)
        {
            List<PlayerLeaderBoardController> leaderboardList = new List<PlayerLeaderBoardController>();
            json = json.TrimStart('[').TrimEnd(']');
            string[] items = json.TrimStart('{').TrimEnd('}').Split("},{");

            foreach (string item in items)
            {
                GameObject gameObject = new GameObject();
                gameObject.transform.SetParent(leaderboardTempContainer);
                PlayerLeaderBoardController playerLeaderBoard = gameObject.AddComponent<PlayerLeaderBoardController>();
                string[] properties = item.Split(",");

                foreach (string property in properties)
                {
                    string[] keyValue = property.Split(":");
                    if (keyValue[0].Trim('\'') == "username")
                    {
                        playerLeaderBoard.Username = keyValue[1].Trim('\'');
                    }
                    else if (keyValue[0].Trim('\'') == "score")
                    {
                        playerLeaderBoard.Score = keyValue[1].Trim('\'');
                    }
                }
                Debug.Log(playerLeaderBoard.Username + playerLeaderBoard.Score);
                leaderboardList.Add(playerLeaderBoard);
            }
            return leaderboardList;
        }
        public List<MessageController> MessageList(string json)
        {
            List<MessageController> messageList = new List<MessageController>();
            json = json.TrimStart('[').TrimEnd(']');
            string[] items = json.TrimStart('{').TrimEnd('}').Split("},{");

            foreach (string item in items)
            {
                GameObject gameObject = new GameObject();
                gameObject.transform.SetParent(messageTempContainer);
                MessageController message = gameObject.AddComponent<MessageController>();
                string[] properties = item.Split(",");

                foreach (string property in properties)
                {
                    string[] keyValue = property.Split(":");
                    if (keyValue[0].Trim('\'') == "message")
                    {
                        message.message = keyValue[1].Trim('\'');
                    }
                    else if (keyValue[0].Trim('\'') == "sender_name")
                    {
                        message.username = keyValue[1].Trim('\'');
                    }
                }
                Debug.Log(message.username + message.message);
                messageList.Add(message);
            }
            return messageList;
        }
        public void ClearRoomTemp()
        {
            foreach (Transform child in roomTempContainer)
            {
                Destroy(child.gameObject);
            }
        }
        public void ClearLeaderboardTemp()
        {
            foreach (Transform child in leaderboardTempContainer)
            {
                Destroy(child.gameObject);
            }
        }
        public void ClearMessageTemp()
        {
            foreach (Transform child in messageTempContainer)
            {
                Destroy(child.gameObject);
            }
        }
    }
    [Serializable]
    public class PlayerPosition
    {
        public Position position;
    }
    [Serializable]
    public class Position
    {
        public float x;
        public float y;
        public float z;
    }
    [Serializable]
    public class PlayerRotation
    {
        public float rotation;
    }

    [Serializable]
    public class Data
    {
        public string gameMode;
        public string characterId;
        public string lobbyId;
    }
    [Serializable]
    public class TagGameData
    {
        public string id;
    }
}

