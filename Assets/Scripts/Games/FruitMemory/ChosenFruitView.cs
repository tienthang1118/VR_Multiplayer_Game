using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChosenFruitView : MonoBehaviour
{
    [SerializeField]
    private Image[] images;
    [SerializeField]
    private List<FruitType> fruitTypesList = new List<FruitType>();
    [SerializeField]
    private List<Sprite> fruitSpritesList = new List<Sprite>();

    private Dictionary<FruitType, Sprite> fruitSprites = new Dictionary<FruitType, Sprite>();

    private void Awake()
    {
        for (int i = 0; i < fruitTypesList.Count; i++)
        {
            fruitSprites.Add(fruitTypesList[i], fruitSpritesList[i]);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetFruitSprite(FruitType type)
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].sprite = fruitSprites[type];
        }   
    }
    public void RemoveFruitSprite()
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].sprite = null;
        }
    }
}
