using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 1f;

    public CircleCollider2D cc;
    public Rigidbody2D rb;
    public Animator animator;

    public LayerMask warpLayer;

    public PlayerAttackBox weapon;

    private Vector2 movement;

    private bool strafe;
    public float direction;
    private int attackState;
    private bool attackHold;
    private bool weaponGot = false;

    private bool ALIVE = true;

    private void Start()
    {
        weaponGot = false;
        attackState = 0;
        ALIVE = true;
        animator.SetBool("Alive", ALIVE);
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = 0;
        movement.y = 0;
        if (ALIVE)
        {
            bool attack = false;
            if (weaponGot)
            {
                attack = Input.GetKeyDown(KeyCode.Space);
            }

            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            strafe = Input.GetKey(KeyCode.LeftShift);


            if (attackState < 1 && attack)
            {
                attackState = 1;
            }

            animator.SetFloat("Direction", direction);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }

    }

    //Called at a specific interval
    void FixedUpdate()
    {
        float moveMulti = moveSpeed;
        if (ALIVE)
        {

            //Determine move spped with sprint and diagonals
            

            if (movement.x != 0 && movement.y != 0)
            {
                moveMulti = moveMulti * .75f;
            }

            //DETERMINE DIRECTION IF NOT STRAFING
            if (!strafe)
            {
                if (movement.y != 0)
                {
                    direction = (1f - movement.y);
                }
                else if (movement.x != 0)
                {
                    direction = (2f - movement.x);
                }
            }

            if (attackState == 1)
            {

                StartCoroutine("PlayerAttack");
            }


            //GetHashCode the velocity of the player
        }

        rb.velocity = new Vector2((movement.x * moveMulti), (movement.y * moveMulti));
    }

    //throws out a damagin hitbox
    public IEnumerator PlayerAttack()
    {

        attackState = 2;
        weapon.InitiateAttack(direction);
       // weapon.UpdateHitbox(direction);


        animator.SetFloat("Attack", 1);
        animator.SetFloat("AtkDirection", direction);

        for (int x = 0; x < 20; x++)
        {
            yield return null;
        }

        do
        {
            yield return null;
        } while (Input.GetKeyDown(KeyCode.Space));

        weapon.EndAttack();

        animator.SetFloat("Attack", 0);

        for (int x = 0; x < 5; x++)
        {
            yield return null;
        }



        attackState = 0;
    }

    public void GetWeapon()
    {
        weaponGot = true;
    }

  
    public bool TakeKnockback(Vector2 power)
    {
        return true;
    }
    
    public void Die()
    {
        ALIVE = false;
        animator.SetBool("Alive", ALIVE);
    }

    public void PausePlayer(bool b)
    {
        ALIVE = b;
    }

    //collision detection
    void OnCollisionEnter2D(Collision2D collide)
    {

    }
}
