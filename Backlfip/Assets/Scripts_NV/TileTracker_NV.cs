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
        tilesCaptured += 1;
    }

    public void tileLost()
    {
        tilesCaptured -= 1;
    }
}
