using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownUI_NV : MonoBehaviour
{
    public Image fireballCooldown;
    public Image lavaPuddleCooldown;
    public float fireballTime = 2;
    public float lavaTime = 5;
    bool fireCooldown = false;
    bool lavaCooldown = false;
    public KeyCode fireballKey;
    public KeyCode lavaKey;

    // Start is called before the first frame update
    void Start()
    {
        fireballCooldown.fillAmount = 0;
        lavaPuddleCooldown.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        FireballUI();
        LavaUI();
    }

    void FireballUI()
    {
        if (Input.GetKey(fireballKey) && fireCooldown == false)
        {
            fireCooldown = true;
            fireballCooldown.fillAmount = 1;
        }
        if (fireCooldown)
        {
            fireballCooldown.fillAmount -= 1 / fireballTime * Time.deltaTime;

            if(fireballCooldown.fillAmount <= 0)
            {
                fireballCooldown.fillAmount = 1;
                fireCooldown = false;
            }
        }
    }

    void LavaUI()
    {
        if (Input.GetKey(lavaKey) && lavaCooldown == false)
        {
            lavaCooldown = true;
            lavaPuddleCooldown.fillAmount = 1;
        }
        if (lavaCooldown)
        {
            lavaPuddleCooldown.fillAmount -= 1 / lavaTime * Time.deltaTime;

            if (lavaPuddleCooldown.fillAmount <= 0)
            {
                lavaPuddleCooldown.fillAmount = 1;
                lavaCooldown = false;
            }
        }
    }
}
