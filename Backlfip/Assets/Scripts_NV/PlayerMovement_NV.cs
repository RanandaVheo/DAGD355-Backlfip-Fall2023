using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerMovement_NV : MonoBehaviour
{
    [SerializeField] TempBar_NV tempBar;

    public float speed;
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
                rb.velocity = new Vector2(rb.velocity.x, 10f);
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

        GameManager_NV.gameManagerNV.playerTemp.DamageUnit(10 * Time.deltaTime);
        tempBar.SetTemp(GameManager_NV.gameManagerNV.playerTemp.Temp);
        
        if(GameManager_NV.gameManagerNV.playerTemp.Temp <= 0)
        {
            // GAME OVER
            Debug.Log("GAME OVER");
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
            GameManager_NV.gameManagerNV.playerTemp.DamageUnit(15);
            tempBar.SetTemp(GameManager_NV.gameManagerNV.playerTemp.Temp);
            isDamaged = true;
            animator.SetBool("IsDamaged", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Juice")
        {
            GameManager_NV.gameManagerNV.playerTemp.HealUnit(5);
            tempBar.SetTemp(GameManager_NV.gameManagerNV.playerTemp.Temp);
        }
        if (collision.gameObject.tag == "Refresh")
        {

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Lava")
        {
            GameManager_NV.gameManagerNV.playerTemp.HealUnit(5 * Time.deltaTime);
            tempBar.SetTemp(GameManager_NV.gameManagerNV.playerTemp.Temp);
        }
    }


    private void animationUpdate()
    {
        animator.SetBool("IsDamaged", false);
        animator.SetBool("IsAttacking", false);
    }
}