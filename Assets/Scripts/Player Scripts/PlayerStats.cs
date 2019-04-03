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

    //The player always begins with his maximum health amount
    public void SetPlayerStats()
    {
        currentHp = maxHp;
    }

    //Used for the health bar size
    public float GetHpAmount()
    {
        return (float)currentHp / maxHp;
    }

    //Used to calculate the damage dealt to the player after an attack
    public void TakeDamage(int rawDamage)
    {
        currentHp -= rawDamage;
        Debug.Log(transform.name + " now has" + currentHp + " HP");
    }
}
