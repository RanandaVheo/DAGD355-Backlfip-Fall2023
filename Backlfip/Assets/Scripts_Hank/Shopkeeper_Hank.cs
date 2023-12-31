using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Shopkeeper_Hank : MonoBehaviour
{

    public GameObject[] availableItems = { };
    [SerializeField] private GameObject indicatorHandle;
    [SerializeField] private GameObject purchaseableButtonPrefab;
    private Canvas shopCanvas;
    private Canvas textCanvas;
    private SpriteRenderer indicatorRenderer;
    private bool eKeyDownLastFrame = false;
    private GameObject playerHandle;
    public List<GameObject> buttons = new();

    void Start()
    {
        indicatorRenderer = indicatorHandle.GetComponent<SpriteRenderer>();
        indicatorRenderer.enabled = false;

        shopCanvas = GetComponentInChildren<Canvas>();
        shopCanvas.enabled = false;
        textCanvas = GetComponentsInChildren<Canvas>()[1];
        textCanvas.enabled = false;
        for (int i = 0; i < 5; i++)
        {
            GameObject tempPurchaseableButton = Instantiate(purchaseableButtonPrefab, shopCanvas.transform.position, Quaternion.identity, shopCanvas.transform);
            tempPurchaseableButton.transform.parent = shopCanvas.transform.parent;
            tempPurchaseableButton.transform.position = new Vector3(shopCanvas.transform.position.x - 2.2f + i * 1.1f, shopCanvas.transform.position.y, 1);
            buttons.Add(tempPurchaseableButton);

        }

        for (int i =0; i < availableItems.Length; i++)
        {

            buttons[i].GetComponent<PurchaseableButton_Hank>().SetItem(availableItems[i]);

        }
        
    }

    private void Update()
    {
        if (indicatorRenderer.enabled && Input.GetKeyDown(KeyCode.E))
        {
            if (!eKeyDownLastFrame)
            {
                ToggleShow();
            }
            eKeyDownLastFrame = true;
        }
        else
        {
            eKeyDownLastFrame = false;
        }

        if (!playerHandle) return;
        PlayerInventory_Hank inventory = playerHandle.GetComponent<PlayerInventory_Hank>();
        if (inventory.uiCanvas.enabled && shopCanvas.enabled)
        {
            inventory.uiCanvas.enabled = false;
        }
    }

    public bool TryPurchaseItem()
    {

        return true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            indicatorRenderer.enabled = true;
            textCanvas.enabled = true;
            playerHandle = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            indicatorRenderer.enabled = false;
            textCanvas.enabled = false;
            shopCanvas.enabled = false;
            foreach (GameObject button in buttons)
            {
                Canvas tempCanvas = button.GetComponentInChildren<Canvas>();
                tempCanvas.enabled = false;
            }
            
        }
    }

    void ToggleShow()
    {
        shopCanvas.enabled = !shopCanvas.enabled;
        foreach (GameObject button in buttons) 
        {
            Canvas tempCanvas = button.GetComponentInChildren<Canvas>();
            tempCanvas.enabled = !tempCanvas.enabled;
        }
        
    }

    
}
