using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerStats : NetworkBehaviour
{
    [SerializeField] private int MaxHP = 100;
    public int cash { get; private set; }
    NetworkAnimator animator { get; set; }
    //Backing Store
    [SyncVar(hook = "OnHpChanged")] public int currentHp;

    void Awake()
    {
        animator = gameObject.GetComponentInChildren<NetworkAnimator>();
        SetPlayerStats();
    }

    public void TakeDamage(int rawDamage)
    {
        if (!isServer)
            return;

        currentHp -= rawDamage;

        if (currentHp <= 0) //Trigger player death
        {
            currentHp = 0;
            TriggerDeath();
        }

        if (currentHp > MaxHP) //Prevent over-healing
            currentHp = MaxHP;

        Debug.Log(transform.name + " now has " + currentHp + " HP");
    }

    void OnHpChanged(int hp)
    {
        currentHp = hp;
    }

    //Used for the health bar size
    public float GetHpPercentage()
    {
        return (float)currentHp / (float)MaxHP;
    }

    public void SetPlayerStats()
    {
        currentHp = MaxHP; //The player always begins with his maximum health amount
        cash = 0;
        ChangeCash(50);
    }

    public void ChangeCash(int cashDifference)
    {
        cash += cashDifference;
        GameObject.Find("MoneyCounter").transform.Find("Money").GetComponent<Text>().text = cash.ToString();
    }

    public void TriggerDeath()
    {
        animator.SetTrigger("Death");
        GetComponentInChildren<Animator>().SetBool("Dead", true);

        Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
        rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX
                                    | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        GetComponent<PlayerController>().isDead = true;
        GetComponent<PlayerShootingController>().isDead = true;
    }

    public void Respawn()
    {
        GetComponentInChildren<Animator>().SetBool("Dead", false);

        SetPlayerStats();

        Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
        rigidbody.constraints = RigidbodyConstraints.None;

        GetComponent<PlayerController>().isDead = false;
        GetComponent<PlayerShootingController>().isDead = false;
    }
}
