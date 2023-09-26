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
    public  SpriteRenderer spriteRenderer;
    private CircleCollider2D[] circleColliders;
    private Timer_Hank pickupCooldownTimer = new(1);

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        circleColliders = GetComponents<CircleCollider2D>();

        spriteRenderer.color = Random.ColorHSV();
    }



    // Update is called once per frame
    void Update()
    {
        pickupCooldownTimer.Update();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!onGround || !pickupCooldownTimer.isDone) return;
        if (collision.CompareTag("Player"))
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
        pickupCooldownTimer.Reset();
        onGround = true;
        Enable();
    }

    public void Disable()
    {
        spriteRenderer.enabled = false;
        foreach (CircleCollider2D circleCollider in circleColliders)
        {
            circleCollider.enabled = false;
        }
    }

    public void Enable()
    {
        spriteRenderer.enabled = true;
        foreach (CircleCollider2D circleCollider in circleColliders)
        {
            circleCollider.enabled = true;
        }
    }

}
