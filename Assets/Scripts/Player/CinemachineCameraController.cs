using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Networking;

public class CinemachineCameraController : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    public GameObject[] players;
    public GameObject cameraPoint;
    private void Start()
    {
        StartCoroutine(SetUpCamera());
    }
    IEnumerator SetUpCamera()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("Find player");
        players = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log("Find success");

        foreach (GameObject player in players)
        {
            if (player.GetComponent<NetworkIdentity>().IsControlling())
            {
                cameraPoint = player.transform.GetChild(0).gameObject;
            }
        }
        if (cameraPoint != null)
        {
            vcam.LookAt = cameraPoint.transform;
            vcam.Follow = cameraPoint.transform;
        }
    }
}
