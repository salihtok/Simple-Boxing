using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class EnemyAiV2 : MonoBehaviour
{
   
    [Header("Parameters")] 
    public float moveSpeed;// move speed to player.
    public float moveSpeedRegulator;//regulates to move speed.
    public float rotateSpeed;// rotation speed.
    public float distance;// distance between fighters.
    public float determinedDistance;// distance that we can arrange.
    public float newSpeedReg;//regulates to move speed. this one is valid.
    
   
    [Header("Game Objects")] 
    public GameObject playerBoxer;

  
    public ProperPunch playerPunchScript;
    public Rigidbody enemyRigidbody;
    [Header("Vectors")]
    public Vector3 targetDirection;
    public Vector3 faceToPlayer;
    public Vector3 goToPlayer;
    
    
    void Start()
    {
        StartStuff();
    }

    void StartStuff()
    {
        enemyRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //MoveAndRotationHandler();
        DistanceHandler();
        MoveAndRotationHandlerV2();
    }

    void MoveAndRotationHandler() //move and rotation to player.
    { 
        moveSpeed = moveSpeedRegulator * Time.deltaTime;
         targetDirection = playerBoxer.transform.position - transform.position;
         faceToPlayer = Vector3.RotateTowards(transform.forward, targetDirection, rotateSpeed, 0.0f);
        goToPlayer = Vector3.MoveTowards(transform.position, targetDirection, moveSpeed);

        
        enemyRigidbody.MovePosition(goToPlayer);
         enemyRigidbody.MoveRotation( Quaternion.LookRotation(faceToPlayer));
         // when i call this method enemy boxer slows down to almost stop while player around the corner. ı dont know why it happens.
         // it should be about move towards method.
    }

    void MoveAndRotationHandlerV2()
    {
        transform.LookAt(playerBoxer.transform);
        transform.position += transform.forward * Time.deltaTime * newSpeedReg;
    }

    void DistanceHandler()
    {
        distance = Vector3.Distance(transform.position, playerBoxer.transform.position);

        if (distance  > determinedDistance)
        {
            newSpeedReg = .8f * moveSpeedRegulator; // if distance is less than "striking zone" enemy goes to player.
        }

        

        if (distance <= determinedDistance)
        {
             newSpeedReg = -0.1f; //if distance is too close, enemy goes back.
        }

       
        
    }
    

    
   
}
