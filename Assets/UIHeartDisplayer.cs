using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHeartDisplayer : MonoBehaviour
{

    public Animator heart1;
    public Animator heart2;
    public Animator heart3;
    public Animator heart4;
    public Animator heart5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void healthUpdate(int hp)
    {
        heart1.SetFloat("Health", hp);
        heart5.SetFloat("Health", hp);
        heart4.SetFloat("Health", hp);
        heart3.SetFloat("Health", hp);
        heart2.SetFloat("Health", hp);
    }
}
