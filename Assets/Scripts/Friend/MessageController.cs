using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageController : MonoBehaviour
{
    public string username;
    public string message;

    [SerializeField]
    private TextMeshProUGUI messageDisplay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DisplayMessage()
    {
        messageDisplay.text = string.Format("{0}: {1}", username, message);
    }
}
