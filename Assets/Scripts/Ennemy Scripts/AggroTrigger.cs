using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroTrigger : MonoBehaviour
{
    Ennemy RootEnnemy { get; set; }

    private void Awake()
    {
        RootEnnemy = GetComponentInParent<Ennemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (RootEnnemy.Target == RootEnnemy.DefaultTarget) //Will not change target if the ennemy is already chasing someone
            RootEnnemy.SetTarget(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == RootEnnemy.Target) //Will not lose target because of another player leaving aggro range
            RootEnnemy.TargetLost();
    }
}
