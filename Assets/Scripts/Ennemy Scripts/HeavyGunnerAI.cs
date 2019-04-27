using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyGunnerAI : Ennemy
{
    static readonly int[] HPTiers = { 100, 200, 300, 400 };
    static readonly int[] ArmorTiers = { 5, 10, 15, 20 };
    static readonly int[] SpeedTiers = { 3, 3, 3, 3 };
    static readonly int[] DamageTiers = { 10, 15, 20, 25 };

    protected override void SetStats(int ennemyTier)
    {
        MaxHP = HPTiers[ennemyTier];
        HP = MaxHP;
        Armor = ArmorTiers[ennemyTier];
        StartingSpeed = SpeedTiers[ennemyTier];
        Speed = StartingSpeed;
        Damage = DamageTiers[ennemyTier];
    }
}
