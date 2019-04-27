using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerStats : NetworkBehaviour
{
    [SerializeField] private int MaxHP = 100;
    PlayerUtilities utilities { get; set; }
    //Backing Store
    [SyncVar(hook = "OnHpChanged")] int currentHp;

    void Awake()
    {
        utilities = GetComponent<PlayerUtilities>();
        SetPlayerStats();
    }

    public void TakeDamage(int rawDamage)
    {
        if (!isServer)
            return;

        currentHp -= rawDamage;

        if (currentHp <= 0) //Trigger player death
            currentHp = 0;

        if (currentHp > MaxHP) //Prevent over-healing
            currentHp = MaxHP;

        Debug.Log(transform.name + " now has " + currentHp + " HP");
    }

    void OnHpChanged(int hp)
    {
        var UI = utilities.playerUI.GetComponent<PlayerUI>();

        UI.hpBar.sizeDelta = new Vector2(hp * 2, UI.hpBar.sizeDelta.y);
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
    }
}
