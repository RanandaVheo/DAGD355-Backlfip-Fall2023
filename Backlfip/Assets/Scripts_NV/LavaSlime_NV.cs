using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaSlime_NV : MonoBehaviour
{

    public AudioSource source;
    GameObject player;
    public GameObject miniSlimePrefab;
    public float speed = 5f;
    public float burnTime = 2f;
    public bool inLava = false;
    public Animator animator;
    private Rigidbody2D rb;
    private Vector2 movement;
    private float slimeHealth = 7f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        if(slimeHealth == 0)
        {
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Rock")
        {
            slimeHealth--;
            animator.SetBool("IsHurt", true);
            source.Play();
        }
        if (collision.gameObject.tag == "Fireball")
        {
            slimeHealth--;
            source.Play();
            Invoke("burnDamage", 1);
            Invoke("burnDamage", 2);
            animator.SetBool("IsHurt", true);
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Lava" )
        {
            slimeHealth--;
            Invoke("lavaBurn", 1);
            Invoke("lavaBurn", 2);
            Invoke("lavaBurn", 3);
        }
    }

    private void burnDamage()
    {
        slimeHealth--;
        source.Play();
        Debug.Log("BURNING");
    }

    private void lavaBurn()
    {
        slimeHealth--;
        source.Play();
        Debug.Log("BURNING");
    }
}
