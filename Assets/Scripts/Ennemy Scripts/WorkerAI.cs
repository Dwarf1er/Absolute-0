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
        if (isDead)
            return;

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
            Animator.SetTrigger("StopAttack");
        }

        //Attack
        if (timeSinceLastAttack < AttackDelay)
            timeSinceLastAttack += Time.deltaTime; //No need to be calculated if the ennemy has already waited long enough
        else
        {
            if (inRange)
            {
                AttackTarget();
                timeSinceLastAttack = 0;
            }
        }


        //Controlling the animation
        stopped = NavMeshAgent.velocity.magnitude == 0;
        stride = NavMeshAgent.velocity.magnitude / NavMeshAgent.speed;

        Animator.SetBool("IsStopped", stopped);
        Animator.SetFloat("Speed", NavMeshAgent.velocity.magnitude);
        Animator.SetFloat("Stride", stride);
    }

    void AttackTarget()
    {
        Animator.SetTrigger("Attack");
        //Target.GetComponent<PlayerUtilities>().
    }
}
