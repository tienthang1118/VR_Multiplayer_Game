using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> playerLeaderBoardButtons;

    [SerializeField]
    private Transform container;

    // Start is called before the first frame update
    void Start()
    {
/*        playerLeaderBoardButtons = new List<GameObject>();*/
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateLeaderBoard(List<PlayerLeaderBoardController> leaderBoard)
    {
        Debug.Log(leaderBoard.Count);
        foreach (var playerLeaderBoardButton in playerLeaderBoardButtons)
        {
            playerLeaderBoardButton.GetComponent<PlayerLeaderBoardController>().ClearText();
            Debug.Log("Update1");
        }

        for (int i = 0; i < leaderBoard.Count; i++)
        {
            Debug.Log("Update2");
            GameObject go = playerLeaderBoardButtons[i];
            go.GetComponent<PlayerLeaderBoardController>().Username = leaderBoard[i].Username;
            Debug.Log(leaderBoard[i].Username);
            go.GetComponent<PlayerLeaderBoardController>().Score = leaderBoard[i].Score;
            go.GetComponent<PlayerLeaderBoardController>().SetNameAndScore();
        }
    }
}
