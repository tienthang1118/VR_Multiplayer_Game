using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ProfileController : MonoBehaviour
{
    public TextMeshProUGUI displayName;
    public TextMeshProUGUI username;
    public TextMeshProUGUI email;
    public TextMeshProUGUI fruitScore;
    public TextMeshProUGUI tagScore;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetProfileInfo(string dname, string uname, string email, string fscore, string tscore)
    {
        displayName.text = dname;
        username.text = uname;
        this.email.text = email;
        fruitScore.text = fscore;
        tagScore.text = tscore;
    }
}
