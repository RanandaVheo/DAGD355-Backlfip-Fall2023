using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Hank : MonoBehaviour
{
    public string itemName = "template name";
    public string description = "create prefab instance for specific item";
    public bool canStack = true;
    public int amount = 1;
    public int maxStack = 3;

    private bool onGround = true;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!onGround) return;
        if (collision.tag == "Player")
        {
            Player_Hank player_HankScript = collision.GetComponent<Player_Hank>();
            bool addedSuccessfully = player_HankScript.AddToInventory(gameObject);
            if (!addedSuccessfully) return;
            Disable();
            onGround = false;
        }
    }

    public void Drop()
    {
        onGround = true;
        Enable();
    }

    public void Disable()
    {
        spriteRenderer.enabled = false;
        circleCollider.enabled = false;
    }

    public void Enable()
    {
        spriteRenderer.enabled = true;
        circleCollider.enabled = true;
    }

}
