using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerAI : Ennemy
{
    static readonly int[] HPTiers = { 40, 80, 120, 160 };
    static readonly int[] ArmorTiers = { 0, 3, 6, 9 };
    static readonly int[] SpeedTiers = { 4, 4, 4, 4 };
    static readonly int[] DamageTiers = { 10, 20, 30, 40 };
    static readonly int[] CashTiers = { 5, 10, 15, 20 };

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
