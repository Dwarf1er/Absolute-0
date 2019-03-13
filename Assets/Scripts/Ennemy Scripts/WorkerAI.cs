using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerAI : Ennemy
{

    // Update is called once per frame
    void Update()
    {
        Animator.SetBool("IsStopped", NavMeshAgent.isStopped);
        Animator.SetFloat("Speed", NavMeshAgent.velocity.magnitude);
    }

    public override void TriggerDeath()
    {
        throw new System.NotImplementedException();
    }
}
