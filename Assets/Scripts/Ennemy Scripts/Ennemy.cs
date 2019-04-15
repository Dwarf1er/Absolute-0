using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AI;

public abstract class Ennemy : NetworkBehaviour
{
    //Components
    protected virtual Animator Animator { get; set; } //Must have the following parameters: (float) Speed, (trigger) TakeDamage, (trigger) Death
    protected virtual NetworkAnimator NetAnimator { get; set; }
    protected virtual NavMeshAgent NavMeshAgent { get; set; }
    public GameObject Target { get; set; }
    [SerializeField] public GameObject DefaultTarget;

    //Stats
    int Tier;
    int MaxHP;
    int Armor;
    float StartingSpeed;
    protected int Damage;

    //Bools
    [SyncVar] public bool isAttacking;
    [SyncVar] bool isStopped;
    [SyncVar] protected bool isDead;
    [SyncVar] bool lastHitWasHeadshot;

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
                if (!isDead)
                    CmdTriggerDeath();
            }

            if (hp_ > MaxHP) //Prevent over-healing
                hp_ = MaxHP;
        }
    }

    float Speed
    {
        get { return speed_; }
        set
        {
            speed_ = value;
            NavMeshAgent.speed = value;
        }
    }

    //Backing Store
    [SyncVar] int hp_;
    [SyncVar] float speed_;

    

    protected virtual void Awake()
    {
        
    }

    [Command]
    public void CmdSetStats(int maxHp, int armor, float speed, int damage)
    {
        Animator = GetComponent<Animator>();
        NetAnimator = GetComponent<NetworkAnimator>();
        NavMeshAgent = GetComponent<NavMeshAgent>();

        MaxHP = maxHp;
        HP = MaxHP;
        Armor = armor;
        StartingSpeed = speed;
        Speed = speed;
        Damage = damage;

        isDead = false;
    }

    [Server]
    private void Update()
    {
        if (isDead)
            return;

        if (Target != null && Target.GetComponent<PlayerController>() != null) //Only recalculate for non-static targets (players)
            CmdSetDestination(Target.transform.position);

        //Controlling the animation
        isStopped = NavMeshAgent.velocity.magnitude == 0;

        Animator.SetBool("IsStopped", isStopped);
        Animator.SetFloat("Speed", NavMeshAgent.velocity.magnitude);
    }

    // Functions - Taking Damage

    [Command]
    public void CmdTakeDamage(int rawDamage)
    {
        lastHitWasHeadshot = false;
        int damage = rawDamage - Armor;

        if (damage >= 0)
            NetAnimator.SetTrigger("TakeDamage"); //Animation is only triggered if the ennemy takes damage
        else
            damage = 0; //Damage must not be negative

        HP -= damage;
    }

    [Command]
    public void CmdTakeHeadshot(int rawDamage)
    {
        lastHitWasHeadshot = true;
        int damage = rawDamage * 3 - Armor; //Headshot multiplier is x3

        if (damage >= 0)
            NetAnimator.SetTrigger("TakeDamage"); //Animation is only triggered if the ennemy takes damage
        else
            damage = 0; //Damage must not be negative

        HP -= damage;
    }

    [Command]
    public void CmdTriggerDeath()
    {
        Animator.SetBool("Dead", true);
        if (lastHitWasHeadshot)
            NetAnimator.SetTrigger("HeadshotDeath");
        else
            NetAnimator.SetTrigger("Death");

        isDead = true;

        //Stop the corpse from moving
        NavMeshAgent.isStopped = true;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    // Functions - Setting Target

    [Command]
    public void CmdSetDestination(Vector3 destination)
    {
        NavMeshAgent.SetDestination(destination);
    }
    
    [Command]
    public void CmdSetTarget(GameObject newTarget)
    {
        Target = newTarget;
        CmdSetDestination(Target.transform.position);
    }

    [Command]
    public void CmdSetDefaultTarget(GameObject defaultTarget)
    {
        DefaultTarget = defaultTarget;
        CmdSetTarget(DefaultTarget);
    }

    [Command]
    public void CmdTargetLost()
    {
        CmdSetTarget(DefaultTarget);
    }

    // Functions - Attacking

    [Command]
    public void CmdStartAttack()
    {
        NetAnimator.SetTrigger("StartAttack");
        isAttacking = true;
        Speed = 0;
    }

    [Command]
    public void CmdStopAttack()
    {
        NetAnimator.SetTrigger("StopAttack");
        isAttacking = false;
        Speed = StartingSpeed;
    }

    [Command]
    public void CmdAttack()
    {
        if (Target.GetComponent<PlayerStats>() != null)
            Target.GetComponent<PlayerStats>().TakeDamage(Damage);
        if (Target.GetComponent<Core>() != null)
            Target.GetComponent<Core>().TakeDamage(Damage);
    }
}
