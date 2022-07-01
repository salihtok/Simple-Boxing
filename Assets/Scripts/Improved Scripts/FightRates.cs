using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class FightRates : MonoBehaviour
{
    public ProperPunch playerScript; // the script that we collect all parameters for usage.
    [Header("Rates and Parameters")] 
    public float elapsedTime; // time elapsed since fight started.

    public float playerPunches; // number of player punches that thrown from player.
    public float playerGuardTime;
    public float aggressionRate;
    public float defensiveRate;
    [Header(("Booleans"))] 
    public bool playerPunching;// whether player punching or not.

    public bool guarding; // whether player guarding or not.

    public bool playerGuarding;
    void Start()
    {
        StartStuff();
    }


    void FixedUpdate()
    {
        PlayerParameterHandler();
        AggressionRate();
        DefensiveRate();
        TimeHandler();
    }

    public void StartStuff()
    {
        playerPunches = 0;
        playerGuardTime = 0f;
        elapsedTime = 0.0f;
    }

    public void PlayerParameterHandler()
    {
        if (playerScript.canCross || playerScript.canJab)
        {
            playerPunches += (1 / Time.deltaTime) / 1000; // if player punches jab or cross, this parameter increase by 1.
        }

        if (playerScript.canGuard)
        {
            playerGuardTime += Time.deltaTime; // if player hold guard, the player guarding time will increase.
        }
    }

    public void AggressionRate() // how aggressive player is.
    {
        aggressionRate = playerPunches / elapsedTime;
    }

    public void DefensiveRate() // how defensive player is.
    {
        defensiveRate = playerGuardTime / elapsedTime;
    }

    public void TimeHandler()
    {
        elapsedTime += Time.deltaTime;
    }
}
