using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball_NV : MonoBehaviour
{

    public AudioSource source;
    public GameObject player;
    public GameObject fireballPrefab;
    public float fireballCooldown = 2;
    public bool fireballOnCooldown = false;
    public float fireballSpeed = 50f;
    public float lifetime = 2f;
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

        if (Input.GetMouseButtonDown(1) && fireballOnCooldown == false)
        {
            // NEED TO ADD: CHECK IF PLAYER HAS ENOUGH FIRE JUICE
            fireballOnCooldown = true;
            float distance = difference.magnitude;
            Vector2 direction = difference / distance;
            direction.Normalize();
            fireball(direction);
            source.Play();
        }
        if (fireballOnCooldown)
        {
            fireballCooldown -= 1 * Time.deltaTime;
            if (fireballCooldown <= 0)
            {
                fireballCooldown = 2;
                fireballOnCooldown = false;
            }
        }
    }

    void fireball(Vector2 direction)
    {
        GameObject f = Instantiate(fireballPrefab) as GameObject;
        f.transform.position = player.transform.position;
        f.GetComponent<Rigidbody2D>().velocity = direction * fireballSpeed;
        Destroy(f, lifetime);
    }
}
