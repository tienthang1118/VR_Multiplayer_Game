using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Networking;
using System;

namespace Project.Player
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField]
        private float speed = 5;
        [SerializeField]
        private float rotation = 60;
        [SerializeField]
        private NetworkIdentity networkIdentity;
        private float lastRotation;
        // Update is called once per frame
        void Update()
        {
            if (networkIdentity.IsControlling())
            {
                CheckMovement();
            }
            /*CheckMovement();*/
        }
        private void CheckMovement()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            transform.position += new Vector3(horizontal, 0, vertical) * speed * Time.deltaTime;
            transform.Rotate(new Vector3(0, (horizontal + vertical) * rotation * Time.deltaTime, 0));
        }

        public static implicit operator PlayerManager(Networking.PlayerPosition v)
        {
            throw new NotImplementedException();
        }
    }
}

