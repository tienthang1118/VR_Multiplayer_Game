using Project.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatUpdate : MonoBehaviour
{
    private MenuManager menuManager;
    // Start is called before the first frame update
    void Awake()
    {
        menuManager = GameObject.Find("MenuManager").GetComponent<MenuManager>();
    }
    private void OnEnable()
    {
        StartCoroutine(UpdateMessage());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator UpdateMessage()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);
            menuManager.UpdateMessage();
        }
        
    }
}
