using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempBar_NV : MonoBehaviour
{
    Slider tempBar;

    private void Start()
    {
        tempBar = GetComponent<Slider>();
    }
    public void SetMaxTemp(float maxTemp)
    {
        tempBar.maxValue = maxTemp;
        tempBar.value = maxTemp;
    }

    public void SetTemp(float temp)
    {
        tempBar.value = temp;
    }

}
