using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Project.Networking;

public class GamescoreController : MonoBehaviour
{
    private GamescoreModel model;
    [SerializeField]
    private GameObject resultPanel;

    /*[SerializeField]
    private TextMeshProUGUI rankingText;*/
    [SerializeField]
    private GameObject mainCamera;
    /*[SerializeField]
    private GamescoreView view;*/
    // Start is called before the first frame update
    private void Awake()
    {
        model = new GamescoreModel();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
/*        if(resultPanel.activeSelf)
        {
            Vector3 newPosition = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z + 5);
            resultPanel.transform.position = newPosition;
            float targetRotationY = mainCamera.transform.rotation.eulerAngles.y;
            Quaternion newRotation = Quaternion.Euler(resultPanel.transform.rotation.eulerAngles.x, targetRotationY, resultPanel.transform.rotation.eulerAngles.z);
            resultPanel.transform.rotation = newRotation;
        }*/
    }
    public void ShowResultPanel()
    {
        mainCamera = GameObject.FindWithTag("MainCamera");
        resultPanel.SetActive(true);
    }
    /*public void UpdateResultPanel(string rank, string numberOfPlayer)
    {
        rankingText.text = string.Format("{0}/{1}", rank, numberOfPlayer);
    }*/
    /*public void OnQuit()
    {
        GameObject.Find("Networking").GetComponent<NetworkClient>().QuitToMainMenu();
    }*/
    /*public void ShowScoreboard(Dictionary<string, int> scoreDict)
    {
        view.ShowScoreboard(scoreDict);
    }*/
}
