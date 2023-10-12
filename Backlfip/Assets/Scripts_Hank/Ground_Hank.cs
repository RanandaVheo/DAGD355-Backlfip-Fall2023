using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Ground_Hank : MonoBehaviour
{

    public Sprite originalSprite;
    public Sprite fireSprite;
    public Sprite waterSprite;

    private Tilemap tilemapHandle;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = originalSprite;
        Tilemap tilemap = FindAnyObjectByType<Tilemap>();
        transform.position = tilemap.GetCellCenterWorld(new Vector3Int((int) transform.position.x, (int) transform.position.y, (int) transform.position.z));
    }

    public void Burn()
    {
        spriteRenderer.sprite = fireSprite;
    }

    public void Hydrate()
    {
        spriteRenderer.sprite = waterSprite;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       switch(collision.gameObject.tag)
        {
            case "Player":

                Burn();
                break;

            case "Enemy":

                Hydrate();
                break;
        }

    }
}
