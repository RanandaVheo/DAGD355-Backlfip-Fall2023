using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockAttack : MonoBehaviour
{

    public GameObject player;
    public GameObject rockPrefab;
    public float rockSpeed = 60f;
    private Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        target = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));

        Vector3 difference = target - player.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        player.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);

        if(Input.GetMouseButtonDown(0))
        {
            float distance = difference.magnitude;
            Vector2 direction = difference / distance;
            direction.Normalize();
            rockAttack(direction, rotationZ);
        }
    }

    void rockAttack(Vector2 direction, float rotationZ)
    {
        GameObject r = Instantiate(rockPrefab) as GameObject;
        r.transform.position = player.transform.position;
        r.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
        r.GetComponent<Rigidbody2D>().velocity = direction * rockSpeed;
    }
}
