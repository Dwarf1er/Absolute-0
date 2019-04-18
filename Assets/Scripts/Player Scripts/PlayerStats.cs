using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerStats : NetworkBehaviour
{
    [SerializeField]
    private int MaxHP = 100;

    //Properties
    int HP
    {
        get { return hp_; }
        set
        {
            hp_ = value;

            if (hp_ <= 0) //Trigger player death
            {
                hp_ = 0;
                /* Death has not yet been added
                if (!isDead)
                    CmdTriggerDeath();
                */
            }

            if (hp_ > MaxHP) //Prevent over-healing
                hp_ = MaxHP;

            Debug.Log("Set HP to " + HP);
        }
    }

    //Backing Store
    [SerializeField] //Serialize Field for debugging purposes
    [SyncVar] int hp_;

    void Awake()
    {
        SetPlayerStats();
    }
    
    public void SetPlayerStats()
    {
        HP = MaxHP; //The player always begins with his maximum health amount
    }

    //Used for the health bar size
    public float GetHpPercentage()
    {
        return (float) HP / MaxHP;
    }
    
    public void TakeDamage(int rawDamage)
    {
        HP -= rawDamage;
        Debug.Log(transform.name + " now has" + HP + " HP");
    }
}
