using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackBox : MonoBehaviour
{

    public float attackState;

    public BoxCollider2D boxBack;
    public BoxCollider2D boxFront;
    public BoxCollider2D boxRight;
    public BoxCollider2D boxLeft;

    



    // Start is called before the first frame update
    void Start()
    {
        attackState = -1;
    }

    public void InitiateAttack(float direction)
    {

        attackState = direction;

    }

    public BoxCollider2D ActiveAttack()
    {
        BoxCollider2D output = null;

        switch (attackState)
        {
            case 0:
                output = boxBack;
                break;
            case 1:
                output = boxRight;
                break;
            case 2:
                output = boxFront;
                break;
            case 3:
                output = boxLeft;
                break;

        }
        return output;
    }

    public void EndAttack()
    {
        attackState = -1;
    }

   /* private void BoxPositonforward()
    {
        weaponBox.offset.Set(0f,0f);
        weaponBox.size.Set(0.080f,0.16f);
    }

    private void BoxPositonBack()
    {
        weaponBox.offset.Set(-0.08f, 0.16f);
        weaponBox.size.Set(0.080f, 0.16f);
    }

    private void BoxPositonRight()
    {
        weaponBox.offset.Set(.04f, 0.12f);
        weaponBox.size.Set(0.16f, 0.080f);
    }

    private void BoxPositonLeft()
    {
        weaponBox.offset.Set(-.12f, .04f);
        weaponBox.size.Set(0.16f, 0.08f);
    }*/
}
