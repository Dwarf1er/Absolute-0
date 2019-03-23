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
        RootEnnemy.SetTarget(other.gameObject);
    }
}
