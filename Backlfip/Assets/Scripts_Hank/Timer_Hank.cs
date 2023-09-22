using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer_Hank
{
    float duration;
    public float timeLeft;
    public bool isDone = true;
    public bool autoRestart = false;
    bool paused = false;

    public Timer_Hank(float duration)
    {
        this.duration = duration;
        this.timeLeft = duration;
        isDone = false;
    }

    public void Update()
    {
        if (this.paused) return;

        if (timeLeft <= 0)
        {
            timeLeft = 0;
            isDone = true;
            if (autoRestart) Reset();
        }
        else
        {
            if (!isDone)
            {
                timeLeft -= Time.deltaTime;
            }
        }
    }

    public void Reset()
    {
        timeLeft = duration;
        isDone = false;
    }

    public void TogglePause()
    {
        this.paused = !this.paused;
    }
}


