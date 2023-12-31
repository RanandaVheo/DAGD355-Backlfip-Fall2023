using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Keq : MonoBehaviour
{
    public bool isFishing = false; //false = not fishing, true = fishing right now
    public bool fishingWin = false; //will be true, to signal to other scripts a success
    public bool canFish = true; //to prevent endless QTE events while underwater
    public int howManyQTE = 3; //how many QTE circles do we want to popup during minigames
    public float fishingTimer;
    public float fishingTimerMax = 4f;
    public float QTEdelayTimer = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        fishingTimer = fishingTimerMax;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
