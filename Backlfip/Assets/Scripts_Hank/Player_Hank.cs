using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player_Hank : MonoBehaviour
{
    public float speed = 1.0f;

    private Rigidbody2D rb;
    private PlayerInventory_Hank inventory;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inventory = GetComponent<PlayerInventory_Hank>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(new Vector2(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed));
    }

    public bool AddToInventory(GameObject item)
    {
        return inventory.Add(item);
    }
}
