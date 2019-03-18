using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Ennemy : MonoBehaviour
{
    //Components
    protected virtual Animator Animator { get; set; } //Must have the following parameters: (float) Speed, (trigger) TakeDamage, (trigger) Death
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

    public void SetStats(int maxHp, int armor, float speed, int damage)
    {
        MaxHP = maxHp;
        HP = MaxHP;
        Armor = armor;
        Speed = speed;
        Damage = damage;
    }

    private void Update()
    {
        Animator.SetFloat("Speed", NavMeshAgent.velocity.magnitude);
    }

    //Functions
    public void TriggerDeath()
    {
        Animator.SetTrigger("Death");
    }

    public void TakeDamage(int rawDamage)
    {
        int damage = rawDamage - Armor;

        if (damage >= 0)
            Animator.SetTrigger("TakeDamage"); //Animation is only triggered if the ennemy takes damage
        else
            damage = 0; //Damage must not be negative

        HP -= damage;
    }

    public void SetDestination(Vector3 destination)
    {
        NavMeshAgent.SetDestination(destination);
    }
}
