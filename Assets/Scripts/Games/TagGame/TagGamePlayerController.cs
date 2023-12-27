using Project.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Player
{
    public class TagGamePlayerController : MonoBehaviour
    {
        [SerializeField]
        private TagGamePlayerModel tagGamePlayerModel;
        public TagGameController tagGameController;
        public float distance;

        private void Awake()
        {
            tagGamePlayerModel = new TagGamePlayerModel(false);
        }
        void Update()
        {
            CheckPlayerCollision();
        }

        /*void OnCollisionEnter(Collision collision)
        {
            TagGamePlayerController otherPlayer = collision.gameObject.GetComponent<TagGamePlayerController>();
            if (!otherPlayer)
            {
                return;
            }
            if (gameObject.GetComponent<NetworkIdentity>().IsControlling() && otherPlayer.IsChaser)
            {
                otherPlayer.IsChaser = false;
                tagGameController = GameObject.Find("GameManager").GetComponent<TagGameController>();
                tagGameController.SendEliminatePlayerToServer();
            }
        }*/
        void CheckPlayerCollision()
        {
            if (!gameObject.GetComponent<NetworkIdentity>().IsControlling())
            {
                return;
            }
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, distance))
            {
                CharacterController characterController = hit.collider.GetComponent<CharacterController>();
                
                if (characterController != null && IsChaser == true)
                {
                    
                    IsChaser = false;
                    if(GetComponentInChildren<TagGameRoleController>() != null)
                    {
                        Debug.Log("OMG");
                        GetComponentInChildren<TagGameRoleController>().DestroyRole();
                        Debug.Log("OMG");
                    }
                 
                    tagGameController = GameObject.Find("GameManager").GetComponent<TagGameController>();
                    tagGameController.SendEliminatePlayerToServer(hit.collider.gameObject);
                }

                PortalController portalController = hit.collider.GetComponent<PortalController>();
                if (portalController != null)
                {
                    portalController.Teleport(gameObject);
                }
            }
        }
        public bool IsChaser
        {
            get { return tagGamePlayerModel.isChaser; }
            set { tagGamePlayerModel.isChaser = value; }
        }
    }
}

