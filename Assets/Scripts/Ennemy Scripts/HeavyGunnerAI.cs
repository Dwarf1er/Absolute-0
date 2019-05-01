using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyGunnerAI : Ennemy
{
    static readonly int[] HPTiers = { 300, 600, 900, 1200 };
    static readonly int[] ArmorTiers = { 5, 10, 15, 20 };
    static readonly int[] SpeedTiers = { 3, 3, 3, 3 };
    static readonly int[] DamageTiers = { 10, 15, 20, 25 };
    static readonly int[] CashTiers = { 40, 80, 120, 160 };

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
