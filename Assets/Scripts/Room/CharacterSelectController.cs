using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectController : MonoBehaviour
{
    private CharacterSelectModel model;
    [SerializeField]
    private CharacterSelectView view;
    [SerializeField]
    private GameObject[] characters;

    private void Awake()
    {
        model = new CharacterSelectModel(0);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnSelectCharacter()
    {
        for(int i = 0; i < characters.Length; i++)
        {
            if (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject == characters[i])
            {
                model.Id = i;
                view.SetSelectFramePos(characters[i].transform);
                break;
            }
        }
    }
    public int GetCharacterId()
    {
        return model.Id;
    }
}
