using Project.Networking;
using Project.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagGameRoleController : MonoBehaviour
{
    [SerializeField]
    private TagGameRoleModel model;

    [SerializeField]
    private TagGameRoleView view;

    [SerializeField]
    private float timeElapsed;
    private TagGameController tagGameController;
    [SerializeField]
    private bool isFlicker = false;
    /*[SerializeField]
    private bool isEliminate = false;*/

    private void Awake()
    {
        
        model = new TagGameRoleModel(30);
    }
    // Start is called before the first frame update
    void Start()
    {
        timeElapsed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if(timeElapsed >= (model.timeToChase - 10f) && !isFlicker)
        {
            Debug.Log("flicker");
            isFlicker = true;
            StartCoroutine(view.Flicker());
        }
        if (timeElapsed >= model.timeToChase)
        {
            EliminatePlayer();
            ResetTimeElapse();
        }
    }
    void EliminatePlayer()
    {
        if(gameObject.GetComponentInParent<NetworkIdentity>().IsControlling())
        {
            tagGameController = GameObject.Find("GameManager").GetComponent<TagGameController>();
            tagGameController.SendEliminatePlayerToServer(gameObject.GetComponentInParent<PlayerController>().gameObject);
            Debug.Log("Destroy");
        }
        Destroy(gameObject);


    }
    public void ResetTimeElapse()
    {
        timeElapsed = 0;
        view.StopFlicker();
        isFlicker = false;
    }
    public void DestroyRole()
    {
        Debug.Log("CCC");
        Destroy(gameObject);
    }

}
