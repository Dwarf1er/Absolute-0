using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerStats : NetworkBehaviour
{
    [SerializeField]
    private int maxHp = 100;
    int currentHp;

    void Awake()
    {
        SetPlayerStats();
    }

    public void SetPlayerStats()
    {
        currentHp = maxHp;
    }

    public void TakeDamage(int rawDamage)
    {
        currentHp -= rawDamage;
        Debug.Log(transform.name + " now has" + currentHp + " HP");
    }
}
