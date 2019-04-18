using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerStats : NetworkBehaviour
{
    [SerializeField]
    private int MaxHP = 100;
    
    //Properties
    /*
    int HP
    {
        get
        {
            int hp = hp_;
            return hp;
        }
        set
        {
            hp_ = value;

            if (hp_ <= 0) //Trigger player death
            {
                hp_ = 0;
            }

            if (hp_ > MaxHP) //Prevent over-healing
                hp_ = MaxHP;

            Debug.Log("Set HP to " + HP);
        }
    }
    */

    //Backing Store
    [SyncVar]
    int hp;

    void Awake()
    {
        SetPlayerStats();
    }

    [Client]
    public void SetPlayerStats()
    {
        hp = MaxHP; //The player always begins with his maximum health amount
    }

    //Used for the health bar size
    public float GetHpPercentage()
    {
        return (float) hp / (float) MaxHP;
    }

    [Client]
    public void TakeDamage(int rawDamage)
    {
        hp -= rawDamage;
        if (hp <= 0) //Trigger player death
        {
            hp = 0;
        }

        if (hp > MaxHP) //Prevent over-healing
            hp = MaxHP;
        Debug.Log(transform.name + " now has " + hp + " HP");
    }
}
