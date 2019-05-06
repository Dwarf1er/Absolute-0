using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerUtilities))]
public class PlayerController : MonoBehaviour
{
    //Player attributes
    [SerializeField]
    float playerSpeed = 15f;
    [SerializeField]
    float mouseSensitivity = 5f;
    Vector3 CameraOffSet = new Vector3(0, 0, -5f); //The camera offset should only be a distance on the z axis
    public bool isDead { get; set; }

    //Reference to PlayerUtilities to be set in Start
    PlayerUtilities utilities { get; set; }

    private void Start()
    {
        //Ensuring every player gets a rigibody
        utilities = GetComponent<PlayerUtilities>();
    }

    private void Update()
    {
        if (isDead)
            return;

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

        //Calculating the camera offset
        Vector3 finalCameraOffSet = CameraOffSet;
        RaycastHit raycastHit;
        Vector3 raycastOrigin = gameObject.transform.position + (Vector3.up * PlayerUtilities.PlayerHeight) + CameraOffSet.z * utilities.playerCamera.transform.TransformDirection(Vector3.forward); //Tête du player + 5 vers l'arrière
        Vector3 raycastDirection = utilities.playerCamera.transform.TransformDirection(Vector3.forward);
        float raycastRange = 5f;
        int raycastMask = LayerMask.GetMask("LocalPlayer", "Environment");

        if (Physics.Raycast(raycastOrigin, raycastDirection, out raycastHit, raycastRange, raycastMask))
        {
            //Checks if the camera is behind a part of the environment (e.g a wall, the floor, etc...)
            if (raycastHit.transform.gameObject.layer == LayerMask.NameToLayer("Environment"))
            {
                Debug.DrawRay(raycastOrigin, raycastDirection * raycastHit.distance, Color.red); //Debug info
                //Debug.Log("Hit l'environnement"); //Debug info
                float translateFactor = (raycastHit.point - raycastOrigin).magnitude / CameraOffSet.magnitude; //Translate factor is determined by the distance from obstacle to camera and from player to camera
                finalCameraOffSet *= (0.9f - translateFactor); // (1 - translateFactor) collerait la caméra à la surface du plancher/mur
            }

            //Checks if the camera is not behind a part of the environment (e.g a wall, the floor, etc...)
            if (raycastHit.transform.gameObject.layer == LayerMask.NameToLayer("LocalPlayer"))
            {
                Debug.DrawRay(raycastOrigin, raycastDirection * raycastHit.distance, Color.blue); //Debug info
                //Debug.Log("Hit le joueur"); //Debug info
            }
        }

        //Executing the previous positioning on the camera
        utilities.SetCameraOffSet(finalCameraOffSet);
    }

    public void SetCursorActive(bool isActive)
    {
        Scene currentScene = SceneManager.GetActiveScene();

        Cursor.visible = isActive;
        Cursor.lockState = CursorLockMode.Confined;

        if (currentScene.name == "Lobby")
            return;
        
        if (isActive)
            Cursor.lockState = CursorLockMode.Confined;
    }
}
