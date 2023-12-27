using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;

namespace Project.Player
{
    public class PlayerView : MonoBehaviour
    {
        private PlayerController controller;
        /*public Transform cameraTransform;*/
        // Start is called before the first frame update
        void Start()
        {
            controller = GetComponent<PlayerController>();
/*            SetCameraTransform();*/

        }

        // Update is called once per frame
        
        void Update()
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new(moveHorizontal, 0.0f, moveVertical);

            /*var joystickAxis = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick, OVRInput.Controller.RTouch);
            Vector3 movement = new Vector3(joystickAxis.x, 0, joystickAxis.y);
            if (movement != null) {
                Debug.Log(movement);
            }
            if(cameraTransform!=null)
            {
                movement = cameraTransform.TransformDirection(movement);

            }*/
/*
            controller.MovePlayer();*/
        }
/*        public void SetCameraTransform()
        {
            cameraTransform = Camera.main.transform;
        }*/
    }
}

