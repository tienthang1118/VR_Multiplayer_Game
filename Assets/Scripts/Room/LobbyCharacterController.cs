using Project.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyCharacterController : MonoBehaviour
{
    private LobbyCharacterModel model;
    [SerializeField] LobbyCharacterView view;
    

    private void Awake()
    {
        model = new LobbyCharacterModel();
        
    }
    public void SetCharacter(string name, int index)
    {
        view.ChangeText(name);
        view.SetCharacterImage(index);
        model.Playername = name;
    }
    
}
