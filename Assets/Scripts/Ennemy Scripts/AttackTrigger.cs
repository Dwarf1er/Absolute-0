using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    Ennemy RootEnnemy { get; set; }

    private void Awake()
    {
        RootEnnemy = GetComponentInParent<Ennemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == RootEnnemy.Target)
            RootEnnemy.inRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == RootEnnemy.Target)
            RootEnnemy.inRange = false;
    }
}
