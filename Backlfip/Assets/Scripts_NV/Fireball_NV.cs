using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball_NV : MonoBehaviour
{
    [SerializeField] TempBar_NV tempBar;

    public AudioSource source;
    public GameObject player;
    public GameObject fireballPrefab;
    public Transform fireballTransform;
    private Rigidbody2D rb;
    private Camera mainCam;
    public float fireballCooldown = 2;
    public bool fireballOnCooldown = false;
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

        if (Input.GetMouseButtonDown(1) && fireballOnCooldown == false)
        {
            fireballOnCooldown = true;
            Instantiate(fireballPrefab, fireballTransform.position, Quaternion.identity);
            GameManager_NV.gameManagerNV.playerTemp.DamageUnit(10);
            tempBar.SetTemp(GameManager_NV.gameManagerNV.playerTemp.Temp);
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
}
