using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonClickSound : MonoBehaviour
{
    private Button button { get { return GetComponent<Button>(); } }
    private AudioController audioController;
    void Start()
    {
        button.onClick.AddListener(() => PlaySound());
        audioController = GameObject.Find("Singleton").GetComponent<AudioController>();
    }

    void PlaySound()
    {
        audioController.PlaySE(0);
    }
}
