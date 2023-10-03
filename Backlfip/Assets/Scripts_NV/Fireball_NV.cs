using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball_NV : MonoBehaviour
{

    public GameObject player;
    public GameObject fireballPrefab;
    public float rockSpeed = 75f;
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

        if (Input.GetMouseButtonDown(1))
        {
            // NEED TO ADD: CHECK IF PLAYER HAS ENOUGH FIRE JUICE
            float distance = difference.magnitude;
            Vector2 direction = difference / distance;
            direction.Normalize();
            fireball(direction, rotationZ);
        }
    }

    void fireball(Vector2 direction, float rotationZ)
    {
        GameObject f = Instantiate(fireballPrefab) as GameObject;
        f.transform.position = player.transform.position;
        f.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
        f.GetComponent<Rigidbody2D>().velocity = direction * rockSpeed;
    }
}
