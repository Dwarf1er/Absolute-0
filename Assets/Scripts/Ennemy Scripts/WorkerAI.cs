using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerAI : Ennemy
{
    bool stopped;
    float stride; //how much speed this ennemy has accumulated (velocity/max speed)

    protected override void Awake()
    {
        Animator = GetComponent<Animator>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //Pathfinding
        if (inRange)
        {
            NavMeshAgent.isStopped = true;
        }
        else
        {
            NavMeshAgent.isStopped = false;
            if (Target != null && Target.GetComponent<PlayerController>() != null) //Only recalculate for non-static targets (players)
                SetDestination(Target.transform.position);
        }
        

        //Controlling the animation
        stopped = NavMeshAgent.velocity.magnitude == 0;
        stride = NavMeshAgent.velocity.magnitude / NavMeshAgent.speed;

        Animator.SetBool("IsStopped", stopped);
        Animator.SetFloat("Speed", NavMeshAgent.velocity.magnitude);
        Animator.SetFloat("Stride", stride);
    }
}
