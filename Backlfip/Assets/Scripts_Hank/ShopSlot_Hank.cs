using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSlot_Hank : MonoBehaviour
{

    Shopkeeper_Hank shopkeeper;
    // Start is called before the first frame update
    void Start()
    {
        shopkeeper = GetComponentInParent<Shopkeeper_Hank>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
