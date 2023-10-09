using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockAttack_NV : MonoBehaviour
{

    public GameObject player;
    public GameObject rockPrefab;
    public float rockSpeed = 50f;
    private Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        target = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));

        Vector2 difference = target - player.transform.position;

        if(Input.GetMouseButtonDown(0))
        {
            float distance = difference.magnitude;
            Vector2 direction = difference / distance;
            direction.Normalize();
            rockAttack(direction);
        }

    }

    void rockAttack(Vector2 direction)
    {
        GameObject r = Instantiate(rockPrefab) as GameObject;
        r.transform.position = player.transform.position;
        r.GetComponent<Rigidbody2D>().velocity = direction * rockSpeed;
    }
}
