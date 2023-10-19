using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTemp
{
    float currentTemp;
    float currentMaxTemp;

    public float Temp
    {
        get
        {
            return currentTemp;
        }
        set
        {
            currentTemp = value;
        }
    }

    public float MaxTemp
    {
        get
        {
            return currentMaxTemp;
        }
        set
        {
            currentMaxTemp = value;
        }
    }

    public PlayerTemp(float temp, float maxTemp)
    {
        currentTemp = temp;
        currentMaxTemp = maxTemp;
    }

    public void DamageUnit(float damageAmount)
    {
        if (currentTemp > 0)
        {
            currentTemp -= damageAmount;
        }
    }

    public void HealUnit(float healAmount)
    {
        if (currentTemp < currentMaxTemp)
        {
            currentTemp += healAmount;
        }
        if (currentTemp > currentMaxTemp)
        {
            currentTemp = currentMaxTemp;
        }
    }
}
