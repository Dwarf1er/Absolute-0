using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        Camera.main.GetComponent­<PlayerCamera>().setTarget(gameObject.transform);   //set the target for the camera as the player on instanciation
    }

    void Update()
    {
        if (isLocalPlayer)  //Checks for local player ownership
        {
            if (Input.GetKey(KeyCode.A))
                transform.Translate(Vector3.left);

            if (Input.GetKey(KeyCode.W))
                transform.Translate(Vector3.forward);

            if (Input.GetKey(KeyCode.S))
                transform.Translate(Vector3.back);

            if (Input.GetKey(KeyCode.D))
                transform.Translate(Vector3.right);
        }
    }
}
