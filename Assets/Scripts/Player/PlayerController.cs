using System;
using System.Collections;
using System.Collections.Generic;
using Project.Managers;
using Project.Networking;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        protected PlayerModel model;
        /*protected PlayerInfoModel infoModel;*/

        [SerializeField]
        private GameObject[] playerPrefabs;

        [SerializeField]
        private PlayerView view;

        [SerializeField]
        private Animator animator;
        private float checkTime = 0.5f;
        private float nextCheck;
        private Vector3 lastPosition;

        [SerializeField]
        private GameObject smokePrefab;

        [SerializeField]
        private TextMeshProUGUI name;
        /*public Rigidbody rb;*/

        private OVRPlayerController ovrPlayerController;
        /*private CharacterController controller;*/

        public GameObject ovrCamera;
        public Transform rightHandTarget;
        public Transform leftHandTarget;
        public Transform rightHandAnchor;
        public Transform leftHandAnchor;
        // Start is called before the first frame update
        void Awake()
        {
            /*rb = GetComponent<Rigidbody>();*/
            model = new PlayerModel(5, 60, false, false);
            /*infoModel = new PlayerInfoModel();*/
            ovrPlayerController = gameObject.GetComponentInChildren<OVRPlayerController>();

        }
        private void Start()
        {
            lastPosition = transform.position;
            nextCheck = Time.time + checkTime;

            int countLoaded = SceneManager.sceneCount;
            Scene[] loadedScenes = new Scene[countLoaded];
            for (int i = 0; i < countLoaded; i++)
            {
                loadedScenes[i] = SceneManager.GetSceneAt(i);
                Debug.Log(loadedScenes[i].name);
                if (loadedScenes[i].name == "FruitMemory")
                {
                    GetComponent<FruitMemoryPlayerController>().enabled = true;
                    break;
                }
                else if (loadedScenes[i].name == "TagGame")
                {
                    GetComponent<TagGamePlayerController>().enabled = true;
                    break;
                }
            }
            PlayerPrefs.SetInt("isEliminated", 0);
            /*controller = GetComponent<CharacterController>();
            controller.detectCollisions = true;*/
        }
        public void Initialization()
        {
            animator = GetComponentInChildren<Animator>();
        }
        /*// Let the rigidbody take control and detect collisions.
        public void EnableRagdoll()
        {
            rb.isKinematic = false;
            rb.detectCollisions = true;
        }

        // Let animation control the rigidbody and ignore collisions.
        public void DisableRagdoll()
        {
            rb.isKinematic = true;
            rb.detectCollisions = false;
        }*/
        private void Update()
        {
            /*UpdateHandTransform();*/
            if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0.5f)
            {
                Debug.Log("Axis1D.PrimaryIndexTrigger");
            }

            if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0.5f)
            {
                Debug.Log("Axis1D.SecondaryIndexTrigger");
            }
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Vector3 position = new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z);
                Instantiate(playerPrefabs[1], position, Quaternion.identity, gameObject.transform);
            }
            if(!GetComponent<NetworkIdentity>().IsControlling() && animator != null)
            {
                UpdateAnimation();
            }
        }
        void UpdateAnimation()
        {
            if (Time.time >= nextCheck)
            {
                float distanceMoved = Vector3.Distance(transform.position, lastPosition);

                if (distanceMoved > 0.1f)
                {
                    animator.SetBool("IsRunning", true);
                }
                else
                {
                    animator.SetBool("IsRunning", false);
                }

                lastPosition = transform.position;
                nextCheck = Time.time + checkTime;
            }
        }
        public void SetName(string nameGiven)
        {
            name.text = nameGiven;
        }
        public void SetPlayerCharacter(int index)
        {
            for(int i = 0; i < playerPrefabs.Length; i++)
            {
                if(i == index)
                {
                    

                    /*if(SceneManager.GetActiveScene().name == "TagGame")
                    {
                        GetComponent<TagGamePlayerController>().enabled = true;
                    }
                    else if(SceneManager.GetActiveScene().name == "FruitMemory")
                    {
                        GetComponent<FruitMemoryPlayerController>().enabled = true;
                    }*/
                    Debug.Log(gameObject.GetComponent<NetworkIdentity>().IsControlling());
                    if (gameObject.GetComponent<NetworkIdentity>().IsControlling())
                    {

                        StartCoroutine(SetUpController());
                    }
                    else
                    {
                        Vector3 position = new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z);
                        Instantiate(playerPrefabs[i], position, Quaternion.identity, gameObject.transform);
                        Initialization();
                    }
                    break;
                }
            }
        }
        IEnumerator SetUpController()
        {
            yield return new WaitForSeconds(1f);
            Debug.Log("SetUpController");
            GameObject.Find("OVRCameraRig").transform.parent = gameObject.transform;
            GameObject.Find("OVRCameraRig").transform.position = gameObject.transform.position;
            GetComponent<OVRPlayerController>().enabled = true;
            GetComponent<Example>().enabled = true;
            StartCoroutine(AddOVRManager());
            /*rightHandAnchor = GameObject.Find("RightHandAnchor").transform;
            leftHandAnchor = GameObject.Find("LeftHandAnchor").transform;
            rightHandTarget = transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0);
            leftHandTarget = transform.GetChild(0).GetChild(2).GetChild(1).GetChild(0);*/
            Debug.Log("SetUpCOntrollerDone");
        }
        /*public void UpdateHandTransform()
        {
            if(rightHandTarget!=null)
            {
                rightHandTarget.position = rightHandAnchor.position;
                leftHandTarget.position = leftHandAnchor.position;
            }
        }*/
        public static implicit operator PlayerController(Networking.PlayerPosition v)
        {
            throw new NotImplementedException();
        }
        public void SetEliminated(bool isEliminated)
        {
            model.IsEliminated = isEliminated;
        }
        public bool IsEliminated()
        {
            return model.IsEliminated;
        }
        public void Eliminate(Transform eliminatedZonePos1, Transform eliminatedZonePos2)
        {
            Debug.Log(model.IsEliminated);
            if(model.IsEliminated && !model.IsInPrison)
            {
                model.IsInPrison = true;
                Instantiate(smokePrefab, transform.position, transform.rotation);
                if(gameObject.GetComponent<NetworkIdentity>().IsControlling())
                {
                    PlayerPrefs.SetInt("isEliminated", 1);
                    GetComponent<OVRPlayerController>().enabled = false;
                    GetComponent<Example>().enabled = false;
                    transform.position = new(UnityEngine.Random.Range(eliminatedZonePos1.position.x, eliminatedZonePos2.position.x), eliminatedZonePos1.position.y, UnityEngine.Random.Range(eliminatedZonePos1.position.z, eliminatedZonePos2.position.z));
                    
                    StartCoroutine(AddOVRManager());
                    GameObject.Find("GameManager").GetComponent<GamescoreController>().ShowResultPanel();
                }
                Instantiate(smokePrefab, transform);
                
            }    
        }
        public void SetId(string id)
        {
            model.Id = id;
        }
        public string GetId()
        {
            return model.Id;
        }
        public void SetRunAnimation(bool isRunning)
        {
            animator.SetBool("IsRunning", isRunning);
        }
        public void SendAnimData()
        {

        }
        IEnumerator AddOVRManager()
        {
            yield return new WaitForSeconds(0.5f);
            GetComponent<OVRPlayerController>().enabled = true;
            GetComponent<Example>().enabled = true;
            if (gameObject.GetComponentInChildren<OVRManager>() == null)
            {
                GetComponentInChildren<OVRCameraRig>().gameObject.AddComponent<OVRManager>();
            }
        }
    }
}
[Serializable]
public class AnimationData
{
    public bool isRunning;
}

