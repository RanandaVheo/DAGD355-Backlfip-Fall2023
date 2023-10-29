using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaSlime_NV : MonoBehaviour
{

    public AudioSource source;
    GameObject player;
    public GameObject miniSlimePrefab;
    public GameObject slimeSplit;
    public float speed = 5f;
    public float burnTime = 2f;
    public bool inLava = false;
    public Animator animator;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Combat_Hank combat;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = this.GetComponent<Rigidbody2D>();
        combat = GetComponent<Combat_Hank>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        if(combat.isDead)
        {
            spawnSlimeSplit();
            spawnMiniSlime1();
            spawnMiniSlime2();
            Destroy(gameObject);
        }
    }


    private void spawnMiniSlime1()
    {
        GameObject m = Instantiate(miniSlimePrefab) as GameObject;
        m.transform.position = new Vector2(transform.position.x + 0.5f, transform.position.y);
    }

    private void spawnMiniSlime2()
    {
        GameObject m = Instantiate(miniSlimePrefab) as GameObject;
        m.transform.position = new Vector2(transform.position.x - 0.5f, transform.position.y);
    }

    private void spawnSlimeSplit()
    {
        GameObject s = Instantiate(slimeSplit) as GameObject;
        s.transform.position = new Vector2(transform.position.x, transform.position.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Rock")
        {
            combat.TakeDamage(2);
            animator.SetBool("IsHurt", true);
            source.Play();
        }
        if (collision.gameObject.tag == "Fireball")
        {
            burnDamage();
            Invoke("burnDamage", 1);
            Invoke("burnDamage", 2);
        }
        if (collision.gameObject.tag == "Player")
        {
            source.Play();
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Platform")
        {
            animator.SetBool("IsHurt", false);
        }
        if (collision.gameObject.tag == "Underwater")
        {
            combat.isDead = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Lava")
        {
            burnDamage();
            Invoke("burnDamage", 0.5f);
            Invoke("burnDamage", 1);
        }
    }

    private void burnDamage()
    {
        animator.SetBool("IsHurt", true);
        combat.TakeDamage(2);
        source.Play();
    }

    private void lavaDamage()
    {
        animator.SetBool("IsHurt", true);
        combat.TakeDamage(2);
        source.Play();
    }
}
