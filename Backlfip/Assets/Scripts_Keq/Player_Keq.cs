using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Keq : MonoBehaviour
{
    public GameManager_Keq managerRef;
    public HPBar_Keq HPBarRef;
    public float speed = 10f;

    private PlayerInventory_Hank inventory;
    private Rigidbody2D rb;
    private bool underwater = false;
    private bool qDownLastFrame = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inventory = GetComponent<PlayerInventory_Hank>();
    }

    
    void Update()
    {
        if (!managerRef.isGameOver)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.identity, Time.deltaTime * 320);

            //for now, lock player movement while fishing. Can be taken out later if it's not fun for play
            if (!managerRef.isFishing)
            {
                //Movement Things
                transform.position += new Vector3(Input.GetAxis("Horizontal"), 0f, 0f) * Time.deltaTime * speed;

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    rb.AddForce(Vector2.up * 100);
                }
                
                //Inventory Things
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    if (!qDownLastFrame)
                    {
                        DropFromInventory(inventory.TryGetItem());
                    }
                    qDownLastFrame = true;
                }
                else
                {
                    qDownLastFrame = false;
                }
            }
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            HPBarRef.changeHPBar(true, 1);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Underwater") && underwater == false)
        {
            rb.gravityScale = .06f;
            underwater = true;
        }

        if (collision.gameObject.CompareTag("Fishies"))
        {
            HPBarRef.changeHPBar(false, 1);
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
