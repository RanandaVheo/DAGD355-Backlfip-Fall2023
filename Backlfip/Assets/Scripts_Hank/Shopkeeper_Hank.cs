using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopkeeper_Hank : MonoBehaviour
{

    [SerializeField] GameObject[] availableItems = { };
    [SerializeField] private GameObject indicatorHandle;
    private Canvas shopCanvas;
    private SpriteRenderer indicatorRenderer;
    private bool eKeyDownLastFrame = false;
    private GameObject playerHandle;

    void Start()
    {
        indicatorRenderer = indicatorHandle.GetComponent<SpriteRenderer>();
        shopCanvas = GetComponentInChildren<Canvas>();
        Debug.Log(shopCanvas == null);
        
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
    }

    public void TryPurchaseItem()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            indicatorRenderer.enabled = true;
            playerHandle = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            indicatorRenderer.enabled = false;
            shopCanvas.enabled = false;
        }
    }

    void ToggleShow()
    {
        shopCanvas.enabled = !shopCanvas.enabled;
    }

    
}
