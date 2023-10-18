using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_NV : MonoBehaviour
{

    public float speed;
    public float fireJuice = 5f;
    public float lavaTime = 1f;
    public bool isDamaged;
    public bool isAttacking;
    public Animator animator;
    private Rigidbody2D rb;
    private bool grounded = true;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        animator.SetFloat("Speed", Mathf.Abs(h));
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("IsJumping", true);
            if (grounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, 5f);
                grounded = false;
            }
            else
            {
                rb.velocity = new Vector2(h * speed, rb.velocity.y);
            }
        }
        else
        {
            rb.velocity = new Vector2(h * speed, rb.velocity.y);
        }

        if (Input.GetMouseButtonDown(0))
        {
            isAttacking = true;
            animator.SetBool("IsAttacking", true);
        }
        else
        {
            isAttacking = false;
            animator.SetBool("IsAttacking", false);
        }


        if (Input.GetMouseButtonDown(1))
        {
            isAttacking = true;
            animator.SetBool("IsAttacking", true);
        }
        else
        {
            isAttacking = false;
            animator.SetBool("IsAttacking", false);
        }

        if (isDamaged == true)
        {
            Invoke("animationUpdate", 0.1f);
            isDamaged = false;
        }

        if (isAttacking == true)
        {
            Invoke("animationUpdate", 0.1f);
            isAttacking = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            animator.SetBool("IsJumping", false);
            grounded = true;
        }
        if (collision.gameObject.tag == "Enemy")
        {
            isDamaged = true;
            Debug.Log("DAMAGE");
            animator.SetBool("IsDamaged", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Juice")
        {
            fireJuice++;
            Debug.Log("MORE JUICE!");
        }
        if (collision.gameObject.tag == "Lava")
        {
            Invoke("inLava", 1);
            Invoke("inLava", 2);
            Invoke("inLava", 3);
        }
    }

    private void inLava()
    {
        fireJuice++;
        Debug.Log("MORE JUICE");
    }

    private void animationUpdate()
    {
        animator.SetBool("IsDamaged", false);
        animator.SetBool("IsAttacking", false);
    }
}