using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Keq : MonoBehaviour
{
    //public GameManager_Keq managerRef;

    private Rigidbody2D rb;
    private bool underwater = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        /*float h = Input.GetAxis("Horizontal");

        if (!managerRef.isGameOver)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.identity, Time.deltaTime * 320);

            //for now, lock player movement while fishing. Can be taken out later if it's not fun for play
            if(!managerRef.isFishing) transform.position += new Vector3(h, 0f, 0f) * Time.deltaTime * speed;
        }*/

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Underwater") && underwater == false)
        {
            rb.gravityScale = .06f;
            underwater = true;
        }

        if (collision.gameObject.CompareTag("Fishies"))
        {
            //make collecting this heal you
            Destroy(collision.gameObject);
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Underwater") && underwater == true)
        {
            rb.gravityScale = 1f;
            underwater = false;
        }
    }
}
