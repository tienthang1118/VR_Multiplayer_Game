using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLeaderBoardController : MonoBehaviour
{
    private PlayerLeaderBoard model;
    [SerializeField]
    private PlayerLeaderBoardView view;

    private void Initialize()
    {
        model = new PlayerLeaderBoard();
    }
    public void Awake()
    {
        Initialize();
    }
    public void SetNameAndScore()
    {
        view.ChangeText(model.username, model.score);
    }
    public void ClearText()
    {
        view.ChangeText("", "");
    }
    public string Username
    {
        get { return model.username; }
        set { model.username = value; }
    }
    public string Score
    {
        get { return model.score; }
        set { model.score = value; }
    }
}
