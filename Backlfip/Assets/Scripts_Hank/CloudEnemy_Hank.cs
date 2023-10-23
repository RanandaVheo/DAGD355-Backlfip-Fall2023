using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudEnemy_Hank : MonoBehaviour
{

    private GameObject player;
    [SerializeField] private GameObject rainDrop;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private Timer_Hank rainTimer = new(0.05f);

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rainTimer.Update();
        if (player.transform.position.x > transform.position.x)
        {
            spriteRenderer.flipX = false;
            transform.position = new Vector2(transform.position.x + 1 * Time.deltaTime, 0);
        }
        if (player.transform.position.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
            transform.position = new Vector2(transform.position.x - 1 * Time.deltaTime, 0);
        }

        float xSpawnLocation = Random.Range(0, boxCollider.size.x* 2.5f);

        if (rainTimer.isDone)
        {
            Instantiate(rainDrop, new Vector2(transform.position.x - boxCollider.size.x + xSpawnLocation, transform.position.y), Quaternion.identity);
            rainTimer.Reset();
        }
        
    }
}
