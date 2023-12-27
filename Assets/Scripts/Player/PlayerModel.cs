using System;
using UnityEngine;

namespace Project.Player
{
    [Serializable]
    public class PlayerModel
    {
        public string Id;
        public string Name;
        public float Speed;

        public float Rotation;

        public bool IsEliminated;
        public bool IsInPrison;
        public PlayerModel(float speed, float rotation, bool isEliminated, bool isInPrison)
        {
            Speed = speed;
            Rotation = rotation;
            IsEliminated = isEliminated;
            IsInPrison = isInPrison;
        }
    }
    [Serializable]
    public class PlayerInfoModel
    {
        public string Id;

        public string Playername;

        public int CharacterId;

        public PlayerInfoModel()
        {
        }

        public PlayerInfoModel(string playername, int characterId)
        {
            Playername = playername;
            CharacterId = characterId;
        }
    }
}

