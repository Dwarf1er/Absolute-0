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
        Vector3 cameraOffsetVector = new Vector3(0, 0, 5);
        Vector3 cameraOffsetVectorN = cameraOffsetVector.normalized;
        cameraOffset = cameraOffsetVector;  //Keeping an equal distance between the player and the camera
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
        float horizontal = Input.GetAxis("Mouse X") * mouseSensitivity; //Using the mouse's X axis to rotate the player's transform
        float vertical = Input.GetAxis("Mouse Y") * mouseSensitivity; //Using the mouse's Y axis to rotate the camera's transform
        transform.Rotate(0, horizontal, 0);  //Rotating the camera's transform on the Y axis
        cameraTarget.transform.Rotate(0, horizontal, 0);   //Rotating the player's transform on the Y axis
        transform.Rotate(vertical, 0, 0);   //Rotating the camera's transform on the X axis


        float desiredAngleY = cameraTarget.transform.eulerAngles.y;  //The desired angle on the Y axis is the same as the player's transform
        float desiredAngleX = transform.eulerAngles.x;  //The desired angle on the X axis is the same as the camera's transform

        if (transform.localEulerAngles.x > 80 && transform.localEulerAngles.x < 280)  //To keep the player from looking upwards at more than 80 degrees
        {
            desiredAngleX = 80f;
        }

        if (transform.localEulerAngles.x < 280 && transform.localEulerAngles.x > 80)  //To keep the player from looking downwards at more than 80 degrees
        {
            desiredAngleX = 280f;
        }
        

        Quaternion rotation = Quaternion.Euler(desiredAngleX, desiredAngleY, 0);  //Transforming the rotation into a Quaternion based on the desired angles
        transform.position = cameraTarget.transform.position - (rotation * cameraOffset);  //Applying the rotation to the camera's transform

        transform.LookAt(cameraTarget.transform);
    }

    public void setTarget(GameObject target)  //sets the player's transform as the camera target
    {
        cameraTarget = target;
    }
}
