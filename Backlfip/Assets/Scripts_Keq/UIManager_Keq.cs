using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager_Keq : MonoBehaviour
{
    public GameManager_Keq managerRef;
    public GameObject QTEprefab;
    public AudioSource ClickSound;
    public float QTEspeed = 5f;

    private RectTransform[] QTE_RectHolder;
    private Image[] QTE_ImageHolder;
    private Vector2 QTEsizeDelta;
    private float QTEcolorDelta;
    private int QTEcount = 0;
    private float spawnTimer = 0;
    private bool prevFPressed = false;
    private float ringMin = 18;
    private float ringMax = 28;



    void Start()
    {
        QTE_RectHolder = new RectTransform[managerRef.howManyQTE * 2]; //Array to easily pass around all QTE rects when needed
        QTE_ImageHolder = new Image[managerRef.howManyQTE * 2]; //Array to easily pass around all QTE targets when needed

        float changedSize = Time.deltaTime * QTEspeed;
        QTEsizeDelta = new Vector2(changedSize, changedSize);
        QTEcolorDelta = Time.deltaTime / 4;
    }

    
    void Update()
    {
        //if we are fishing
        if (managerRef.isFishing)
        {
            //run the spawn timer/spawn QTEs as needed
            if(QTEcount < managerRef.howManyQTE) spawnQTEs();

            //we need to be running animations for the UI
            QuickTimeEventpopups();

            //we need to be checking for where the player is clicking
            if(Input.GetKeyDown(KeyCode.F) && !prevFPressed) QTEcollisionDetect();

        }
        //else if we are not fishing and the QTE icons are still on the screen, 
        else if (!managerRef.isFishing) 
        {
            //delete them
            destroyQTE();

            //refresh timer for next time we fish
            spawnTimer = 0;
        }

        prevFPressed = Input.GetKeyDown(KeyCode.F);
    }

    //this function animates the ring around QTE targets
    private void QuickTimeEventpopups()
    {
        //for each circle,
        for (int i = 0; i < QTEcount; i++)
        {
            //if the vector is here, the ring is not visible anymore and should stop scaling/player didn't click in time
            if (QTE_RectHolder[i].sizeDelta.x > 500 || QTE_RectHolder[i].sizeDelta.x < (QTE_RectHolder[i + 1].rect.width * .15))
            {
                managerRef.isFishing = false;
                break; 
            }

            //makes and sets the new color for the ring. Color is on a scale of 0-1 for RGBA
            Color newColor = QTE_ImageHolder[i].color;
            if (newColor.g >= 1)
            {
                newColor.r -= QTEcolorDelta;
            }
            else if (newColor.r >= 0)
            {
                newColor.g += QTEcolorDelta;
            }

            for (int c = 0; c < 2; c++)
            {
                QTE_ImageHolder[i + c].color = newColor;
            }

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
            if(Vector3.Distance(Input.mousePosition, QTE_RectHolder[i + 1].position) <= (QTE_RectHolder[i + 1].rect.width / 2))
            {
                //if the player timed their click correctly, add to points. If not, then they failed fishing
                if (ringMin <= QTE_RectHolder[i].rect.width && QTE_RectHolder[i].rect.width <= ringMax)
                {
                    managerRef.fishingWin = true;
                    ClickSound.Play();
                }
                
                managerRef.isFishing = false;
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

            //ring = 0, target = 1
            for (int i = 0; i < 2; i++)
            {
                //pass the ring/target transform for animation/clicking purposes
                QTE_RectHolder[QTEcount + i] = newQTE.GetComponentsInChildren<RectTransform>()[i];
                QTE_ImageHolder[QTEcount + i] = newQTE.GetComponentsInChildren<Image>()[i];
            }

            spawnTimer = managerRef.QTEdelayTimer;
            QTEcount++;
        }

        //timer for a delay between QTE target spawns, so they don't appear all at once
        spawnTimer -= Time.deltaTime;
    }

    private void destroyQTE() //overloaded: no parameters = destroy all
    {
        //can't destroy anything if there's nothing to destroy
        if (GameObject.FindGameObjectWithTag("QTE object") == null) return;

        GameObject[] choppingBlock = GameObject.FindGameObjectsWithTag("QTE object");
        for (int i = 0; i < managerRef.howManyQTE; i++)
        {
            Destroy(choppingBlock[i]);
            QTEcount--;
        }
    }

    private void destroyQTE(int QTEindex) //overloaded: specify which QTE target to destroy
    {
        //can't destroy anything if there's nothing to destroy
        if (GameObject.FindGameObjectWithTag("QTE object") == null) return;

        GameObject[] choppingBlock = GameObject.FindGameObjectsWithTag("QTE object");
        Destroy(choppingBlock[QTEindex]);
        QTEcount--;
    }
}
