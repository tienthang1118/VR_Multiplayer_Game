using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Player;
using Project.Utility.Attributes;
using Project.Utility;

namespace Project.Networking
{
    [RequireComponent(typeof(NetworkIdentity))]
    public class NetworkRotation : MonoBehaviour
    {
        [SerializeField]
        [GreyOut]
        private float oldRotation;

        /*[SerializeField]
        private PlayerManager playerManager;*/

        private NetworkIdentity networkIdentity;
        private PlayerRotation player;
        private float stillCounter = 0;
        // Start is called before the first frame update
        void Start()
        {
            networkIdentity = GetComponent<NetworkIdentity>();
            player = new PlayerRotation();
            player.rotation = 0;

            if(!networkIdentity.IsControlling())
            {
                enabled = false;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (networkIdentity.IsControlling())
            {
                if(oldRotation != transform.localEulerAngles.y)
                {
                    oldRotation = transform.localEulerAngles.y;
                    stillCounter = 0;
                    SendData();
                }
                else
                {
                    stillCounter += Time.deltaTime;
                    if(stillCounter >= 1)
                    {
                        stillCounter = 0;
                        SendData();
                    }
                }
            }
        }
        private void SendData()
        {
            player.rotation = transform.localEulerAngles.y.TwoDecimals();
            
            networkIdentity.GetSocket().Emit("updateRotation", new JSONObject(JsonUtility.ToJson(player)));
        }
    }
}

