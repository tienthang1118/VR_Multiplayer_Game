using Project.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoController : MonoBehaviour
{
    [SerializeField]
    protected PlayerInfoModel infoModel;

    private void Awake()
    {
        infoModel = new PlayerInfoModel();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetId(string id)
    {
        infoModel.Id = id;
    }
    public void SetPlayername(string playername)
    {
        infoModel.Playername = playername;
    }
    public void SetCharacterId(int characterId)
    {
        infoModel.CharacterId = characterId;
    }
    public string GetId()
    {
        return infoModel.Id;
    }
    public string GetPlayername()
    {
        return infoModel.Playername;
    }
    public int GetCharacterId()
    {
        return infoModel.CharacterId;
    }
}
