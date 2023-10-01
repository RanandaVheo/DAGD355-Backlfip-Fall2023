using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniSlime_NV : MonoBehaviour
{

    GameObject player;
    public float speed = 5f;
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
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Rock")
        {
            miniSlimeHealth--;
        }
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
