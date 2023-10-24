using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;

public class PlayerMovement_NV : MonoBehaviour
{
    [SerializeField] TempBar_NV tempBar;

    public float speed;
    public int playerScore;
    public bool isDamaged;
    public bool isAttacking;
    public Animator animator;
    private Rigidbody2D rb;
    private bool grounded = true;

    public TextMeshProUGUI scoreUI;

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

        scoreUI.text = playerScore.ToString();

        GameManager_NV.gameManagerNV.playerTemp.DamageUnit(1 * Time.deltaTime);
        tempBar.SetTemp(GameManager_NV.gameManagerNV.playerTemp.Temp);
        
        if(GameManager_NV.gameManagerNV.playerTemp.Temp <= 0)
        {
            // GAME OVER
            SceneManager.LoadScene("GameOver_NV");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            animator.SetBool("IsJumping", false);
            grounded = true;
        }
        if (collision.gameObject.tag == "WaterEnemy")
        {
            GameManager_NV.gameManagerNV.playerTemp.DamageUnit(15);
            tempBar.SetTemp(GameManager_NV.gameManagerNV.playerTemp.Temp);
            isDamaged = true;
            animator.SetBool("IsDamaged", true);
            playerScore -= 5;
        }
        if (collision.gameObject.tag == "FireEnemy")
        {
            GameManager_NV.gameManagerNV.playerTemp.DamageUnit(15);
            tempBar.SetTemp(GameManager_NV.gameManagerNV.playerTemp.Temp);
            isDamaged = true;
            animator.SetBool("IsDamaged", true);
            playerScore -= 5;
        }
        if (collision.gameObject.tag == "Fishies")
        {
            GameManager_NV.gameManagerNV.playerTemp.HealUnit(15);
            tempBar.SetTemp(GameManager_NV.gameManagerNV.playerTemp.Temp);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Juice")
        {
            GameManager_NV.gameManagerNV.playerTemp.HealUnit(5);
            tempBar.SetTemp(GameManager_NV.gameManagerNV.playerTemp.Temp);
            playerScore += 5;
        }
        if (collision.gameObject.tag == "Coin")
        {
            playerScore += 25;
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