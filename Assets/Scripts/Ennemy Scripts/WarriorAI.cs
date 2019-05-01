using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WarriorAI : Ennemy
{
    static readonly int[] HPTiers = { 100, 200, 300, 400 };
    static readonly int[] ArmorTiers = { 5, 10, 15, 20 };
    static readonly int[] SpeedTiers = { 4, 4, 4, 4 };
    static readonly int[] DamageTiers = { 25, 50, 75, 100 };
    static readonly int[] CashTiers = { 10, 20, 30, 40 };

    protected override void SetStats(int ennemyTier)
    {
        MaxHP = HPTiers[ennemyTier];
        HP = MaxHP;
        Armor = ArmorTiers[ennemyTier];
        StartingSpeed = SpeedTiers[ennemyTier];
        Speed = StartingSpeed;
        Damage = DamageTiers[ennemyTier];
        CashOnKill = CashTiers[ennemyTier];
    }
}
