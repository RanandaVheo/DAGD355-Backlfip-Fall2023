using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaPuddleLifetime_NV : MonoBehaviour
{

    public float lavaPoolLifetime = 4f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(lifetime());
    }

    IEnumerator lifetime()
    {
        while (true)
        {
            yield return new WaitForSeconds(lavaPoolLifetime);
            Destroy(gameObject);
            StopCoroutine(lifetime());
        }
    }
}
