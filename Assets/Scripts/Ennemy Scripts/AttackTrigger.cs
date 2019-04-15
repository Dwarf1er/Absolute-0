using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AttackTrigger : MonoBehaviour
{
    Ennemy RootEnnemy { get; set; }

    private void Awake()
    {
        RootEnnemy = GetComponentInParent<Ennemy>();
    }

    //[Client]
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == RootEnnemy.Target)
        {
            RootEnnemy.CmdStartAttack();
        }
    }

    //[Client]
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == RootEnnemy.Target)
        {
            RootEnnemy.CmdStopAttack();
        }
    }
}
