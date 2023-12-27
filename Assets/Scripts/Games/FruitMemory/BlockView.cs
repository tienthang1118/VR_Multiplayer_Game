using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockView : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private List<FruitType> fruitTypesList = new List<FruitType>();
    [SerializeField]
    private List<Sprite> fruitSpritesList = new List<Sprite>();

    private Dictionary<FruitType, Sprite> fruitSprites = new Dictionary<FruitType, Sprite>();

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        for (int i = 0; i < fruitTypesList.Count; i++)
        {
            fruitSprites.Add(fruitTypesList[i], fruitSpritesList[i]);
        }
    }
    // Start is called before the first frame update

    void Start()
    {
        /*SetFruitSprite(FruitType.Apple);*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetFruitSprite(FruitType type)
    {
        spriteRenderer.sprite = fruitSprites[type];
    }
    public void RemoveFruitSprite()
    {
        spriteRenderer.sprite = null;
    }
}
