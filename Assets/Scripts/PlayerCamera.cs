using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform playerTransform;
    public GameObject cameraTarget;
    public float rotationVelocity = 2;
    public Vector3 cameraDepth = new Vector3(0, 10, -20);
    Vector3 cameraOffset;

    private void Start()
    {
        cameraOffset = cameraTarget.transform.position - transform.position;  //Keeping an equal distance between the player and the camera
    }

    private void Update()
    {
            if (playerTransform != null)
            {
                transform.position = playerTransform.position + cameraDepth;
            }
    }

    private void LateUpdate()  //LateUpdate runs after Update ensuring that the player finished moving before the camera adjusts
    {
        float horizontal = Input.GetAxis("Mouse X") * rotationVelocity; //Using the mouse's X axis to rotate the camera's transform
        cameraTarget.transform.Rotate(0, horizontal, 0);

        //float desiredAngle = cameraTarget.transform.eulerAngles.y;                               WTF IS THIS TBNK
        //Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        //transform.position = cameraTarget.transform.position - (rotation * cameraOffset);

        //transform.LookAt(cameraTarget.transform);
    }

    public void setTarget(Transform target)  //sets the player's transform as the camera target
    {
        playerTransform = target;
    }
}
