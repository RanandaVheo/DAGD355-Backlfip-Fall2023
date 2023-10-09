using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player_Hank : MonoBehaviour
{
    public float speed = 1.0f;
    public float maxSpeed = 5f;

    [SerializeField] private GameObject projectilePrefab;
    private Rigidbody2D rb;
    private PlayerInventory_Hank inventory;
    private Animator animator;
    private Timer_Hank attackingAnimationTimer = new(.5f);
    private Timer_Hank damagedAnimationTimer = new(.5f);
    private bool qDownLastFrame = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inventory = GetComponent<PlayerInventory_Hank>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(new Vector2(Input.GetAxis("Horizontal") * speed, 0f));
        
        //if (Input.GetAxis("Horizontal") == 0f)
        //{
        //    timeToStopTimer.Update();
        //    rb.velocity = new Vector2( Mathf.Lerp(rb.velocity.x, 0f, timeToStopTimer.timeLeft / timeToStopTimer.duration), rb.velocity.y);
        //}

        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (!qDownLastFrame)
                {
                    DropFromInventory(inventory.TryGetItem());
                }
                qDownLastFrame = true;
            } else
            {
                qDownLastFrame = false;
            }


            if (Input.GetMouseButtonDown(0))
            {
                attackingAnimationTimer.Reset();
                animator.SetBool("IsAttacking", true);
                GameObject tempProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity, transform);
                tempProjectile.GetComponent<Rigidbody2D>().AddForce( Vector2.right * 100 );
                tempProjectile.GetComponent<Rigidbody2D>().AddTorque(-1000);
            } 

            if (Input.GetMouseButton(1))
            {
                damagedAnimationTimer.Reset();
                animator.SetBool("IsDamaged", true);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(Vector2.up * 100);
            }
            {
                
            }
        }

        attackingAnimationTimer.Update();
        damagedAnimationTimer.Update();
        if (attackingAnimationTimer.isDone)
        {
            animator.SetBool("IsAttacking", false);
        }
        if (damagedAnimationTimer.isDone)
        {
            animator.SetBool("IsDamaged", false);
        }

        animator.SetBool("IsGrounded", rb.velocity.y == 0f);
        animator.SetBool("IsMoving", rb.velocity.x > 1f || rb.velocity.x < -1f);
    }

    public void DropFromInventory(GameObject item)
    {
        if (item == null)
        {
            Debug.Log("No item to drop.");
            return;
        }

        item.transform.position = transform.position;
        inventory.Drop(item);
    }

    public bool AddToInventory(GameObject item)
    {
        return inventory.Add(item);
    }
}
