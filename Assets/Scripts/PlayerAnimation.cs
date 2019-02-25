using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    PlayerUtilities utilities;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        utilities = GetComponent<PlayerUtilities>();
    }

    // Update is called once per frame
    void Update()
    {
        //animator.SetFloat("Velocity", utilities.playerVelocity);
    }
}
