using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
public class PlayerControllerV2 : MonoBehaviour
{
    public Rigidbody playerRigidBody;
    [Header("Parameters")] 
    public float movementSpeed;
    public float rotationSpeed;
    public float horizontalMouseAxis;
    void Start()
    {
        StartPackage();
    }

    
    void FixedUpdate()
    {
      
      BodyMovementHandler();
        BodyRotationHandler();
    }

    void StartPackage()
    {
        horizontalMouseAxis = 0;
        playerRigidBody = GetComponent<Rigidbody>();
        
    }

    void BodyMovementHandler()
    {
        Vector3 movement = transform.forward * movementSpeed * Time.deltaTime;
        Vector3 lateralMovement = transform.right * movementSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.W))
        {
            playerRigidBody.MovePosition(playerRigidBody.position + movement);
        }
        if (Input.GetKey(KeyCode.S))
        {
            playerRigidBody.MovePosition(playerRigidBody.position - movement);
        }
        if (Input.GetKey(KeyCode.A))
        {
            playerRigidBody.MovePosition(playerRigidBody.position - lateralMovement);
        }
        if (Input.GetKey(KeyCode.D))
        {
            playerRigidBody.MovePosition(playerRigidBody.position + lateralMovement);
        }
        
    }

    void BodyRotationHandler()
    {
        horizontalMouseAxis += rotationSpeed * Input.GetAxis("Mouse X");
        transform.eulerAngles = new Vector3(0, horizontalMouseAxis, 0);
    }
   

}
