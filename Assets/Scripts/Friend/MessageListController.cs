using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageListController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> dialog;

    [SerializeField]
    private GameObject messagePrefab;

    [SerializeField]
    private Transform messageContainer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateMessageList(List<MessageController> messageList)
    {
        foreach (var dialogmessage in dialog)
        {
            dialogmessage.SetActive(false);
        }

        for (int i = 0; i < messageList.Count; i++)
        {
            GameObject go = i >= dialog.Count ? CreateMessage() : dialog[i];
            go.SetActive(true);
            go.GetComponent<MessageController>().username = messageList[i].username;
            Debug.Log(messageList[i].username);
            go.GetComponent<MessageController>().message = messageList[i].message;
            go.GetComponent<MessageController>().DisplayMessage();
        }
    }
    GameObject CreateMessage()
    {
        GameObject go = Instantiate(messagePrefab, messageContainer);
        dialog.Add(go);
        return go;
    }
}
