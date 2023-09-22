using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Hank : MonoBehaviour
{
    [SerializeField] GameObject itemPrefab;
    private Timer_Hank itemSpawnTimer = new(2);

    void Update()
    {
        itemSpawnTimer.Update();
        if (itemSpawnTimer.isDone)
        {
            itemSpawnTimer.Reset();
            Instantiate(itemPrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity, transform.parent);
        }
    }
}
