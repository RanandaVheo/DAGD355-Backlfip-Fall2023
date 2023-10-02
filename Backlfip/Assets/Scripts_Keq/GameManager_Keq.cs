using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Keq : MonoBehaviour
{
    public bool isGameOver = false;
    public bool winOrLose = false; //false = win, true = lose

    public bool isFishing = false; //false = not fishing, true = fishing right now
    public int howManyQTE = 3; //how many QTE circles do we want to popup during minigames
    public float fishingTimer;
    public float fishingTimerMax = 4f;

    public int scoreTally = 0;
    public int WinCondition = 5;

    public int playerHP = 10;
    public int playerHPMax = 10;

    //public GameObject QTEprefab;



    // Start is called before the first frame update
    void Start()
    {
        playerHP = playerHPMax;
        fishingTimer = fishingTimerMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHP < 0) winOrLose = true;

        if (!isGameOver)
        {
            if (isFishing) fishingTimer -= Time.deltaTime;
            else if (fishingTimer != fishingTimerMax) fishingTimer = fishingTimerMax;
        }

        if (scoreTally >= WinCondition || winOrLose) isGameOver = true;
    }
}
