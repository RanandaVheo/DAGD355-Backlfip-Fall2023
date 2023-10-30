using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;

public class Player_Keq : MonoBehaviour
{
    public GameManager_Keq managerRef;

    private Rigidbody2D rb;
    private bool underwater = false;
    private PlayerInventory_Hank inventory;
    private bool xDownLastFrame = false;
    private bool eDownLastFrame = false;
    private TextMeshProUGUI textMesh;
    public int money = 0;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inventory = GetComponent<PlayerInventory_Hank>();
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
    }

    
    void Update()
    {
        managerRef.canFish = !underwater;
        textMesh.text = "$ " + money.ToString();

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (!xDownLastFrame)
            {
                inventory.DropSelectedItem();
            }
            xDownLastFrame = true;
        }
        else
        {
            xDownLastFrame = false;
        }

        // HOW TO USE ITEMS
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!eDownLastFrame)
            {
                UseSelectedItem(); // see this function for details
            }
            eDownLastFrame = true;
        }
        else
        {
            eDownLastFrame = false;
        }

        float mouseWheelvalue = Input.GetAxis("Mouse ScrollWheel");
        if (mouseWheelvalue > 0) inventory.selectedItemIndex--;
        if (mouseWheelvalue < 0) inventory.selectedItemIndex++;

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


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Underwater") && underwater == false)
        {
            rb.gravityScale = .06f;
            underwater = true;
        }

        if (collision.gameObject.CompareTag("Fishies"))
        {
            
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

    void UseSelectedItem()
    {
        string selectedItemName = inventory.GetSelectedItemName();
        if (selectedItemName == null) return;

        switch (selectedItemName)
        {
            case "ExampleItemName":
                // do related example item functions!
                break;
            case "Deed":
                SceneManager.LoadScene("GameWin_NV");
                break;
            default:
                Debug.Log(selectedItemName + " used!");
                break;
        }
    }
}
