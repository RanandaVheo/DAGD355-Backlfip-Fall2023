using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WaterTile_Hank : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Tilemap tilemap = FindAnyObjectByType<Tilemap>();
        transform.position = tilemap.GetCellCenterWorld(new Vector3Int((int)transform.position.x, (int)transform.position.y, (int)transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                Player_Hank player = collision.gameObject.GetComponent<Player_Hank>();
                player.TakeDamage(10);

                break;
            case "Enemy":
                break;
        }
    }
}
