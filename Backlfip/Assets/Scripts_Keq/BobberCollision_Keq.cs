using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class BobberCollision_Keq : MonoBehaviour
{
    public GameObject spawnedObject;
    public GameManager_Keq managerRef;
    private GameObject playerHandle;
    [SerializeField] GameObject boatPrefab;

    // Start is called before the first frame update
    void Start()
    {
        playerHandle = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (managerRef.fishingWin) fishingSpawn();
    }

    //For the Fishing Bobber, we want the timer to go down while its touching the water
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Underwater") && !managerRef.isGameOver && managerRef.canFish)
        {
            managerRef.isFishing = true;

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Underwater")) managerRef.isFishing = false;
    }

    //needed to spawn prizes on a success. Or enemies.
    public void fishingSpawn()
    {
        GameObject fishedObject = Instantiate(spawnedObject, transform.position, Quaternion.identity); //spawn the selected object at this current position
        int boatSpawnInt = Random.Range(0, 20);
        if (boatSpawnInt == 19)
        {
            Instantiate(boatPrefab, transform.position, Quaternion.identity);
        }

        fishedObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Vector2 directionToPlayer = new Vector2(playerHandle.transform.position.x - fishedObject.transform.position.x , playerHandle.transform.position.y - fishedObject.transform.position.y).normalized;
        fishedObject.GetComponent<Rigidbody2D>().AddForce(directionToPlayer * 400);
        Debug.Log(fishedObject.transform.position);
        managerRef.fishingWin = false;

    }
}
