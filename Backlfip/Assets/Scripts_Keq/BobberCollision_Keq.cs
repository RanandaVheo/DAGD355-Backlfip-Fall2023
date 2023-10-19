using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class BobberCollision_Keq : MonoBehaviour
{
    public GameObject spawnedObject;
    public GameManager_Keq managerRef;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (managerRef.fishingWin) fishingSpawn();
    }

    //For the Fishing Bobber, we want the timer to go down while its touching the water
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Underwater") && !managerRef.isGameOver)
        {
            managerRef.isFishing = true; //cooldownTimer -= Time.deltaTime;

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Underwater")) managerRef.isFishing = false;
    }

    //needed to spawn prizes on a success. Or enemies.
    public void fishingSpawn()
    {
        Instantiate(spawnedObject, transform.position, Quaternion.identity); //spawn the selected object at this current position
        managerRef.fishingWin = false;
    }
}
