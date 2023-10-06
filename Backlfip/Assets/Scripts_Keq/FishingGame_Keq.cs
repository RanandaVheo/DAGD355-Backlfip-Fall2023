using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FishingGame_Keq : MonoBehaviour
{
    private Vector3[] linePoints;

    public GameManager_Keq managerRef;

    public LineRenderer lineRef;
    public Transform poleRef;
    public Rigidbody2D bobberRef;

    private bool castingLine = false;
    private bool prevMousePressed = false;

    // Start is called before the first frame update
    void Start()
    {
        linePoints = new Vector3[2];
        linePoints[0] = poleRef.position;
        linePoints[1] = bobberRef.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //if we are fishing, we want the bobber to stop moving. If we aren't fishing, the bobber should move back to neutral position
        if (managerRef.isFishing)
        {
            bobberRef.velocity = Vector2.zero;
            bobberRef.angularVelocity = 0;
        }
        else if (castingLine == false && Vector3.Distance(bobberRef.transform.position, poleRef.position) > 4f && !managerRef.isFishing)
        {
            bobberRef.transform.position = Vector3.MoveTowards(bobberRef.transform.position, poleRef.position, 20 * Time.deltaTime);
        }


        linePoints[0] = poleRef.position;
        linePoints[1] = bobberRef.transform.position;

        lineRef.SetPositions(linePoints);

        if (Input.GetMouseButtonDown(0) && !prevMousePressed) clickToCast();

        prevMousePressed = Input.GetMouseButtonDown(0);

    }

    //Used for casting or reeling in the fishing pole bobber with clicks
    private void clickToCast()
    {
        if(castingLine && !managerRef.isFishing) castingLine = false; //if we already cast our line, reel it back in

        else if(!castingLine) //if we haven't cast our line yet, cast it out
        {
            Vector3 mouseVec = Camera.main.ScreenToWorldPoint(Input.mousePosition); //get where the player is pointing at
            mouseVec = mouseVec - bobberRef.transform.position; //calculate the direction of the click compared to the bobber's current location
            mouseVec = mouseVec.normalized; //normalize that vector
            mouseVec = new Vector3(mouseVec.x, .6f, 0f); //this makes the bobber shoot up a little, and towards the mouse
            bobberRef.AddForce(mouseVec * 75); //this sets the force it moves with
            castingLine = true; //after the above steps, the line has been cast
        }
    }

}
