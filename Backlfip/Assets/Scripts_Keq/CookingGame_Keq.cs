using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class CookingGame_Keq : MonoBehaviour
{
    //public GameManager_Keq managerRef;
    public PlayerInventory_Hank invRef;
    public GameObject BGImage;
    public Transform invSpawn;
    public Image ingredientL, ingredientR, ingredientT, mealResult;
    private int ingredientCount = 0; // L = 0, R = 1, T = 2, meal = 3

    public bool isCooking = false;

    private bool prevCpressed = false;
    private GameObject invParent;


    // Start is called before the first frame update
    void Start()
    {
        //invParent = invRef.GetParent
    }

    // Update is called once per frame
    void Update()
    {

        if (!prevCpressed && Input.GetKeyDown(KeyCode.C))
        {
            isCooking = !isCooking;
            BGImage.SetActive(!BGImage.activeSelf);
            CookingInv(isCooking);
        }

        prevCpressed = Input.GetKeyDown(KeyCode.C);
    }
    public void CookingInv(bool status) //true = cooking is being turned on, false = cooking is being turned off
    {
        invRef.uiCanvas.enabled = !invRef.uiCanvas.enabled;

        if (status)
        {
            invRef.inventoryUI.transform.SetParent(invSpawn, true);
            invRef.inventoryUI.transform.position = invSpawn.position;
            invRef.inventoryUI.transform.localScale = Vector3.one;
        }
        else
        {
            print("close inventory");
        }

        

        
    }

    public void ingredientAdded(Item_Hank ingredient)
    {
        if (ingredientCount == 0)
        {
            ingredientL.sprite = ingredient.spriteRenderer.sprite;
        }
        if (ingredientCount == 1)
        {
            ingredientR.sprite = ingredient.spriteRenderer.sprite;
        }
        if (ingredientCount == 2)
        {
            ingredientT.sprite = ingredient.spriteRenderer.sprite;
        }
        if (ingredientCount == 3)
        {
            print("make the meal");
        }




    }









    //make function to add items to inventory if box is exited before making a meal
}
