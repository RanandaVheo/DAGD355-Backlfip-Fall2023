using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public RectTransform[] QTE_RectHolder;
    public GameObject[] QTE_TargetHolder;

    public GameManager managerRef;
    public TMPro.TextMeshProUGUI scoreRef;
    public RectTransform HPbar;
    public GameObject loseRef;
    public GameObject winRef;
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
        QTE_TargetHolder = new GameObject[managerRef.howManyQTE]; //Array to easily pass around all QTE targets when needed

        for (int i = 0; i < managerRef.howManyQTE; i++)
        {
            Vector3 thisGuyPos = new Vector3(Random.Range(50, Screen.width - 50), Random.Range(50, Screen.height - 50), 0f);
            GameObject newQTE = Instantiate(QTEprefab, thisGuyPos, Quaternion.identity, transform);
            QTE_RectHolder[i] = newQTE.GetComponentInChildren<RectTransform>();
        }

        QTE_TargetHolder = GameObject.FindGameObjectsWithTag("QTE UI target");

        float changedSize = Time.deltaTime * QTEspeed;
        QTEsizeDelta = new Vector2(changedSize, changedSize);
    }

    
    void Update()
    {

        playerHealthBar(managerRef.playerHP);

        if (managerRef.isFishing)
        {
            QuickTimeEventpopups(QTE_RectHolder);
            QTEcollisionDetect(QTE_TargetHolder);

        }

        //if the score changes, update the text to reflect that
        if (managerRef.scoreTally != prevScoreTally) scoreRef.text = BasicScoreText + managerRef.scoreTally;

        //if the game is over, check if we won or lost
        if (managerRef.isGameOver) 
        {
            //false = win, true = lose
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
        //checks if the popups are already onscreen, there is no need to change active if it is already active
        if (QTEpopups[0].gameObject.activeSelf == false)
        {
            for (int i = 0; i < QTEpopups.Length; i++)
            {
                QTEpopups[i].gameObject.SetActive(true);
            }
        }

        //for each circle,
        for (int i = 0; i < QTEpopups.Length; i++)
        {
            if (QTEpopups[i].sizeDelta.x > 5000 || QTEpopups[i].sizeDelta.x < -1000) break;
            //if the vector is here, it's not visible anymore and should stop calculating
            //this is a safety measure, when the Vector2 gets crazy numbers, you crash lol

            Vector2 newSize = QTEsizeDelta;

            //newSize will become the new circle's size
            //sizeDelta needs to be changed as a whole Vector2, it won't allow you to change x and y individually
            newSize.x = QTEpopups[i].sizeDelta.x - QTEsizeDelta.x;
            newSize.y = QTEpopups[i].sizeDelta.y - QTEsizeDelta.y;
            QTEpopups[i].sizeDelta = newSize;
        }
    }

    private void QTEcollisionDetect(GameObject[] QTEpopups)
    {

    }
}
