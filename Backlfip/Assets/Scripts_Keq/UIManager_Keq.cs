using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager_Keq : MonoBehaviour
{
    public RectTransform[] QTE_RectHolder, QTE_TargetHolder;

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
            //we need to be running animations for the UI
            QuickTimeEventpopups(QTE_RectHolder);

            //we need to be checking for where the player is clicking
            QTEcollisionDetect(QTE_TargetHolder, QTE_RectHolder);

        }
        //else if we are not fishing and the QTE icons are still on the screen, 
        else if (!managerRef.isFishing && QTE_TargetHolder[0] != null) 
        {
            //we need to delete them
            destroyQTEs();
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

    private void QuickTimeEventpopups(RectTransform[] QTEpopups)
    {
        //for each circle,
        for (int i = 0; i < QTEpopups.Length; i++)
        {
            //checks if the correct popups are already onscreen, spawns them if not
            if(QTEpopups[i] == null) spawnQTEs(i);

            if (QTEpopups[i].sizeDelta.x > 500 || QTEpopups[i].sizeDelta.x < -100 && QTEpopups[i] != null) continue;
            //if the vector is here, it's not visible anymore and should stop scaling. This is a safety measure leftover from bug testing, when the Vector2 gets crazy numbers, Unity crashes lol
            //we also don't want to try to scale any rings that haven't even spawned yet

            Vector2 newSize = QTEsizeDelta;

            //newSize will become the new circle's size
            //sizeDelta needs to be changed as a whole Vector2, it won't allow you to change x and y individually
            newSize.x = QTEpopups[i].sizeDelta.x - QTEsizeDelta.x;
            newSize.y = QTEpopups[i].sizeDelta.y - QTEsizeDelta.y;
            QTEpopups[i].sizeDelta = newSize;
        }
    }

    private void QTEcollisionDetect(RectTransform[] QTEtargets, RectTransform[] QTErings)
    {
        //print(QTEtargets[0].position);
        //print("mouse position is: " + Input.mousePosition);

        for(int i = 0; i < QTEtargets.Length; i++) 
        {
            //if the player is clicking this circle
            if(Vector3.Distance(Input.mousePosition, QTEtargets[i].position) <= (QTEtargets[0].rect.width / 2))
            {
                //check for how wide the rings are

                //if the ring is around the same size as the target, the player correctly clicked the target
                //if not, end fishing game

            }
        }
    }

    private void spawnQTEs(int whichQTE)
    {
        if ()
        {
            Vector3 thisGuyPos = new Vector3(Random.Range(50, Screen.width - 50), Random.Range(50, Screen.height - 50), 0f);
            GameObject newQTE = Instantiate(QTEprefab, thisGuyPos, Quaternion.identity, transform);

            QTE_RectHolder[i] = newQTE.GetComponentInChildren<RectTransform>();
        }

        GameObject[] objectArray = GameObject.FindGameObjectsWithTag("QTE UI target");

        for (int i = 0;i < managerRef.howManyQTE; i++)
        {
            QTE_TargetHolder[i] = objectArray[i].GetComponent<RectTransform>();
        }
    }

    private void destroyQTEs()
    {
        GameObject[] choppingBlock = GameObject.FindGameObjectsWithTag("QTE object");

        for (int i = 0; i < managerRef.howManyQTE; i++)
        {
            Destroy(choppingBlock[i]);
        }
    }
}
