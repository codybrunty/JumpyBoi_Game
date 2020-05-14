using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/*
<summary> The players jump direction and power controler. Also creates a line renderer of the players swipe
on screen showing the direction and color depending on how much power is used. 
*/

public class PlayerControler : MonoBehaviour{

    [Header("Jump Settings")]
    public bool touchEnabled = true;
    public float jumpPower;
    public Vector2 lineMinPower;
    public Vector2 lineMaxPower;
    private Vector2 ballForce;
    private Vector3 startPoint;
    private Vector3 endPoint;
    private bool clicked = false;

    [Header("Line Settings")]
    public Color startColor;
    public Color middleColor;
    public Color endColor;

    [Header("Jump Game Objects")]
    [SerializeField] GroundMechanic groundBox = default;
    [SerializeField] RewindMechanic rewindButton = default;
    [SerializeField] JumpCounter jumpCounter = default;
    private Camera mainCam;
    private Rigidbody2D rb;
    private LineRenderer line;


    private void Start() {
        mainCam = Camera.main;
        line = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody2D>();
        SetPlayerPositionFromPositionManager();
    }

    //Get saved player position from the Position Manager and set player to that position.
    private void SetPlayerPositionFromPositionManager() {
        gameObject.transform.position = PositionManager.PM.playerPosition;
    }


    private void Update() {
        if (touchEnabled) {
            CalculateJump();
        }
    }

    private void CalculateJump() {
        
        //Start jump calculation when user presses mouse and the user is grounded.
        if (Input.GetMouseButtonDown(0) && groundBox.grounded == true) {
            //Makes sure we arent clicking on UI buttons.
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            //Record the position of the mouse on first click.
            clicked = true;
            startPoint = mainCam.ScreenToWorldPoint(Input.mousePosition);
            startPoint.z = 0;
        }

        //As the mouse is still down we draw a line from the stat position to the current postion.
        if (Input.GetMouseButton(0) && clicked == true) {
            Vector3 currentPoint = mainCam.ScreenToWorldPoint(Input.mousePosition);
            currentPoint.z = 0;
            DrawLine(startPoint, currentPoint);
        }

        //When the mouse button is released we record the players position for the rewind button.
        //The Jump counter is incremented by one. The final position of the mouse is recorded on release 
        //for the calculation of the jump force and added to the players rigidbody. The line renderer is reset.
        if (Input.GetMouseButtonUp(0) && clicked == true) {

            rewindButton.SavePosition(gameObject.transform.position);
            jumpCounter.AddOneJump();

            endPoint = mainCam.ScreenToWorldPoint(Input.mousePosition);
            endPoint.z = 0;

            //Using the mouse point on start and end we calculate the direction and make sure the numbers are within the clamped min and max.
            ballForce = new Vector2(Mathf.Clamp(startPoint.x - endPoint.x, lineMinPower.x, lineMaxPower.x), Mathf.Clamp(startPoint.y - endPoint.y, lineMinPower.y, lineMaxPower.y));
            //Multiple this direction by the jump power and add to the players rigidbody as a force.
            rb.AddForce(ballForce * jumpPower, ForceMode2D.Impulse);
            EndLine();
            clicked = false;
        }
    }

    //Draw a line with the line renderer between two points
    public void DrawLine(Vector3 start, Vector3 end) {
        line.positionCount = 2;
        Vector3[] Allpoint = new Vector3[2];
        Allpoint[0] = start;
        Allpoint[1] = end;
        line.SetPositions(Allpoint);


        //Change the color of the lineRenderer depending on how far apart the points are giving the user
        //a visual indicator of how much power they are using.
        float mainDistance = Mathf.Max(start.x - end.x, start.y - end.y);
        mainDistance = Mathf.Clamp(mainDistance,lineMinPower.x, lineMaxPower.x);
        mainDistance = Mathf.Abs(mainDistance);

        //Lerp a Color between the StartColor, MiddleColor, EndColor to indicate power of jump being calculated.
        Color lerpedColor;
        //Distance between start and end is greater then half way of our max distance then we lerp between our middle and end color.
        if (mainDistance >= lineMaxPower.x / 2.0f) {
            lerpedColor = Color.Lerp(middleColor, endColor, (mainDistance - (lineMaxPower.x / 2.0f)) / (lineMaxPower.x / 2.0f));
        }
        //If distance between start and end is less then half way of our max then we lerp between our start and middle color.
        else {
            lerpedColor = Color.Lerp(startColor, middleColor, mainDistance / (lineMaxPower.x / 2.0f));
        }

        //Set the linerenderers gradient color to our lerped color.
        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(lerpedColor, 0.0f), new GradientColorKey(lerpedColor, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) });
        line.colorGradient = gradient;
    }

    //Reset line renderer.
    public void EndLine() {
        line.positionCount = 0;
    }
}
