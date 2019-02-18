using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public GameObject cameraTarget;
    public float mouseSensitivity = 2;
    public Vector3 cameraDepth = new Vector3(0, 10, -20);
    Vector3 cameraOffset;

    private void Start()
    {
        cameraOffset = new Vector3(0,-2,5);  //Keeping an equal distance between the player and the camera
    }

    private void Update()
    {
            if (cameraTarget.transform != null)
            {
                transform.position = cameraTarget.transform.position + cameraDepth;
            }
    }

    private void LateUpdate()  //LateUpdate runs after Update ensuring that the player finished moving before the camera adjusts
    {
        float horizontal = Input.GetAxis("Mouse X") * mouseSensitivity; //Using the mouse's X axis to rotate the camera's transform
        cameraTarget.transform.Rotate(0, horizontal, 0);

        float desiredAngle = cameraTarget.transform.eulerAngles.y;    //why??                           
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        transform.position = cameraTarget.transform.position - (rotation * cameraOffset);

        float vertical = Input.GetAxis("Mouse Y") * mouseSensitivity; //Using the mouse's Y axis to rotate the camera's transform
        transform.Rotate(0, 0, vertical);

        transform.LookAt(cameraTarget.transform);
    }

    public void setTarget(GameObject target)  //sets the player's transform as the camera target
    {
        cameraTarget = target;
    }
}
