using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    public float moveSpeed = .50f;
    public float walkSpeed = .20f;

    public int hp = 60;

    //properties of the entity
    public CircleCollider2D cc;
    //public BoxCollider2D attackHitbox;
    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer renderer;
    public PlayerHealth targetDamage;
    public BoxCollider2D playerCollider;
    public bool ALIVE = true;
    public float direction = 2;

    public PlayerAttackBox playerWeapons;

    public Transform target;

    private Vector2 movement;

    private bool waiting = false;
    private int waitFrames = 0;
    private int waitTimer = 0;

    private int stun = 0;

    


    //Determines what behaviour to use, 0=idle 1=follow player 2=attack player
    public int behaviourState;

    //determines what stage of attack the entity is in, 0=not attacking 1=begin attack 2=damageing part 3=wind down
    public int attackState;

    public float moveX;
    public float moveY;

    // Start is called before the first frame update
    void Start()
    {
        hp = 60;
        animator.SetBool("Alive", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (ALIVE)
        {
            //get distance from entitiy to player
            float tempMoveX = (target.position.x - rb.position.x);
            float tempMoveY = (target.position.y - rb.position.y);
            float moveTotal = Mathf.Sqrt(tempMoveX * tempMoveX + tempMoveY * tempMoveY);

            float walk = 0;
            //update behaviour state based on distance from player
            if (moveTotal < .5)
            {
                behaviourState = 2;
                walk = 1;
            }
            else if (moveTotal < 1.5)
            {
                behaviourState = 1;
                walk = 1;
            }
            else if (moveTotal > 10)
            {
                walk = 0;
                behaviourState = 0;
                
            }
            animator.SetFloat("Speed", walk);


            //DETERMINE DIRECTION
            if (Mathf.Abs(tempMoveX) > Mathf.Abs(tempMoveY))
            {
                if (tempMoveX > 0)
                {
                    direction = 1;
                } else
                {
                    direction = 3;
                }
            } else
            {
                if (tempMoveY > 0)
                {
                    direction = 0;
                } else
                {
                    direction = 2;
                }
            }

            animator.SetFloat("Direction", direction);

            //makes the enitity render appropraitely to the player
            if (rb.position.y < target.position.y)
            {
                renderer.sortingOrder = 11;
            }
            else
            {
                renderer.sortingOrder = 9;
            }

            if (playerWeapons.attackState > -1 && cc.IsTouching(playerWeapons.ActiveAttack()))
            {
                TakeDamage();
            }
        }
    }

    private void FixedUpdate()
    {
        if (ALIVE)
        {
            //CALCULATE MOVEMENT OF ZOMBIE
            //move the entity if the player is within range
            if (behaviourState > 0)
            {
                //moves entity towards the player at a fixed rate
                moveX = (target.position.x - rb.position.x);
                moveY = (target.position.y - rb.position.y);
                float moveTotal = Mathf.Sqrt(moveX * moveX + moveY * moveY);
                movement = new Vector2((moveX / moveTotal) * moveSpeed, (moveY / moveTotal) * moveSpeed);

            }
            else
            {
                movement = new Vector2(0, 0);
            }
            //MOVE ZOMBIE IF NOT STUNNED
            if (stun <= 0)
            {
                rb.velocity = movement;
            }
            else
            {
                stun--;
            }

            if (behaviourState == 2 && attackState == 0)
            {
                StartCoroutine("ZombieAttack");
            }

        }
    }

    //LETS THE ZOMBIE ATTACK
    public IEnumerator ZombieAttack()
    {
        attackState = 1;
        renderer.color = Color.red;
        for (int x = 0; x <30; x++)
        {
            yield return null;
        }
        attackState = 2;

        renderer.color = Color.white;
        animator.SetFloat("Attack", 1);

        if (cc.IsTouching(playerCollider))
        {
            targetDamage.DealDamage(1);
            targetDamage.DealKnockback(new Vector2((target.position.x - rb.position.x), (target.position.y - rb.position.y)));
        }

        
        for (int x = 0; x < 10; x++)
        {
            yield return null;
        }
        animator.SetFloat("Attack", 0);

        attackState = 3;

        for (int x = 0; x < 70; x++)
        {
            yield return null;
        }
        attackState = 0;

       
    }

    public void TakeDamage()
    {
        hp--;
        stun = 10;
        renderer.color = Color.grey;
        if (hp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        ALIVE = false;
        rb.simulated = false;
        animator.SetBool("Alive", false);
    }
   

    public int RandomNumber(int min, int max)
    {
        return Random.Range(min, max);
    }

    public static IEnumerator Frames(int frameCount)
    {
      
        yield return new WaitForSeconds(frameCount);
    }
}
