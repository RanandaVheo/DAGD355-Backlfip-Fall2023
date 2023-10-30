using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTracker_NV
{
    public float tilesCaptured = 15;

    void Update()
    {
        
    }


    public void tileCaptured()
    {
        tilesCaptured++;
    }

    public void tileLost()
    {
        tilesCaptured--;
    }
}
