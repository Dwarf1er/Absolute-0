using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoldierControler : MonoBehaviour
{
    public Camera playerCam;
    public NavMeshAgent soldierAgent;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray mouseRay = playerCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit mouseTarget;

            if (Physics.Raycast(mouseRay, out mouseTarget))
            {
                soldierAgent.SetDestination(mouseTarget.point);
            }
        }

        animator.SetFloat("Velocity", soldierAgent.velocity.magnitude);
    }
}
