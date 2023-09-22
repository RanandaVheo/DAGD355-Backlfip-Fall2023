using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInventory_Hank : MonoBehaviour
{
    List<GameObject> items = new List<GameObject>();
    public int inventorySize = 5;


    private bool tabDownLastFrame = false;

    void Start()
    {

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
    }

    void Show()
    {
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
        if (item.tag != "Item")
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
