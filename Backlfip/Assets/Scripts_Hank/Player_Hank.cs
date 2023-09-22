using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player_Hank : MonoBehaviour
{
    public float speed = 1.0f;
    public float maxSpeed = 5f;

    private Rigidbody2D rb;
    private PlayerInventory_Hank inventory;
    private Timer_Hank timeToStopTimer = new(.5f);
    private bool qDownLastFrame = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inventory = GetComponent<PlayerInventory_Hank>();
    }

    // Update is called once per frame
    void Update()
    {
        if ( true ) 
        {
            
            rb.AddForce(new Vector2(Input.GetAxis("Horizontal") * speed, 0f));
            timeToStopTimer.Reset();
        }
        //if (Input.GetAxis("Horizontal") == 0f)
        //{
        //    timeToStopTimer.Update();
        //    rb.velocity = new Vector2( Mathf.Lerp(rb.velocity.x, 0f, timeToStopTimer.timeLeft / timeToStopTimer.duration), rb.velocity.y);
        //}

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
