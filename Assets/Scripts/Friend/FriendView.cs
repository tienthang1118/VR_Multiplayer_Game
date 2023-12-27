using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FriendView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI username;
    public void ChangeText(string text)
    {
        username.text = text;
    }
}
