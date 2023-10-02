using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaPuddle_NV : MonoBehaviour
{

    public GameObject player;
    public GameObject lavaPuddlePrefab;
    private Vector3 target;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        target = new Vector2(player.transform.position.x, player.transform.position.y - 0.75f);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            lavaPuddle();
        }
    }

    void lavaPuddle()
    {
        GameObject l = Instantiate(lavaPuddlePrefab);
        l.transform.position = target;
    }
}
