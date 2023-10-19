using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockAttack_NV : MonoBehaviour
{

    public AudioSource source;
    public GameObject player;
    public GameObject rockPrefab;
    public Transform rockTransform;
    private Rigidbody2D rb;
    private Camera mainCam;
    private Vector3 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(rockPrefab, rockTransform.position, Quaternion.identity);
            source.Play();
        }
    }
}
