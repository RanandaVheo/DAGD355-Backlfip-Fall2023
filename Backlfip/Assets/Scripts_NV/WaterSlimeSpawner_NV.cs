using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSlimeSpawner_NV : MonoBehaviour
{

    public GameObject waterSlimePrefab;
    public float respawnTime = 5f;
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

    }

    private void spawnSlime()
    {
        GameObject s = Instantiate(waterSlimePrefab) as GameObject;
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
}
