using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FruitMemoryGameView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI[] movementTimer;
    [SerializeField]
    private TextMeshProUGUI[] rememberTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateMovementTimer(int time)
    {
        for (int i = 0; i < movementTimer.Length; i++)
        {
            movementTimer[i].text = time.ToString();
        }
        
    }
    public void UpdateRememberTimer(int time)
    {
        for (int i = 0; i < rememberTimer.Length; i++)
        {
            rememberTimer[i].text = time.ToString();
        }
    }

}
