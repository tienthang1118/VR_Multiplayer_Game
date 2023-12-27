using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI roomname;
   public void ChangeText(string text)
    {
        roomname.text = text;
    }
}
