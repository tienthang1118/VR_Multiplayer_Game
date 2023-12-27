using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class GameSound : MonoBehaviour
{
    void Start()
    {

    }

    public void PlayTimeSound()
    {
        GameObject.Find("Singleton").GetComponent<AudioController>().PlaySE(6);
    }
}
