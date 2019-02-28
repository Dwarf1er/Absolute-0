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
    [SerializeField]
    Vector3 cameraOffSet = new Vector3(0, 0.5f, 5);

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
        Vector3 finalRotation = new Vector3(0, rotationY, 0) * mouseSensitivity;

        //Doing the previous rotation on the player
        utilities.SetPlayerRotation(finalRotation);

        //Rotating the camera, rotation is on the X axis
        float rotationX = Input.GetAxisRaw("Mouse Y");
        float finalCameraRotation = rotationX * mouseSensitivity;

        //Executing the previous rotation on the camera
        utilities.SetCameraRotation(finalCameraRotation);

        //Positioning the camera
        Vector3 finalCameraOffSet = cameraOffSet;

        //Executing the previous positioning on the camera
        utilities.SetCameraOffSet(finalCameraOffSet);
    }
}
