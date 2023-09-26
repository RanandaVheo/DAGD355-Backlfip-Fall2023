using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject playerRef;
    public float easeSpeed;

    private Vector3 newPos;
    private float camH, camHMax;
    private float permY, permZ;
    private bool isEasing = false;

    // Start is called before the first frame update
    void Start()
    {
        permY = transform.position.y;
        permZ = transform.position.z;
        camH = transform.position.y - playerRef.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (isEasing)
        {
            newPos = new Vector3(playerRef.transform.position.x, (playerRef.transform.position.y + camH), permZ);
            transform.position = Vector3.MoveTowards(transform.position, newPos, easeSpeed * Time.deltaTime);
        }
        
        if(Vector3.Distance(transform.position, playerRef.transform.position) <= permY * 3) isEasing = false;
        else isEasing = true;
    }
}
