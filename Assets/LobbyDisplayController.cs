using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyDisplayController : MonoBehaviour
{
    public GameObject fruitContent;
    public GameObject tagContent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        if(PlayerPrefs.GetString("GameMode") == "FruitMemory")
        {
            tagContent.SetActive(false);
            fruitContent.SetActive(true);
        }
        else if(PlayerPrefs.GetString("GameMode") == "TagGame")
        {
            tagContent.SetActive(true);
            fruitContent.SetActive(false);
        }
    }
}
