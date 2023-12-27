using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public Transform otherPortalPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Teleport(GameObject player)
    {
        player.GetComponent<OVRPlayerController>().enabled = false;
        player.GetComponent<Example>().enabled = false;
        player.transform.position = otherPortalPosition.position;
        StartCoroutine(AddOVRManager(player));
        
    }

    IEnumerator AddOVRManager(GameObject player)
    {
        yield return new WaitForSeconds(0.5f);
        player.GetComponent<OVRPlayerController>().enabled = true;
        player.GetComponent<Example>().enabled = true;
        if (gameObject.GetComponentInChildren<OVRManager>() == null)
        {
            GetComponentInChildren<OVRCameraRig>().gameObject.AddComponent<OVRManager>();
        }
    }
}
