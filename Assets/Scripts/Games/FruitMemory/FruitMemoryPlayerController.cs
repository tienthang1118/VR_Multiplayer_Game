using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Player
{
    public class FruitMemoryPlayerController : MonoBehaviour
    {
        [SerializeField]
        private float distanceToGround = 1.5f;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            /*CheckFruitBlock();*/
        }
        public FruitType CheckFruitBlock()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -Vector3.up, out hit, distanceToGround))
            {
                if (hit.collider.gameObject.GetComponent<BlockController>()!=null)
                {
                    Debug.Log(hit.collider.gameObject.GetComponent<BlockController>().GetType());
                    return hit.collider.gameObject.GetComponent<BlockController>().GetType();

                }
                return FruitType.None;
            }
            else
            {
                return FruitType.None;
            }
        }
    }
}

