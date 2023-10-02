using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyController_Keq : MonoBehaviour
{
    private Transform playerRef;
    private float movementSpeed = 3.5f;

    // Start is called before the first frame update
    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movementVec = playerRef.transform.position - transform.position;
        movementVec = movementVec.normalized;

        if (Vector2.Distance(playerRef.position, transform.position) > 0f)
        {
            transform.position += (movementVec * movementSpeed * Time.deltaTime);
        }
    }

    //collision with player is handled in the PlayerMove script
}
