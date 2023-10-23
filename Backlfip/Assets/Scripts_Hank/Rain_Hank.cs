using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain_Hank : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        switch(collision.tag)
        {
            case "Player":
                Destroy(gameObject);
                break;
            case "WaterEnemy":
                break;
            case "Platform":
                Destroy(gameObject);
                break;
        }
    }
}
