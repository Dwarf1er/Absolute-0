using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerAI : Ennemy
{
    int[] HPTiers = { 1, 2, 3, 4 };
    int[] ArmorTiers = { 1, 2, 3, 4 };
    int[] SpeedTiers = { 1, 2, 3, 4 };
    int[] DamageTiers = { 1, 2, 3, 4 };

    protected override void SetStats(int ennemyTier)
    {

    }
}
