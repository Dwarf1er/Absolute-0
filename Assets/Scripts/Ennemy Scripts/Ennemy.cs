using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Ennemy : MonoBehaviour
{
    //Components
    protected virtual Animator Animator { get; set; }
    protected virtual NavMeshAgent NavMeshAgent { get; set; }

    //Stats
    int Tier { get; set; }
    int MaxHP { get; set; }
    int HP
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
    int Armor { get; set; }
    float Speed { get; set; }
    int Damage { get; set; }
    bool hasRangedAttack { get; set; } //lol ça restera pas là longtemps

    //Backing Store
    int hp;

    protected virtual void Awake()
    {
        Animator = GetComponent<Animator>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    //Functions
    public abstract void TriggerDeath();
}
