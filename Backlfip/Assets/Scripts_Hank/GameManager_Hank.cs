using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager_Hank : MonoBehaviour
{
    [SerializeField] GameObject itemPrefab;
    [SerializeField] GameObject playerHandle;
    public GameObject tilePalletteHandle;
    private Timer_Hank itemSpawnTimer = new(2);
    private Tilemap tilemap;

    private void Start()
    {

        tilemap = tilePalletteHandle.GetComponentInChildren<Tilemap>();
    }

    void Update()
    {
        transform.parent.position = new Vector3(playerHandle.transform.position.x, playerHandle.transform.position.y, -10f);

        itemSpawnTimer.Update();
        if (itemSpawnTimer.isDone)
        {
            itemSpawnTimer.Reset();
            //Instantiate(itemPrefab, tilemap.GetCellCenterWorld(new Vector3Int((int)transform.position.x, (int)transform.position.y, 0)), Quaternion.identity, transform.parent);
            tilemap.GetCellCenterLocal(new Vector3Int(0, 0, 0));
        }
    }
}
