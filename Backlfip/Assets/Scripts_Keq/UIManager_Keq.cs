using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager_Keq : MonoBehaviour
{
    private RectTransform[] QTE_RectHolder, QTE_TargetHolder;

    public GameManager_Keq managerRef;
    public TMPro.TextMeshProUGUI scoreRef;
    public RectTransform HPbar;
    public GameObject loseRef, winRef;
    public GameObject QTEprefab;
    public string BasicScoreText = "Score: "; //the score text that is displayed all the time
    public float QTEspeed = 5f;

    private float HP_WIDTH_START = 1f; //this will store the starting sizeDelta for math reasons
    private float HP_WIDTH_INC = 1f; //this will store math for how wide one section of HP bar should be
    private Vector2 barScale = new Vector2(1f, 1f); //we need a Vector2 to adjust the sizeDelta, can't just change sizeDelta.x
    private int prevScoreTally = 0;
    private Vector2 QTEsizeDelta;
    private int QTEcount = 0;
    private float spawnTimer = 0;
    private bool prevMousePressed = false;
    private float ringMin = 18;
    private float ringMax = 28;



    void Start()
    {
        scoreRef.text = BasicScoreText + managerRef.scoreTally; //we start with 0 points but still need to show that

        //these are for the HP bar's math
        barScale = HPbar.sizeDelta;
        HP_WIDTH_START = barScale.x;
        HP_WIDTH_INC = (HPbar.rect.width + HP_WIDTH_START) / managerRef.playerHPMax;

        QTE_RectHolder = new RectTransform[managerRef.howManyQTE]; //Array to easily pass around all QTE rects when needed
        QTE_TargetHolder = new RectTransform[managerRef.howManyQTE]; //Array to easily pass around all QTE targets when needed

        float changedSize = Time.deltaTime * QTEspeed;
        QTEsizeDelta = new Vector2(changedSize, changedSize);
    }

    
    void Update()
    {

        playerHealthBar(managerRef.playerHP);

        //if we are fishing
        if (managerRef.isFishing)
        {
            //run the spawn timer/spawn QTEs as needed
            if(QTEcount < managerRef.howManyQTE) spawnQTEs();

            //we need to be running animations for the UI
            QuickTimeEventpopups();

            //we need to be checking for where the player is clicking
            if(Input.GetMouseButtonDown(0) && !prevMousePressed) QTEcollisionDetect();

        }
        //else if we are not fishing and the QTE icons are still on the screen, 
        else if (!managerRef.isFishing) 
        {
            //we need to delete them
            destroyQTE(true, QTEcount);

            //refresh timer for next time we fish
            spawnTimer = 0;
        }

        //if the score changes, update the text to reflect that
        if (managerRef.scoreTally != prevScoreTally) scoreRef.text = BasicScoreText + managerRef.scoreTally;

        //if the game is over, check if we won or lost
        if (managerRef.isGameOver) 
        {
            //true = lose, false = win
            if (managerRef.winOrLose) loseRef.SetActive(true);
            else if(!managerRef.winOrLose) winRef.SetActive(true);
        }

        prevScoreTally = managerRef.scoreTally;
        prevMousePressed = Input.GetMouseButtonDown(0);
    }

    //Used to update the health bar based on player HP
    private void playerHealthBar(int currHP)
    {
        //how many hp incriments we need, based on current player HP
        int playerRatio = (managerRef.playerHPMax - currHP);

        //if the player's HP is below 0 it's still just zero and the bar shouldn't get negative scale
        if (managerRef.playerHP >= 0) barScale.x = HP_WIDTH_START - (HP_WIDTH_INC * playerRatio);

        HPbar.sizeDelta = barScale; //set scale
    }

    //this function animates the ring around QTE targets
    private void QuickTimeEventpopups()
    {
        //for each circle,
        for (int i = 0; i < QTEcount; i++)
        {
            //if the vector is here, the ring is not visible anymore and should stop scaling/player didn't click in time
            if (QTE_RectHolder[i].sizeDelta.x > 500 || QTE_RectHolder[i].sizeDelta.x < (QTE_TargetHolder[i].rect.width * .15)) continue;

            //newSize will become the new circle's size
            Vector2 newSize = QTEsizeDelta;

            //sizeDelta needs to be changed as a whole Vector2, it won't allow you to change x and y individually
            newSize.x = QTE_RectHolder[i].sizeDelta.x - QTEsizeDelta.x;
            newSize.y = QTE_RectHolder[i].sizeDelta.y - QTEsizeDelta.y;
            QTE_RectHolder[i].sizeDelta = newSize;
        }
    }

    //this function will handle when the player clicks while fishing
    private void QTEcollisionDetect()
    {
        for(int i = 0; i < QTEcount; i++) 
        {
            //if the player is clicking this circle
            if(Vector3.Distance(Input.mousePosition, QTE_TargetHolder[i].position) <= (QTE_TargetHolder[i].rect.width / 2))
            {
                //if the player timed their click correctly, add to points. If not, then they failed fishing
                if (ringMin <= QTE_RectHolder[i].rect.width && QTE_RectHolder[i].rect.width <= ringMax)
                {
                    managerRef.scoreTally++;
                }
                else managerRef.isFishing = false;
            }
        }
    }

    //this function will handle spawning targets
    private void spawnQTEs()
    {
        if(spawnTimer <= 0)
        {
            //make a new target with a random location
            Vector3 thisGuyPos = new Vector3(Random.Range(50, Screen.width - 50), Random.Range(50, Screen.height - 50), 0f);
            GameObject newQTE = Instantiate(QTEprefab, thisGuyPos, Quaternion.identity, transform);

            //pass the ring/target transform for animation/clicking purposes
            QTE_RectHolder[QTEcount] = newQTE.GetComponentsInChildren<RectTransform>()[0];
            QTE_TargetHolder[QTEcount] = newQTE.GetComponentsInChildren<RectTransform>()[1];

            spawnTimer = managerRef.QTEdelayTimer;
            QTEcount++;
        }

        //timer for a delay between QTE target spawns (so they don't appear all at once)
        spawnTimer -= Time.deltaTime;
    }

    //this function will handle de-spawning all or one target(s)
    private void destroyQTE(bool AllOrOne, int whichOneorHowMany)
    {
        //can't destroy anything if there's nothing to destroy
        if (GameObject.FindGameObjectWithTag("QTE object") == null) return;


        GameObject[] choppingBlock = GameObject.FindGameObjectsWithTag("QTE object");

        if(AllOrOne)
        {
            for (int i = 0; i < whichOneorHowMany; i++)
            {
                Destroy(choppingBlock[i]);
                QTEcount--;
            }
        }
        else if(!AllOrOne) 
        {
            Destroy(choppingBlock[whichOneorHowMany]);
            QTEcount--;
        }
        
    }
}
