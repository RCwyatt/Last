using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHP = 10;
    public int currentHP;

    public PlayerMovement movePlayer;

    public int takingDamage = 0;

    public UIHeartDisplayer hpUpdate;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
        UpdateHealth();
    }

    public void FullHeal()
    {
        currentHP = maxHP;
        UpdateHealth();
        movePlayer.GetWeapon();
    }
   
    

    public bool DealDamage(int dmg)
    {
        currentHP -= dmg;
        UpdateHealth();
        if (currentHP == 0)
        {
            movePlayer.Die();
            return false;
        }
        else
        {
            return true;
        }

        

    }

    public bool DealKnockback(Vector2 power)
    {
        return movePlayer.TakeKnockback(power);
    }

    private void UpdateHealth()
    {
        hpUpdate.healthUpdate(currentHP);
    }

  

  
}
