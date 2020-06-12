using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpScript : MonoBehaviour
{

    public float warpX = 0f;
    public float warpY = 0f;
    private Vector2 warpLocation; 

    // Start is called before the first frame update
    void Start()
    {
        warpLocation = new Vector2(warpX, warpY);
    }

    // Update is called once per frame
  /*  void Update()
    {
        
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger == false)
        {
            collision.attachedRigidbody.position = warpLocation;
        }
    }
}
