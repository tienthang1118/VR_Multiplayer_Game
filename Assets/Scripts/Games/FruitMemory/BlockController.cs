using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    private BlockModel model;
    private BlockView view;

    public BlockModel Model
    {
        get { return model; }
        set { model = value; }
    }
    public BlockView View
    {
        get { return view; }
        set { view = value; }
    }
    private void Awake()
    {
        view = GetComponent<BlockView>();
    }
    // Start is called before the first frame update
    void Start()
    {
        model = new BlockModel();
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
    public new FruitType GetType()
    {
        return model.type;
    }
}
