using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SocialPlatforms.Impl;

public class Ground_Hank : MonoBehaviour
{

    public Sprite originalSprite;
    public Sprite fireSprite;
    public Sprite waterSprite;


    //private Tilemap tilemapHandle;
    private SpriteRenderer spriteRenderer;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //Tilemap tilemap = FindAnyObjectByType<Tilemap>();
        //transform.position = tilemap.GetCellCenterWorld(new Vector3Int((int) transform.position.x, (int) transform.position.y, (int) transform.position.z));
    }

    private void Update()
    {

    }

    public void Burn()
    {
        if (spriteRenderer.sprite == waterSprite)
        {
            spriteRenderer.sprite = fireSprite;
        }
        if (spriteRenderer.sprite == originalSprite)
        {
            spriteRenderer.sprite = fireSprite;
            GameManager_NV.gameManagerNV.tileTracker.tileLost();
        }
    }

    public void Hydrate()
    {
        if (spriteRenderer.sprite == fireSprite)
        {
            spriteRenderer.sprite = waterSprite;
        }
        if (spriteRenderer.sprite == originalSprite)
        {
            spriteRenderer.sprite = waterSprite;
            GameManager_NV.gameManagerNV.tileTracker.tileLost();
        }
    }
    public void Capture()
    {
        if(spriteRenderer.sprite == fireSprite || spriteRenderer.sprite == waterSprite)
        {
            spriteRenderer.sprite = originalSprite;
            GameManager_NV.gameManagerNV.tileTracker.tileCaptured();
        }
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
