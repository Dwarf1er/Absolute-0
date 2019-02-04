using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    void Update()
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
