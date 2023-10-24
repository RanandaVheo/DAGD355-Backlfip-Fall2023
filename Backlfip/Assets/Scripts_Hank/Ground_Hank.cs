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
    public void Capture()
    {
        spriteRenderer.sprite = originalSprite;
        GameManager_NV.gameManagerNV.playerScore += 5;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       switch(collision.gameObject.tag)
        {
            case "Player":

                Capture();

                break;

            case "FireEnemy":

                Burn();
                break;

            case "WaterEnemy":

                Hydrate();
                break;
        }

    }
}
