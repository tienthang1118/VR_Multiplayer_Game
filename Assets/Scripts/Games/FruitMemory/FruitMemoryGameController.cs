using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Networking;
using System;
using Project.Player;
using SocketIO;

public class FruitMemoryGameController : MonoBehaviour
{
    [SerializeField]
    private FruitMemoryGameView view;

    private FruitMemoryModel model;

    [SerializeField]
    private GamescoreController gamescoreController;

    [SerializeField]
    private GameSound gameSound;

    [SerializeField]
    private GameObject[] blocks;

    [SerializeField]
    private GameObject[] rememberPanelDisplayer;

    [SerializeField]
    private GameObject[] movementPanelDisplayer;

    [SerializeField]
    private NetworkClient networkClient;

    [SerializeField]
    private float timeRememberFruit;

    [SerializeField]
    private float timeChooseFruit;

    private int point = 0;

    public GameObject player = null;

    [SerializeField]
    private Transform eliminatedZonePos1;
    [SerializeField]
    private Transform eliminatedZonePos2;

    private SocketIOComponent socketReference;

    public ParticleSystem confetti;
    private AudioController audioController;
    public SocketIOComponent SocketReference
    {
        get
        {
            return socketReference = (socketReference == null) ? FindObjectOfType<NetworkClient>() : socketReference;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        model = new FruitMemoryModel(1);
        /*player = GameObject.FindWithTag("Player");
        player.GetComponent<FruitMemoryPlayerController>().enabled = true;*/
        gameSound = GetComponent<GameSound>();
    }
    private void Start()
    {
        StartCoroutine(GameLoop());
        confetti.Play();
        audioController = GameObject.Find("Singleton").GetComponent<AudioController>();
        audioController.PlaySE(4);
        audioController.PlaySE(9);
        PlayerPrefs.SetFloat("EndGame", 0);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("press space");
            StartCoroutine(GameLoop());
            confetti.Play();
        }

    }
    
    void UpdateBlockFruit()
    {
        for(int i = 0; i < blocks.Length; i++)
        {
            FruitType fruitType;
            Enum.TryParse(networkClient.fruits[i], out fruitType);
            /*Enum.TryParse(testList[i], out fruitType);*/
            blocks[i].GetComponent<BlockController>().SetType(fruitType);
        }
    }
    void UpdateChosenFruit()
    {
        FruitType fruitType;
        Enum.TryParse(networkClient.requiredFruit, out fruitType);
        /*Enum.TryParse("Lemon", out fruitType);*/
        GetComponent<ChosenFruitController>().SetType(fruitType);
    }

    void HideBlockImage()
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            blocks[i].GetComponent<BlockController>().HideImage();
        }
    }
    public void SetRound(int round)
    {
        model.round = round;
    }
    void CheckResult() 
    {
        /*if(player.GetComponent<PlayerController>().IsEliminated())
        {*/
        if(player == null)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            for(int i = 0; i < players.Length; i++)
            {
                if(players[i].GetComponent<NetworkIdentity>().IsControlling())
                {
                    player = players[i];
                    break;
                }
            }
        }
        FruitType requiredFruit;
        Enum.TryParse(networkClient.requiredFruit, out requiredFruit);
        FruitType fruit = player.GetComponent<FruitMemoryPlayerController>().CheckFruitBlock();
        bool isEliminated = false;
        if (fruit == requiredFruit)
        {
            point = 100;
        }
        else
        {
            isEliminated = true;
            point = 0;
            //Gui thong bao bi loai cho server
        }
        SocketReference.Emit("roundCompleted", new JSONObject(JsonUtility.ToJson(new FruitMemoryGameData()
        {
            point = point,
            round = model.round,
            isEliminated = isEliminated
        })));
        /*}*/
        
    }
    
    public void EliminatedPlayer(List<GameObject> playerList)
    {
        foreach (var player in playerList)
        {
            player.GetComponent<PlayerController>().Eliminate(eliminatedZonePos1, eliminatedZonePos2);
        }
    }

    /*public IEnumerator ShowScoreboard(Dictionary<string, int> scoreDict)
    {
        gamescoreController.ShowScoreboard(scoreDict);
        yield return new WaitForSeconds(10f);
        //Code out game
    }*/

    IEnumerator GameLoop()
    {
        while (model.round <= 5)
        {
            SetUpRound();
            int timeRememberEslapse = (int)timeRememberFruit;
            int timeChooseEslapse = (int)timeChooseFruit;
            networkClient = GameObject.Find("Networking").GetComponent<NetworkClient>();
            
            yield return new WaitForSeconds(1f);

            //RememberPhase
            UpdateBlockFruit();
            audioController.PlaySE(5);
            audioController.PlaySE(8);
            for (int i = 0; i < rememberPanelDisplayer.Length; i++)
            {
                rememberPanelDisplayer[i].SetActive(true);
            }
            while (timeRememberEslapse >= 0)
            {
                view.UpdateRememberTimer(timeRememberEslapse);
                gameSound.PlayTimeSound();
                yield return new WaitForSeconds(1f);
                timeRememberEslapse--;
            }
            for (int i = 0; i < rememberPanelDisplayer.Length; i++)
            {
                rememberPanelDisplayer[i].SetActive(false);
            }

            HideBlockImage();
            audioController.PlaySE(7);
            audioController.PlaySE(13);
            yield return new WaitForSeconds(1f);

            //ChosePhase
            UpdateChosenFruit();
            audioController.PlaySE(3);
            audioController.PlaySE(12);
            for (int i = 0; i < rememberPanelDisplayer.Length; i++)
            {
                movementPanelDisplayer[i].SetActive(true);
            }

            while (timeChooseEslapse >= 0)
            {
                view.UpdateMovementTimer(timeChooseEslapse);
                gameSound.PlayTimeSound();
                yield return new WaitForSeconds(1f);
                timeChooseEslapse--;
            }
            for (int i = 0; i < rememberPanelDisplayer.Length; i++)
            {
                movementPanelDisplayer[i].SetActive(false);
            }

            UpdateBlockFruit();

            CheckResult();

            yield return new WaitForSeconds(3f);
            if(PlayerPrefs.GetFloat("EndGame") == 1)
            {
                break;
            }
        }
        void SetUpRound()
        {
            switch (model.round)
            {
                case 1:
                    timeRememberFruit = 20;
                    timeChooseFruit = 10;
                    break;
                case 2:
                    timeRememberFruit = 18;
                    timeChooseFruit = 9;
                    break;
                case 3:
                    timeRememberFruit = 16;
                    timeChooseFruit = 8;
                    break;
                case 4:
                    timeRememberFruit = 14;
                    timeChooseFruit = 7;
                    break;
                case 5:
                    timeRememberFruit = 12;
                    timeChooseFruit = 6;
                    break;

            }
        }

    }

    [Serializable]
    public class FruitMemoryGameData
    {
        public int point;
        public int round;
        public bool isEliminated;
    }
}
