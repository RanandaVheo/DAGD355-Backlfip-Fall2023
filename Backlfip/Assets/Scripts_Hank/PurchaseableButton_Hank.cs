using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseableButton_Hank : MonoBehaviour
{
    public GameObject purchaseableItem;
    private List<GameObject> parentList;
    public int cost = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (!purchaseableItem.CompareTag("Item"))
        {
            Debug.Log("WARNING! Shopkeeper's purchaseableButton slots are holding an invalid item: missing 'Item' tag");
        }

        parentList = GetComponentInParent<Shopkeeper_Hank>().buttons;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyItem()
    {
        parentList.Remove(gameObject);

        Destroy(gameObject);
    }

    public void SetItem(GameObject item)
    {
        if (!item.CompareTag("Item"))
        {
            Debug.Log("PurchaseButton SetItem method failed: item missing 'Item' tag");
            return;
        }
        purchaseableItem = item;

        GetComponentInChildren<Button>().image.sprite = item.GetComponentInChildren<SpriteRenderer>().sprite;
        Item_Hank itemScript = item.GetComponent<Item_Hank>();

        switch (itemScript.itemName)
        {
            case "Deed":
                cost = 100;

                break;
            default:
                cost = 3;
                break;
        }
        GetComponentInChildren<TextMeshProUGUI>().text = itemScript.itemName + ":\n$" + cost.ToString();
    }
}
