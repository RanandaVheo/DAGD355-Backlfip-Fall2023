using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class PlayerInventory_Hank : MonoBehaviour
{
    List<GameObject> items = new();
    public int inventorySize = 5;
    public int selectedItemIndex = 0;



    [SerializeField] GameObject inventoryUIPrefab;
    [SerializeField] Sprite activeItemIndicator;
    private GameObject inventoryUI;
    private bool tabDownLastFrame = false;
    private List<Image> uiImageSlots;
    private List<Image> uiImageOverlaySlots;
    private Canvas uiCanvas;

    void Start()
    {
        inventoryUI = Instantiate(inventoryUIPrefab, transform.parent);
        uiCanvas = inventoryUI.GetComponentInChildren<Canvas>();

        uiImageSlots = new List<Image>();
        uiImageOverlaySlots = new List<Image>();
        foreach (Image i in inventoryUI.GetComponentsInChildren<Image>().ToList())
        {
            if (i.gameObject.CompareTag("GameController"))
            {
                uiImageSlots.Add(i);
            }
            if (i.gameObject.CompareTag("Untagged"))
            {
                uiImageOverlaySlots.Add(i);
            }
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!tabDownLastFrame)
            {
                Show();
            }
            tabDownLastFrame = true;
        }
        else
        {
            tabDownLastFrame = false;
        }

        inventoryUI.transform.position = gameObject.transform.position;

        if (!uiCanvas.enabled) return;

        if (selectedItemIndex < 0) selectedItemIndex = uiImageOverlaySlots.Count - 1;
        if (selectedItemIndex >= uiImageOverlaySlots.Count) selectedItemIndex = 0;


        for (int i = 0; i < uiImageOverlaySlots.Count; i++)
        {
            if (i == selectedItemIndex)
            {
                uiImageOverlaySlots[i].sprite = activeItemIndicator;
                uiImageOverlaySlots[i].color = new Color(255, 255, 255, 255);
            } else
            {
                uiImageOverlaySlots[i].sprite = null;
                uiImageOverlaySlots[i].color = new Color(255, 255, 255, 0);
            }
        }


        int potentiallyEmptySlots = 5;
        for (int i = 0; i < items.Count; i++)
        {
            potentiallyEmptySlots--;
            uiImageSlots[i].sprite = items[i].GetComponent<Item_Hank>().spriteRenderer.sprite;
            uiImageSlots[i].color = items[i].GetComponent<Item_Hank>().spriteRenderer.color;
        }
        for (int i = 4; potentiallyEmptySlots > 0; potentiallyEmptySlots--)
        {
            uiImageSlots[i].sprite = null;
            uiImageSlots[i].color = Color.white;
            i--;
        }
        
    }

    void Show()
    {
        uiCanvas.enabled = !uiCanvas.enabled;
        foreach (GameObject item in items)
        {
            Item_Hank itemScript = item.GetComponent<Item_Hank>();
            Debug.Log("Item '" + itemScript.itemName + "': " + itemScript.amount);
        }
    }

    public bool Add(GameObject item)
    {
        if (!IsItemValid(item))
        {
            Debug.Log("Could not add item(s) '" + item.name + "': invalid item.");
            return false;
        }


        Item_Hank currentItem = item.GetComponent<Item_Hank>();

        if (currentItem.canStack)
        {
            Item_Hank itemWithUnfinishedStack = TryFindUnfinishedStack(currentItem.itemName);

            // check if any unfinished stacks exist and fill them before adding another item to inventory
            if (itemWithUnfinishedStack != null)
            {
                while (currentItem.amount > 0)
                {
                    itemWithUnfinishedStack.amount++;
                    currentItem.amount--;
                    if (itemWithUnfinishedStack.amount >= itemWithUnfinishedStack.maxStack)
                    {
                        itemWithUnfinishedStack = TryFindUnfinishedStack(currentItem.itemName);
                        if (itemWithUnfinishedStack == null) break;
                    }

                }
            }
        }
        // make sure to check if item amount has not dropped to zero before adding
        if (currentItem.amount > 0)
        {
            if (items.Count >= inventorySize)
            {
                Debug.Log("Could not add item(s): inventory full");
                return false;
            }

            items.Add(item);
        }

        Debug.Log("Item(s) added to inventory successfully.");
        return true;
    }

    public void DropSelectedItem()
    {
        Item_Hank itemToDrop = items[selectedItemIndex].GetComponent<Item_Hank>();
        if (itemToDrop == null)
        {
            Debug.Log("No item to drop");
            return;
        }
        Drop(items[selectedItemIndex]);
        itemToDrop.transform.position = transform.position;
        itemToDrop.Drop();
    }

    public void Drop(GameObject item)
    {
        Item_Hank itemToDrop = item.GetComponent<Item_Hank>();
        items.Remove(item);
        itemToDrop.Drop();
    }

    bool IsItemValid(GameObject item)
    {
        if (!item.CompareTag("Item"))
        {
            Debug.Log("Item missing 'Item' tag");
            return false;
        }
        return true;    
    }

    Item_Hank TryFindUnfinishedStack(string itemName)
    {
        foreach (GameObject i in items)
        {
            Item_Hank tempItemScript = i.GetComponent<Item_Hank>();
            if (tempItemScript.itemName == itemName && tempItemScript.amount < tempItemScript.maxStack)
            {
                return tempItemScript;
            }
        }
        return null;
    }

    public GameObject TryGetItem()
    {
        try
        {
            return items[0];
        }
        catch
        {
            return null;
        }
        
    }
}
