using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{

    public bool used = false;
    public PlayerHealth healer;
    public Animator animatior;
    public MessagePop sheet;

    public string message = "Fight until your last";

    // Start is called before the first frame update
    void Start()
    {
        used = false;
        animatior.SetBool("Used", used);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!used)
        {
            Pickup();
        }
    }

    private void Pickup()
    {
        
            used = true;
            healer.FullHeal();
            animatior.SetBool("Used", used);
        
            sheet.RenderSheet(message);
    }


}
