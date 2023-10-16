using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LavaTile_Hank : MonoBehaviour
{
    
    private SpriteRenderer spriteRenderer;
    private Tilemap tilemap;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        tilemap = FindAnyObjectByType<Tilemap>();
        transform.position = tilemap.GetCellCenterWorld(new Vector3Int((int)transform.position.x, (int)transform.position.y, (int)transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Player":
                // heal the player
                break;
            case "Enemy":
                // damage enemy
                break;
        }
    }
}
