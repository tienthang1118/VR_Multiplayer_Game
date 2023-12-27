using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChosenFruitController : MonoBehaviour
{
    private ChosenFruitModel model;
    private ChosenFruitView view;
    private void Awake()
    {
        view = GetComponent<ChosenFruitView>();
    }
    // Start is called before the first frame update
    void Start()
    {
        model = new ChosenFruitModel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetType(FruitType fruitType)
    {
        model.type = fruitType;
        view.SetFruitSprite(fruitType);
    }
    public void HideImage()
    {
        view.RemoveFruitSprite();
    }
}
