using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerUtilities))]
public class PlayerController : MonoBehaviour
{
    //Player attributes
    [SerializeField]
    float playerSpeed = 10f;
    [SerializeField]
    float mouseSensitivity = 5f;

    //Reference to PlayerUtilities to be set in Start
    PlayerUtilities utilities { get; set; }

    private void Start()
    {
        //Ensuring every player gets a rigibody
        utilities = GetComponent<PlayerUtilities>();
    }

    private void Update()
    {
        //Creation of the player movement vector
        float movementX = Input.GetAxisRaw("Horizontal"); //Axes go from -1 to 1
        float movementZ = Input.GetAxisRaw("Vertical");

        Vector3 horizontalMovement = transform.right * movementX; //Left and right
        Vector3 verticalMovement = transform.forward * movementZ; //Forward and backward

        //Complete movement vector (adding up previous vectors)
        Vector3 finalMovement = (horizontalMovement + verticalMovement).normalized * playerSpeed; //Normalizing the vectors make for a continous/stable speed

        //Moving the player
        utilities.SetPlayerVelocity(finalMovement);

        //Rotating the player, rotation is on the Y axis
        float rotationY = Input.GetAxisRaw("Mouse X");
        Vector3 finalRotation = new Vector3(0f, rotationY, 0f) * mouseSensitivity;

        //Doing the previous rotation on the player
        utilities.SetPlayerRotation(finalRotation);

        //Rotating the camera, rotation is on the X axis
        float rotationX = Input.GetAxisRaw("Mouse Y");
        Vector3 finalCameraRotation = new Vector3(-rotationX, 0f, 0f) * mouseSensitivity; //The -rotationX is to prevent the movment from being inverted to the mouse's

        //Doing the previous rotation on the camera
        utilities.SetCameraRotation(finalCameraRotation);
    }
}
