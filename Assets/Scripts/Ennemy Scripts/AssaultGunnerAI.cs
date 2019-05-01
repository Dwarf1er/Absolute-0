using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultGunnerAI : Ennemy
{
    static readonly int[] HPTiers = { 60, 120, 180, 240 };
    static readonly int[] ArmorTiers = { 3, 6, 9, 12 };
    static readonly int[] SpeedTiers = { 4, 4, 4, 4 };
    static readonly int[] DamageTiers = { 10, 15, 20, 25 };
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
