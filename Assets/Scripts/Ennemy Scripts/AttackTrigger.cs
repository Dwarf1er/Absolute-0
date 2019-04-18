using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AttackTrigger : NetworkBehaviour
{
    Ennemy RootEnnemy { get; set; }

    private void Awake()
    {
        RootEnnemy = GetComponentInParent<Ennemy>();
    }

    [Server]
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == RootEnnemy.Target)
        {
            RootEnnemy.CmdStartAttack();
        }
    }

    [Server]
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == RootEnnemy.Target)
        {
            RootEnnemy.CmdStopAttack();
        }
    }
}
