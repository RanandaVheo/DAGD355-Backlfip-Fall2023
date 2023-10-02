using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner_Keq : MonoBehaviour
{

    public GameObject enemy;
    public GameManager_Keq managerRef;
    public float cooldownTimer = 10f;

    
    void Update()
    {
        if (!managerRef.isGameOver) cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0f)
        {
            Instantiate(enemy, transform.position, Quaternion.identity);
            cooldownTimer = 10f;
        }
    }
}
