using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GamescoreView : MonoBehaviour
{
    /*[SerializeField]
    private GameObject[] displayers;

    [SerializeField]
    private GameObject playerRankingPrefab;

    [SerializeField]
    private Transform playerRankingContainer;*/

    [SerializeField]
    private GameObject resultPanelPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*public void ShowScoreboard(Dictionary<string, int> scoreDict)
    {
        for(int i = 0; i < displayers.Length; i++)
        {
            foreach (var item in scoreDict)
            {
                GameObject go = Instantiate(playerRankingPrefab, playerRankingContainer);
                go.GetComponentInChildren<TextMeshProUGUI>().text = string.Format("{0}: {1}", item.Key, item.Value);
            }
        }
        
    }*/
}
