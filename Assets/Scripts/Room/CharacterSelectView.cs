using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectView : MonoBehaviour
{
    [SerializeField]
    private GameObject selectFrame;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetSelectFramePos(Transform transform)
    {
        selectFrame.transform.SetParent(transform);
        /*selectFrame.transform.parent = transform;*/
        selectFrame.transform.SetSiblingIndex(0);
        selectFrame.transform.position = transform.position;
    }
}
