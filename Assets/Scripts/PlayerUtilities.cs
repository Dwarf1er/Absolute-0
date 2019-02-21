using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
public class PlayerUtilities : NetworkBehaviour
{
    //Constants
    Vector3 notMoving { get; set; } //used to verify if player is not moving and is the default state

    //References
    Vector3 playerVelocity { get; set; }
    Vector3 playerRotation { get; set; }
    Vector3 cameraRotation { get; set; }
    [SerializeField]
    Camera playerCamera;
    Rigidbody rigibody { get; set; }

    void Start()
    {
        notMoving = Vector3.zero;
        playerVelocity = notMoving;
        playerRotation = notMoving;
        cameraRotation = notMoving;
        rigibody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
                transform.Translate(Vector3.forward);

        if (Input.GetKey(KeyCode.A))
                transform.Translate(Vector3.left);

        if (Input.GetKey(KeyCode.S))
                transform.Translate(Vector3.back);


        if (Input.GetKey(KeyCode.D))
                transform.Translate(Vector3.right);  
    }

    //As recommended by Unity for physics calculations, using FixedUpdate instead of Update
    void FixedUpdate()
    {
        ExecutePlayerMovement();
        ExecutePlayerRotation();
        ExecuteCameraRotation();
    }

    //Receives finalMovement from PlayerController and outputs it as the playerVelocity
    public void SetPlayerVelocity (Vector3 playerVelocityReceived)
    {
        playerVelocity = playerVelocityReceived;
    }

    //Receives finalRotation from PlayerController and outputs it as the playerRotation
    public void SetPlayerRotation (Vector3 playerRotationReceived)
    {
        playerRotation = playerRotationReceived;
    }

    //Receives finalCameraRotation from PlayerController and outputs it as the cameraRotation
    public void SetCameraRotation(Vector3 cameraRotationReceived)
    {
        cameraRotation = cameraRotationReceived;
    }

    //Moves the player
    void ExecutePlayerMovement()
    {
        if (playerVelocity != notMoving)
            rigibody.MovePosition(rigibody.position + playerVelocity * Time.fixedDeltaTime); //Delta time will keep the velocity yo be affected by the FPS rate
    }

    //Rotates the player
    void ExecutePlayerRotation()
    {
        rigibody.MoveRotation(transform.rotation * Quaternion.Euler(playerRotation));
    }

    //Rotates the camera
    void ExecuteCameraRotation()
    {
        if(playerCamera != null)
            playerCamera.transform.Rotate(cameraRotation);
    }
}
