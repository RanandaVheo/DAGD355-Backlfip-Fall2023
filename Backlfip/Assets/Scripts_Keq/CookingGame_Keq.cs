using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class CookingGame_Keq : MonoBehaviour
{
    public GameManager_Keq managerRef;
    public PlayerInventory_Hank invRef;
    public GameObject BGImage;
    public Transform invSpawn;

    private bool prevCpressed = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!prevCpressed && Input.GetKeyDown(KeyCode.C))
        {
            managerRef.isCooking = !managerRef.isCooking;
            BGImage.SetActive(!BGImage.activeSelf);
            //Show();
        }

        /*
        public void Show(Transform spawnPoint)
        {
            inventoryUI.transform.SetParent(spawnPoint, true);
            inventoryUI.transform.position = spawnPoint.position;
            inventoryUI.transform.localScale = Vector3.one;
            inventoryUI.transform.SetParent(spawnPoint, true);

            uiCanvas.enabled = !uiCanvas.enabled;
            foreach (GameObject item in items)
            {
                Item_Hank itemScript = item.GetComponent<Item_Hank>();
                Debug.Log("Item '" + itemScript.itemName + "': " + itemScript.amount);
            }
        }
        */

        prevCpressed = Input.GetKeyDown(KeyCode.C);
    }
}
