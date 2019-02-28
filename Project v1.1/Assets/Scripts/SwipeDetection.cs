using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeDetection : MonoBehaviour
{

    private bool tap, doubleTap, hold, swipeLeft, swipeRight, swipeUp, swipeDown;
    private bool isDragging = false;
    private Vector2 startTouch, swipeDelta;

    private bool held = false;
    private float holdTime = 0.4f;
    private float startHoldTime;
    private float tapDelay = 0.5f;
    private float tapTime;
    public int tapCount = 0;

    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public bool SwipeLeft { get { return swipeLeft;  } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }
    public bool Tap { get { return tap; } }
    public bool DoubleTap { get { return doubleTap; } }
    public bool Hold { get { return hold; } }

    public float swipeRange;

    // Update is called once per frame
    private void Update()
    {
        //Reset detection
        tap = doubleTap = hold = swipeLeft = swipeRight = swipeUp = swipeDown = false;

         #region Mouse Inputs
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            startHoldTime = Time.time;
            tap = true;
            held = true;
            isDragging = true;
            startTouch = Input.mousePosition;
            tapCount++;
            if (tapCount == 1)
                tapTime = Time.deltaTime;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            Reset();
        }

        //Debug.Log(Time.time);

        if((Time.time - startHoldTime) > holdTime && held)
        {
            if ((Vector2)Input.mousePosition == startTouch)
            {
                hold = true;
                held = false;
            }
        }
        #endregion

        #region Mobile Inputs
        //if (Input.touches.Length > 0)
        //{
        //    if(Input.touches[0].phase == TouchPhase.Began)
        //    {k
        //        startHoldTime = Time.time;
        //        tap = true;
        //        held = true;
        //        isDragging = true;
        //        startTouch = Input.touches[0].position;
        //        tapCount++;
        //        if (tapCount == 1)
        //            tapTime = Input.touches[0].deltaTime;
        //    }
        //    else if(Input.touches[0].phase == TouchPhase.Stationary)
        //    {
        //        if ((Time.time - startHoldTime) > holdTime && held)
        //        {
        //            //if (Input.touches[0].position == startTouch)
        //            //{
        //                hold = true;
        //                held = false;
        //            //}
        //        }
        //    }
        //    else if(Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
        //    {
        //        isDragging = false;
        //        Reset();
        //    }

            //if ((Time.time - startHoldTime) > holdTime && held)
            //{
            //    if (Input.touches[0].position == startTouch)
            //    {
            //        hold = true;
            //        held = false;
            //    }
            //}
        //}
        #endregion

        if (tapCount == 2 && Time.deltaTime - tapTime < tapDelay)
        {
            tapCount = 0;
            tapTime = 0;
            doubleTap = true;
        }
        else if (tapCount > 2 || Time.deltaTime - tapTime > 1)
            tapCount = 0;


        swipeDelta = Vector2.zero;
        if(startTouch != Vector2.zero)
        {
            if(isDragging)
            {
                if (Input.touches.Length > 0)
                    swipeDelta = Input.touches[0].position - startTouch;
                else if (Input.GetMouseButton(0))
                    swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }

            if (swipeDelta.magnitude > swipeRange)
            {
                //Check swipe direction
                float x = swipeDelta.x;
                float y = swipeDelta.y;
                if(Mathf.Abs(x) > Mathf.Abs(y))
                {
                    //Left or RIght
                    if(x < 0)
                    {
                        swipeLeft = true;
                        //Debug.Log("Swipe left");
                    }
                    else
                    {
                        swipeRight = true;
                        //Debug.Log("Swipe right");
                    }
                }
                else
                {
                    //Up or Down
                    if (y < 0)
                    {
                        swipeDown = true;
                        //Debug.Log("Swipe down");
                    }
                    else
                    {
                        swipeUp = true;
                        //Debug.Log("Swipe up");
                    }
                }
                Reset();
            }
        }
    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
    }
}
