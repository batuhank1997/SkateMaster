﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject ragdoll;
    public Animator cameraAnim;
    public Animator skateboardAnim;
    public GameObject restartUI;
    public GameObject replayObj;
    public Camera mainCam;
    public GameObject model;

    public GameObject[] awesomeObjs;

    private bool QTESucceed;
    private bool inClick;
    private bool inSwipeU;
    private bool inSwipeL;
    private bool inSwipeR;
    int i = 0;
    private bool click, swipeLeft, swipeRight, swipeUp, swipeDown, isDraging;
    private Vector2 startTouch, swipeDelta;

    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeUp { get { return swipeUp; } }

    public GameObject clickQTE;
    public GameObject swipeRQTE;
    public GameObject swipeLQTE;
    public GameObject swipeUQTE;
    public GameObject finishObj;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.I.player = gameObject;
        //awesomeObjs = new GameObject[6];
        rb = GetComponent<Rigidbody>();
    }
    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDraging = false;
    }
    // Update is called once per frame
    void Update()
    {

        click = swipeLeft = swipeRight = swipeUp = false;

        #region Inputs

        if (Input.GetMouseButtonDown(0))
        {
            isDraging = true;
            click = true;
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDraging = false;
            Reset();
        }
        #endregion

        //calculating distance
        swipeDelta = Vector2.zero;
        if(isDraging)
        {
            if (Input.touches.Length > 0)
                swipeDelta = Input.touches[0].position - startTouch;
            else if (Input.GetMouseButton(0))
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
        }
        //crossing deadzone
        if(swipeDelta.magnitude > 100)
        {
            //direction detect
            float x = swipeDelta.x;
            float y = swipeDelta.y;
            if(Mathf.Abs(x) > Mathf.Abs(y))
            {
                //left or right
                if (x < 0)
                    swipeLeft = true;
                else
                    swipeRight = true;
            }
            else
            {
                //up or down
                if (y < 0)
                    swipeDown = true;
                else
                    swipeUp = true;
            }
        }

        if (inClick == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Clicked");
                Time.timeScale = 1;
                inClick = false;
                QTESucceed = true;
                clickQTE.SetActive(false);
                AwesomeUI();

            }
        } 
        else if (inSwipeU == true)
        {

            if (swipeUp)
            {
                Debug.Log("Upped");
                Time.timeScale = 1;
                inSwipeU = false;
                QTESucceed = true;
                swipeUQTE.SetActive(false);
                AwesomeUI();

            }
        } 
        else if (inSwipeL == true)
        {
            if (swipeLeft)
            {
                Debug.Log("Left");
                Time.timeScale = 1;
                inSwipeL = false;
                QTESucceed = true;
                swipeLQTE.SetActive(false);
                AwesomeUI();

            }
        } 
        else if (inSwipeR == true)
        {
            if (swipeRight)
            {
                Debug.Log("Right");
                Time.timeScale = 1;
                inSwipeR = false;
                QTESucceed = true;
                swipeRQTE.SetActive(false);
                AwesomeUI();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Click")
        {
            QTESucceed = false;
            Time.timeScale = 4 * Time.fixedDeltaTime;
            inClick = true;
            clickQTE.SetActive(true);

        } 
        else if (other.gameObject.tag == "SwipeLeft")
        {
            QTESucceed = false;
            Time.timeScale = 4 * Time.fixedDeltaTime;
            inSwipeL = true;
            swipeLQTE.SetActive(true);

        } 
        else if (other.gameObject.tag == "SwipeRight")
        {
            QTESucceed = false;
            Time.timeScale = 4 * Time.fixedDeltaTime;
            inSwipeR = true;
            swipeRQTE.SetActive(true);

        } 
        else if (other.gameObject.tag == "SwipeUp")
        {
            QTESucceed = false;
            Time.timeScale = 4 * Time.fixedDeltaTime;
            inSwipeU = true;
            swipeUQTE.SetActive(true);

        }
        else if (other.gameObject.tag == "FinishLine")
        {
            i += 1;
            if(i == 2)
            {
                finishObj.SetActive(true);
                StartCoroutine(Wait());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Click" || other.gameObject.tag == "SwipeUp" || other.gameObject.tag == "SwipeRight" || other.gameObject.tag == "SwipeLeft")
        {
            Time.timeScale = 1;

            inSwipeU = false;
            inClick = false;
            inSwipeL = false;
            inSwipeR = false;

            if (QTESucceed == false)
            {
                GameManager.I.timeline.Stop();

                ragdoll.GetComponent<Animator>().enabled = false;
                //cameraAnim.Play("DeadLol");
                skateboardAnim.Play("SkateboardDead");
                mainCam.GetComponent<Animator>().enabled = false;
                mainCam.GetComponent<CameraFollow>().enabled = true;

                MoveToLayer(model.transform, 0);

                clickQTE.SetActive(false);
                swipeRQTE.SetActive(false);
                swipeLQTE.SetActive(false);
                swipeUQTE.SetActive(false);

                StartCoroutine(Restart());
            }
            
                
            Destroy(other.gameObject);
        }
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(2);
        restartUI.SetActive(true);
    }
    public void MoveToLayer(Transform root, int layer)
    {
        root.gameObject.layer = 0;
        foreach (Transform child in root)
            MoveToLayer(child, 0);
    }
    void AwesomeUI()
    {
        int random = Random.Range(0, 5);
        awesomeObjs[random].SetActive(true);
        StartCoroutine(Wait(awesomeObjs[random]));
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.8f);

        replayObj.SetActive(true);
    }
    IEnumerator Wait(GameObject obj)
    {
        yield return new WaitForSeconds(1);

        obj.SetActive(false);
    }
    public static Vector3 GetWorldPos(Vector2 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);

        float t = -ray.origin.y / ray.direction.y;

        return -ray.GetPoint(t);
    }
}
