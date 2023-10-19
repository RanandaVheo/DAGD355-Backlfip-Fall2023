using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpawner_NV : MonoBehaviour
{

    public GameObject lavaSlimePrefab;
    public float respawnTime = 5f;
    public float spawnerHealth = 25f;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        StartCoroutine(slimeSpawner());
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnerHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void spawnSlime()
    {
        GameObject s = Instantiate(lavaSlimePrefab) as GameObject;
        s.transform.position = new Vector2(transform.position.x, transform.position.y);
    }

    IEnumerator slimeSpawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            spawnSlime();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Rock")
        {
            spawnerHealth -= 1;
        }
        if (collision.gameObject.tag == "Fireball")
        {
            spawnerHealth -= 3;
        }
    }
}
