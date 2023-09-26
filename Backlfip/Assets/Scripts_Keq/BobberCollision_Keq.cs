using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class BobberCollision_Keq : MonoBehaviour
{
    public GameObject spawnedObject;
    public GameManager managerRef;
    public float COOLDOWN_TIMER_MAX = 1f;

    private float cooldownTimer = 1f;

    // Start is called before the first frame update
    void Start()
    {
        cooldownTimer = COOLDOWN_TIMER_MAX; //sets the timer to what we need it to be
    }

    // Update is called once per frame
    void Update()
    {
        //when the timer reaches 0,
        //if (cooldownTimer <= 0f)
        //{
        //    Instantiate(spawnedObject, transform.position, Quaternion.identity); //spawn the selected object at this current position
        //    cooldownTimer = COOLDOWN_TIMER_MAX; //and reset timer
        //}
    }

    //For the Fishing Bobber, we want the timer to go down while its touching the water
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Underwater") && !managerRef.isGameOver)
        {
            managerRef.isFishing = true; //cooldownTimer -= Time.deltaTime;

        }
    }
}
