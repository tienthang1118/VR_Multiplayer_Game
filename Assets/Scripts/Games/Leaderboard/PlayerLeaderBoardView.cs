using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerLeaderBoardView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI username;
    [SerializeField]
    private TextMeshProUGUI score;
    public void ChangeText(string username, string score)
    {
        this.username.text = username;
        this.score.text = score;
    }
}
