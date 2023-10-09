using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniSlime_NV : MonoBehaviour
{

    GameObject player;
    public GameObject fireJuicePrefab;
    public float speed = 5f;
    public float burnTime = 1f;
    public Animator animator;
    private Rigidbody2D rb;
    private Vector2 movement;
    private float miniSlimeHealth = 2f;

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

        if (miniSlimeHealth == 0)
        {
            spawnFireJuice();
            Destroy(gameObject);
        }
    }

    private void spawnFireJuice()
    {
        GameObject j = Instantiate(fireJuicePrefab) as GameObject;
        j.transform.position = new Vector2(transform.position.x + 0.5f, transform.position.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Rock")
        {
            miniSlimeHealth--;
            animator.SetBool("IsHurt", true);
        }
        if (collision.gameObject.tag == "Fireball")
        {
            miniSlimeHealth--;
            Debug.Log("BURN");
            Invoke("burnDamage", 1);
            Invoke("burnDamage", 2);
            animator.SetBool("IsHurt", true);
        }
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Platform")
        {
            animator.SetBool("IsHurt", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Lava")
        {
            miniSlimeHealth--;
            Debug.Log("LAVA");
            Invoke("lavaBurn", 1);
            Invoke("lavaBurn", 2);
            Invoke("lavaBurn", 3);
        }
    }

    private void burnDamage()
    {
        miniSlimeHealth--;
        Debug.Log("BURNING");
    }

    private void lavaBurn()
    {
        miniSlimeHealth--;
        Debug.Log("BURNING");
    }
}
