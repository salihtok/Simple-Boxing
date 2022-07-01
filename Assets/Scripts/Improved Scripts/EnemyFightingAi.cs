using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFightingAi : MonoBehaviour
{
    [Header("Punch Waypoints")] 
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
    public int random; //randomness for cross punch.

    public int crossProbability;
    public int guardProbability;
    public int realCrossProbability;
    public int realGuardProbability;
    public float timePassed;// a counter that starts when punch activated.
    public float punchDuration; // how long it take throw a punch.
    public float pingPongTime;// a value for pingpong function;
    public float pingPAdjuster;// value that self increments.
    public float pPSpeed;// for self increment regulation.
    public float guardHandler; // for guard lerp operation.
    public float guardLerpAdj;
    public float standHandler;
    public float standLerpAdj;
    [Header(("Fight Parameters"))]
    public float aggressionRate;
    public float defensiveRate;
    public int theLevel; // enemy boxer level.
    [Header("Other Scripts")] 
    public ProperPunch playerPunchingScript;

    public FightRates playerFightRates;// the script that determines how player aggressive or defensive.
    public EnemyAiV2 enemyAiScript;

    [Header("Booleans")] public bool playerPunching;
    public bool playerGuarding;
    public bool strikeZone;
    public bool jab;
    public bool cross;
    public bool guard;
    public bool punching;
    
    void Start()
    {
        aggressionRate = 0;
        defensiveRate = 0;
    }

    void StartStuff()
    {
        
    }

    void FixedUpdate()
    {
        ScriptCommunication();
        PunchingLogic();
        GuardLogic();
        PunchTimeHandler();
        DistanceHandler();
        Punches();
        PlayerFightRateHandler();
        Level();
        LevelAndBehaviorHandler();
        EnemyAggressionAndDefensive();
    }

    void PlayerFightRateHandler()// determines fight rates for player.
    {
        aggressionRate = playerFightRates.aggressionRate;
        defensiveRate = playerFightRates.defensiveRate;
    }

    void DistanceHandler() // checks whether player in striking zone or not.
    {
        if (enemyAiScript.distance <= enemyAiScript.determinedDistance)
        {
            strikeZone = true;
        }
        else
        {
            strikeZone = false;
        }
    }

    void PunchingLogic()
    {
        
       
        if (strikeZone && playerGuarding && !playerPunching) // if player guarding enemy throws jab always.
        {
            jab = true;
            cross = false;
            punching = true;
            guard = false;
        }
        else if (strikeZone && !playerGuarding && ProbabilityCheck(crossProbability)) // if player not holds guard and probability is true a strong cross comes up.
        {
            cross = true;
            jab = false;
            punching = true;
            guard = false;
        }
       
        
    }

    void GuardLogic()
    {
        if (strikeZone && playerPunching && ProbabilityCheck(guardProbability)) // if player in strike zone also player punching and probability is true, then enemy holds guard.
        {
            guard = true;
            cross = false;
            jab = false;
            punching = false;
        }
       
    }

    void Level()
    {
        theLevel = PlayerPrefs.GetInt("levelNumber");
    }

    void ScriptCommunication() // checks what player is doing.
    {
        playerPunching = playerPunchingScript.canPunch;
        playerGuarding = playerPunchingScript.canGuard;
    }

    void Punches() // punches work with linear interpolation function.
    {
        if (jab)
        {
            pingPAdjuster += Time.deltaTime;
            pingPongTime = Mathf.PingPong(pingPAdjuster * pPSpeed, 1);
            punchR.transform.position = Vector3.Lerp(stanceR.transform.position, jabR.transform.position, pingPongTime);
            punchL.transform.position =  Vector3.Lerp(stanceL.transform.position, jabL.transform.position, pingPongTime);
        }
        if (cross)
        {
            pingPAdjuster += Time.deltaTime;
            pingPongTime = Mathf.PingPong(pingPAdjuster * pPSpeed, 1);
            punchR.transform.position = Vector3.Lerp(stanceR.transform.position, crossR.transform.position, pingPongTime);
            punchL.transform.position =  Vector3.Lerp(stanceL.transform.position, crossL.transform.position, pingPongTime);
        }
        if (guard)
        {
            standLerpAdj = 0;
            guardLerpAdj += Time.deltaTime * 4; // can add a parameter that controls speed of all actions.
            if (guardLerpAdj >= 1)
                guardLerpAdj = 1;
            guardHandler = Mathf.Lerp(0, 1, guardLerpAdj);
            punchR.transform.position = Vector3.Lerp(stanceR.transform.position, guardR.transform.position, guardHandler);
            punchL.transform.position =  Vector3.Lerp(stanceL.transform.position, guardL.transform.position, guardHandler);
        }

        if (!cross && !guard && !jab)
        {
            guardLerpAdj = 0;
            standLerpAdj += Time.deltaTime;
            if (standLerpAdj >= 1)
                standLerpAdj = 1;
            standHandler = Mathf.Lerp(0, 1, standLerpAdj);
            punchR.transform.position =
                Vector3.Lerp(punchR.transform.position, stanceR.transform.position, standHandler);
            punchL.transform.position =
                Vector3.Lerp(punchL.transform.position, stanceL.transform.position, standHandler);
            
            pingPAdjuster = 0;
        }
    }

    bool ProbabilityCheck(int probability) // i will try these with inside the void with update.
    {
        random = Random.Range(0, 100);
        if (random < probability)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void PunchTimeHandler() // regulates punching time.
    {
        if (punching)
        {
            timePassed += Time.deltaTime;
            if (timePassed > punchDuration)
            {
                cross = false;
                jab = false;
                punching = false;
                pingPongTime = 0;
                timePassed = 0;
            }
        }
    }

    void LevelAndBehaviorHandler()
    {
        switch (theLevel)
        {
            case 1:
                punchDuration = .39f;
                pPSpeed = 5;
                crossProbability = 1;
                guardProbability = 2;
                break;
            case 2:
                punchDuration = .27f;
                pPSpeed = 7;
                crossProbability = realCrossProbability;
                guardProbability = realGuardProbability;
                break;
            case 3:
                punchDuration = .19f;
                pPSpeed = 10;
                crossProbability = realCrossProbability;
                guardProbability = realGuardProbability;
                break;
        }
    }

    void EnemyAggressionAndDefensive()
    {
        if (theLevel == 2)
        {
            if (aggressionRate <= 1)
            {
                realGuardProbability = 2;
            }
            else if (aggressionRate >= 1.1f )
            {
                realGuardProbability = 6;
            }

            if (defensiveRate < .25f)
            {
                realCrossProbability = 3;
            }
            else if (defensiveRate >= .25f)
            {
                realCrossProbability = 5;
            }
            
        }

        if (theLevel == 3)
        {
            if (aggressionRate <= 1)
            {
                realGuardProbability = 5;
            }
            else if (aggressionRate >= 1.1f )
            {
                realGuardProbability = 9;
            }

            if (defensiveRate < .25f)
            {
                realCrossProbability = 4;
            }
            else if (defensiveRate >= .25f)
            {
                realCrossProbability = 6;
            }
        }
    }
}
