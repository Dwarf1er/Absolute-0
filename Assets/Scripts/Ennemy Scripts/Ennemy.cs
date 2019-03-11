using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Ennemy : MonoBehaviour
{
    //Components
    Animator Animator { get; set; }
    NavMeshAgent NavMeshAgent { get; set; }

    //Stats
    int Tier { get; set; }
    int MaxHP
    {
        get { return hp; }
        set
        {
            hp = value;

            if (hp <= 0) //Trigger NPC death
            {
                hp = 0;
                TriggerDeath();
            }

            if (hp > MaxHP) //Prevent over-healing
                hp = MaxHP;
        }
    }
    int HP { get; set; }
    int Armor { get; set; }
    float Speed { get; set; }
    int Damage { get; set; }
    bool hasRangedAttack { get; set; } //lol ça restera pas là longtemps

    //Backing Store
    int hp;

    //Functions
    public abstract void TriggerDeath();
}
