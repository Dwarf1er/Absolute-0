using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
public class PlayerUtilities : NetworkBehaviour
{
    //Constants
    Vector3 notMoving { get; set; } //Used to verify if player is not moving and is the default state
    const float defaultRotation = 0f; //Used to set default camera rotation to 0
    public const float PlayerHeight = 2.60f;

    //References
    public Vector3 playerVelocity { get; private set; }
    Vector3 playerRotation { get; set; }
    Vector3 cameraOffSet { get; set; }
    float cameraRotation { get; set; } //Camera rotation on the X axis
    float liveCameraRotation { get; set; } //Current camera rotation on the X axis
    Rigidbody rigibody { get; set; }
    Animator animator; // Used to trigger animations
    bool inAir { get; set; } //Checks if the player jumped
    bool inSprint { get; set; } //Checks if the player is sprinting
    public GameObject playerUI { get; set; }

    [SerializeField]
    public Camera playerCamera;
    [SerializeField]
    float playerCameraRotationCap = 70f;

    void Start()
    {
        //Instantiation of player references
        notMoving = Vector3.zero;
        playerVelocity = notMoving;
        playerRotation = notMoving;
        cameraRotation = defaultRotation;
        liveCameraRotation = defaultRotation;
        rigibody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    //Checks if the player is in contact with the ground
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Environment"))
            animator.SetTrigger("Land");
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Environment") && inAir)
            inAir = false;
    }

    void OnCollisionExit(Collision collision)
    {
        inAir = true;
    }

    //As recommended by Unity for physics calculations, using FixedUpdate instead of Update
    void FixedUpdate()
    {
        ExecutePlayerMovement();
        ExecutePlayerRotation();
        ExecuteCameraRotation();
        ExecuteCameraPositioning();
    }

    //Receives finalMovement from PlayerController and outputs it as the playerVelocity
    public void SetPlayerVelocity (Vector3 playerVelocityReceived)
    {
        playerVelocity = playerVelocityReceived;
        animator.SetFloat("Velocity", playerVelocity.magnitude);
    }

    //Receives finalRotation from PlayerController and outputs it as the playerRotation
    public void SetPlayerRotation (Vector3 playerRotationReceived)
    {
        playerRotation = playerRotationReceived;
    }

    //Receives finalCameraRotation from PlayerController and outputs it as the cameraRotation
    public void SetCameraRotation(float cameraRotationReceived)
    {
        cameraRotation = cameraRotationReceived;
    }

    //Receives finalCameraPosition from PlayerController and outputs it as the cameraPosition
    public void SetCameraOffSet(Vector3 cameraOffSetReceived)
    {
        cameraOffSet = cameraOffSetReceived;
    }

    //Moves the player
    void ExecutePlayerMovement()
    {
        if (playerVelocity != notMoving)
        {
            //Delta time will keep the velocity yo be affected by the FPS rate
            if (Input.GetKey(KeyCode.LeftShift))
            {
                rigibody.MovePosition(rigibody.position + (playerVelocity * 1.5f) * Time.fixedDeltaTime); //Times 2 playerVelocity to go faster
                animator.SetBool("Sprint", true);
            }

            else
            {
                rigibody.MovePosition(rigibody.position + playerVelocity * Time.fixedDeltaTime);
                animator.SetBool("Sprint", false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && !inAir)
        {
            rigibody.AddForce(Vector3.up * 5, ForceMode.Impulse);
            animator.SetTrigger("Jump");            
        }

        animator.SetFloat("ZVelocity", Input.GetAxisRaw("Vertical"));
        animator.SetFloat("XVelocity", Input.GetAxisRaw("Horizontal"));
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
        {
            liveCameraRotation -= cameraRotation; //Using a -= because a += would make the rotation inverted
            liveCameraRotation = Mathf.Clamp(liveCameraRotation, -playerCameraRotationCap, playerCameraRotationCap); //Keeps the camera rotation between two angles
            playerCamera.transform.localEulerAngles = new Vector3(liveCameraRotation, 0f, 0f); //Applying the rotation to the camera's transform
        }        
    }

    //Positions the camera
    void ExecuteCameraPositioning()
    {
        if(playerCamera != null)
        {
            playerCamera.transform.localPosition = Vector3.up * PlayerHeight;
            playerCamera.transform.Translate(cameraOffSet);
        }
    }
}
