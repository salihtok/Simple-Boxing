using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class ProperPunch : MonoBehaviour
{
    [Header("Booleans")] 
    public bool canJab;
    public bool canCross;
    public bool canGuard;
    public bool isStancing;
    public bool canPunch;
    [Header("Punch Points")] 
    public GameObject jabR;
    public GameObject jabL;
    public GameObject crossR;
    public GameObject crossL;
    public GameObject guardR;
    public GameObject guardL;
    public GameObject stanceR;
    public GameObject stanceL;
    public GameObject punchR;
    public GameObject punchL;
    [Header("Parameters")]
    public float timePassed;// a counter that starts when punch activated.
    public float punchDuration; // how long it take throw a punch.
    public float pingPongTime;// a value for pingpong function;
    public float pingPAdjuster;// value that self increments.
    public float pPSpeed;// for self increment regulation.
    public float guardHandler; // for guard lerp operation.
    public float guardLerpAdj;
    public float standHandler;
    public float standLerpAdj;

    public float punchType;
    /*[Header("Vectors")] 
    public Vector3 rightPunch;
    public Vector3 leftPunch;
    public Vector3 rightJab;
    public Vector3 leftJab;
    public Vector3 rightCross;
    public Vector3 leftCross;
    public Vector3 rightGuard;
    public Vector3 leftGuard;
    public Vector3 rightStance;
    public Vector3 leftStance;
    public Vector3 rightActualPunch;
    public Vector3 leftActualPunch;*/  // they are useless.
    
    
    void Start()
    {
        canCross = false;
        canGuard = false;
        canJab = false;
        punchR.transform.position = stanceR.transform.position; // on start player shifts stance;
        punchL.transform.position = stanceL.transform.position;
    }

    
   
    void FixedUpdate()
    {
       PunchHandler();
       PunchTimeHandler();
        InputHandler();
       
    }
    void InputHandler()
    {
        if (Input.GetMouseButtonDown(0))
        {
            canJab = true;
            canPunch = true;
            isStancing = false;
        }

        if (Input.GetMouseButtonDown(1))
        {
            canCross = true;
            canPunch = true;
            isStancing = false;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            canGuard = true;
            canCross = false;
            canJab = false;
            canPunch = false;
            isStancing = false;
        }
        else
        {
           canGuard = false; 
        }
        
    }

    void PunchHandler()
    {
        if (canJab)
        {
            pingPAdjuster += Time.deltaTime;
            pingPongTime = Mathf.PingPong(pingPAdjuster * pPSpeed, 1);
            punchR.transform.position = Vector3.Lerp(stanceR.transform.position, jabR.transform.position, pingPongTime);
            punchL.transform.position =  Vector3.Lerp(stanceL.transform.position, jabL.transform.position, pingPongTime);

        }
        if (canCross)
        {
            pingPAdjuster += Time.deltaTime;
            pingPongTime = Mathf.PingPong(pingPAdjuster * pPSpeed, 1);
            punchR.transform.position = Vector3.Lerp(stanceR.transform.position, crossR.transform.position, pingPongTime);
            punchL.transform.position =  Vector3.Lerp(stanceL.transform.position, crossL.transform.position, pingPongTime);
        }

        if (canGuard)
        {
            standLerpAdj = 0;
            guardLerpAdj += Time.deltaTime * 4; // can add a parameter that controls speed of all actions.
            if (guardLerpAdj >= 1)
                guardLerpAdj = 1;
            guardHandler = Mathf.Lerp(0, 1, guardLerpAdj);
            punchR.transform.position = Vector3.Lerp(stanceR.transform.position, guardR.transform.position, guardHandler);
            punchL.transform.position =  Vector3.Lerp(stanceL.transform.position, guardL.transform.position, guardHandler);
        }

        if (!canCross && !canGuard && !canJab)
        {
            guardLerpAdj = 0;
            standLerpAdj += Time.deltaTime;
            if (standLerpAdj >= 1)
                standLerpAdj = 1;
            standHandler = Mathf.Lerp(0, 1, standLerpAdj);
            punchR.transform.position = Vector3.Lerp(punchR.transform.position, stanceR.transform.position, standHandler);
            punchL.transform.position =  Vector3.Lerp(punchL.transform.position, stanceL.transform.position, standHandler);
            isStancing = true;
            pingPAdjuster = 0;
        }
        
        
    }
 
    
    void PunchTimeHandler()
    {
        if (canPunch)
        {
            timePassed += Time.deltaTime;
            if (timePassed > punchDuration) // when time passed reach duration value, goes stance.
            {
                canPunch = false;
                canJab = false;
                canCross = false;
                pingPongTime = 0;
                timePassed = 0;
            }
        }
    }
}
