using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball_NV : MonoBehaviour
{

    public AudioSource source;
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
        target = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));

        Vector2 difference = target - player.transform.position;
        
        if (Input.GetMouseButtonDown(1))
        {
            // NEED TO ADD: CHECK IF PLAYER HAS ENOUGH FIRE JUICE
            float distance = difference.magnitude;
            Vector2 direction = difference / distance;
            direction.Normalize();
            fireball(direction);
            source.Play();
        }
 
    }

    void fireball(Vector2 direction)
    {
        GameObject f = Instantiate(fireballPrefab) as GameObject;
        f.transform.position = player.transform.position;
        f.GetComponent<Rigidbody2D>().velocity = direction * rockSpeed;
    }
}
