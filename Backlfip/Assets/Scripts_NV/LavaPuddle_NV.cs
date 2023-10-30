using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaPuddle_NV : MonoBehaviour
{

    public GameObject player;
    public GameObject lavaPuddlePrefab;
    public AudioSource source;
    public float lavaPuddleCooldown = 5;
    public bool lavaOnCooldown = false;
    private Vector3 target;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        target = new Vector2(player.transform.position.x, player.transform.position.y + 1.75f);

        if (Input.GetKeyDown(KeyCode.Q) && lavaOnCooldown == false)
        {
            source.Play();
            lavaOnCooldown = true;
            lavaPuddle();
        }
        if (lavaOnCooldown)
        {
            lavaPuddleCooldown -= 1 * Time.deltaTime;
            if (lavaPuddleCooldown <= 0)
            {
                lavaPuddleCooldown = 5;
                lavaOnCooldown = false;
            }
        }
    }

    void lavaPuddle()
    {
        GameObject l = Instantiate(lavaPuddlePrefab);
        l.transform.position = target;
    }
}
