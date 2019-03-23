using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerStats : NetworkBehaviour
{
    [SerializeField]
    private int maxHp = 100;

    //This allows the health of the player to be synced with the server
    [SyncVar]
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
        //Synced with server
        currentHp -= rawDamage;
        Debug.Log(transform.name + " now has" + currentHp + " HP");
    }
}
