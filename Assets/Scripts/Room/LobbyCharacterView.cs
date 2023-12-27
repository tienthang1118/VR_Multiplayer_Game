using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LobbyCharacterView : MonoBehaviour
{
    private Image img;

    [SerializeField]
    private Sprite[] characterImages;

    [SerializeField]
    private TextMeshProUGUI characterId;
    private void Awake()
    {
        img = GetComponentInChildren<Image>();
    }
    public void ChangeText(string text)
    {
        characterId.text = text;
    }
    public void SetCharacterImage(int index)
    {
        img.sprite = characterImages[index];
    }
}
