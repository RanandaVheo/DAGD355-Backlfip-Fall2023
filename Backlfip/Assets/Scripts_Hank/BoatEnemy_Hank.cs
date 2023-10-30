using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatEnemy_Hank : MonoBehaviour
{
    public Sprite damagedSprite;
    public Sprite normalSprite;
    [SerializeField] GameObject coinPrefab;
    private SpriteRenderer spriteRenderer;
    private Combat_Hank combat;
    void Start()
    {
        combat = GetComponent<Combat_Hank>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (combat.isDead)
        {
            Kill();
            return;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Rock":
                combat.TakeDamage(2);
                spriteRenderer.sprite = damagedSprite;
                Invoke("ChangeSprite", .2f);
                break;
        }
    }

    void ChangeSprite()
    {
        spriteRenderer.sprite = normalSprite;
    }

    void Kill()
    {
        for (int i = 0; i < 20; i++)
        {

            GameObject tempCoin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
            tempCoin.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
        }
        Destroy(gameObject);
    }
}
