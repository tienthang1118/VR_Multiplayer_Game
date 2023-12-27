using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagGameRoleView : MonoBehaviour
{
    public GameObject circle;
    [SerializeField]
    private bool isFlicker;
    // Start is called before the first frame update
    void Start()
    {
        /*spriteRenderer = GetComponent<SpriteRenderer>();*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator Flicker()
    {
        isFlicker = true;
        while (isFlicker)
        {
            circle.SetActive(false);
            Debug.Log(0);
            yield return new WaitForSeconds(0.5f);
            circle.SetActive(true);
            Debug.Log(1);
            yield return new WaitForSeconds(0.5f);
        }
    }
    public void StopFlicker()
    {
        isFlicker = false;
    }
}
