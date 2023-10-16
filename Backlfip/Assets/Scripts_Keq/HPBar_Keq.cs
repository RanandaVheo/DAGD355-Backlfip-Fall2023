using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar_Keq : MonoBehaviour
{
    public GameManager_Keq managerRef;
    public int playerHP = 10;
    public int playerHPMax = 10;

    private RectTransform HPBarRect;
    private int playerRatio;
    private float HP_WIDTH_START = 1f; //this will store the starting sizeDelta for math reasons
    private float HP_WIDTH_INC = 1f; //this will store math for how wide one section of HP bar should be
    private Vector2 barScale = new Vector2(1f, 1f); //we need a Vector2 to adjust the sizeDelta, can't just change sizeDelta.x
    

    // Start is called before the first frame update
    void Start()
    {
        HPBarRect = GetComponent<RectTransform>();

        playerHP = playerHPMax;

        //these are for the HP bar's math
        barScale = HPBarRect.sizeDelta;
        HP_WIDTH_START = barScale.x;
        HP_WIDTH_INC = (HPBarRect.rect.width + HP_WIDTH_START) / playerHPMax;
    }

    // Update is called once per frame
    void Update()
    {

        if (playerHP <= 0) managerRef.GameLost();

    }

    public void changeHPBar(bool badOrGood, int amount) 
    {
        //we should only change health if it's 0 or above. No negative hp
        if (playerHP >= 0)
        {
            //how many hp incriments we need, based on current player HP
            playerRatio = (playerHPMax - playerHP);

            //calculates width based on how wide the ui is, minus the amount of damage the player has taken
            barScale.x = HP_WIDTH_START - (HP_WIDTH_INC * playerRatio);

            //sets the scale
            HPBarRect.sizeDelta = barScale;
        }
        else return;
    }
}
