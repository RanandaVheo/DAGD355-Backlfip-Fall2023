using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory_Hank : MonoBehaviour
{
    List<GameObject> items = new();
    public int inventorySize = 5;



    [SerializeField] GameObject inventoryUIPrefab;
    private GameObject inventoryUI;
    private bool tabDownLastFrame = false;
    private List<Image> uiImageSlots;
    private Canvas uiCanvas;

    void Start()
    {
        inventoryUI = Instantiate(inventoryUIPrefab, transform.parent);
        uiCanvas = inventoryUI.GetComponentInChildren<Canvas>();

        // adding more elements inventoryUIPrefab potentially increases this list
        // if there are more than five images objects inside its canvas, the drawing prodedure breaks
        uiImageSlots = inventoryUI.GetComponentsInChildren<Image>().ToList();
        
        
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
