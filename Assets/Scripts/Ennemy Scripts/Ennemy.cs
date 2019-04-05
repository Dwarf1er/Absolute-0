using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AI;

public abstract class Ennemy : NetworkBehaviour
{
    //Components
    protected virtual Animator Animator { get; set; } //Must have the following parameters: (float) Speed, (trigger) TakeDamage, (trigger) Death
    protected virtual NavMeshAgent NavMeshAgent { get; set; }
    [SerializeField] public GameObject Target;
    [SerializeField] public GameObject DefaultTarget;

    //Stats
    int Tier;
    int MaxHP;
    int Armor;
    float StartingSpeed;
    float Speed
    {
        get { return speed_; }
        set
        {
            speed_ = value;
            NavMeshAgent.speed = value;
        }
    }
    protected int Damage;

    [SerializeField] protected float AttackDelay;
    [SerializeField] protected float timeSinceLastAttack;

    [SerializeField] protected bool isDead;

    //Properties
    int HP
    {
        get { return hp_; }
        set
        {
            hp_ = value;

            if (hp_ <= 0) //Trigger NPC death
            {
                hp_ = 0;
                TriggerDeath();
            }

            if (hp_ > MaxHP) //Prevent over-healing
                hp_ = MaxHP;
        }
    }

    //Backing Store
    [SerializeField] int hp_;
    [SerializeField] float speed_;

    //Bools
    //public bool inRange;
    public bool isAttacking;

    protected virtual void Awake()
    {
        Animator = GetComponent<Animator>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void SetStats(int maxHp, int armor, float speed, int damage, float attackDelay)
    {
        MaxHP = maxHp;
        HP = MaxHP;
        Armor = armor;
        StartingSpeed = speed;
        Speed = speed;
        Damage = damage;
        AttackDelay = attackDelay;

        isDead = false;
    }

    private void Update()
    {
        if (isDead)
            return;
        Animator.SetFloat("Speed", NavMeshAgent.velocity.magnitude);
    }

    // Functions - Taking Damage
    
    public void TakeDamage(int rawDamage)
    {
        int damage = rawDamage - Armor;

        if (damage >= 0)
            Animator.SetTrigger("TakeDamage"); //Animation is only triggered if the ennemy takes damage
        else
            damage = 0; //Damage must not be negative

        HP -= damage;
    }

    public void TriggerDeath()
    {
        Animator.SetBool("Dead", true);
        Animator.SetTrigger("Death");
        isDead = true;

        //Stop the corpse from moving
        NavMeshAgent.isStopped = true;
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    // Functions - Setting Target

    public void SetDestination(Vector3 destination)
    {
        NavMeshAgent.SetDestination(destination);
    }
    
    public void SetTarget(GameObject newTarget)
    {
        Target = newTarget;
        SetDestination(Target.transform.position);
    }

    public void SetDefaultTarget(GameObject defaultTarget)
    {
        DefaultTarget = defaultTarget;
        SetTarget(DefaultTarget);
    }

    public void TargetLost()
    {
        SetTarget(DefaultTarget);
    }

    // Functions - Attacking

    public void StartAttack()
    {
        Animator.SetTrigger("StartAttack");
        isAttacking = true;
        Speed = 0;
    }

    public void StopAttack()
    {
        Animator.SetTrigger("StopAttack");
        isAttacking = false;
        Speed = StartingSpeed;
    }

    public void Attack()
    {
        if (Target.GetComponent<PlayerStats>() != null)
            Target.GetComponent<PlayerStats>().TakeDamage(Damage);
        if (Target.GetComponent<Core>() != null)
            Target.GetComponent<Core>().TakeDamage(Damage);
    }
}
