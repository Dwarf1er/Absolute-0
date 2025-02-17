﻿using System.Collections;
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
    protected int Tier;
    protected int MaxHP;
    protected int Armor;
    protected float StartingSpeed;
    [SyncVar] protected int Damage;
    protected int CashOnKill;

    //Bools
    [SyncVar] public bool isAttacking;
    [SyncVar] bool isStopped;
    [SyncVar] public bool isDead;
    [SyncVar] bool lastHitWasHeadshot;

    //Skins
    [SerializeField] public Material SkinTier0;
    [SerializeField] public Material SkinTier1;
    [SerializeField] public Material SkinTier2;
    [SerializeField] public Material SkinTier3;

    //Properties
    protected int HP
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

    protected float Speed
    {
        get { return speed_; }
        set
        {
            speed_ = value;
            NavMeshAgent.speed = value;
        }
    }

    //Backing Store
    [SerializeField] //Serialize Field for debugging purposes
    [SyncVar] int hp_;
    [SerializeField] //Serialize Field for debugging purposes
    [SyncVar] float speed_;

    

    protected virtual void Awake()
    {
        
    }
    
    public void CmdSpawn(int tier)
    {
        Animator = GetComponent<Animator>();
        NetAnimator = GetComponent<NetworkAnimator>();
        NavMeshAgent = GetComponent<NavMeshAgent>();

        SetStats(tier);

        Material[] skins = new Material[] { SkinTier0, SkinTier1, SkinTier2, SkinTier3 };
        GetComponentInChildren<SkinnedMeshRenderer>().material = skins[tier];

        isDead = false;
    }

    protected abstract void SetStats(int ennemyTier);

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

        foreach (PlayerStats playerStats in FindObjectsOfType<PlayerStats>())
        {
            playerStats.ChangeCash(CashOnKill);
        }

        //Stop the corpse from moving
        NavMeshAgent.isStopped = true;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    // Functions - Setting Target

    [Server]
    public void CmdSetDestination(Vector3 destination)
    {
        NavMeshAgent.SetDestination(destination);
    }
    
    [Server]
    public void CmdSetTarget(GameObject newTarget)
    {
        Target = newTarget;
        CmdSetDestination(Target.transform.position);
    }

    [Server]
    public void CmdSetDefaultTarget(GameObject defaultTarget)
    {
        DefaultTarget = defaultTarget;
        CmdSetTarget(DefaultTarget);
    }

    [Server]
    public void CmdTargetLost()
    {
        CmdSetTarget(DefaultTarget);
    }

    // Functions - Attacking

    [Server]
    public void CmdStartAttack()
    {
        NetAnimator.SetTrigger("StartAttack");
        isAttacking = true;
        Speed = 0;
    }

    [Server]
    public void CmdStopAttack()
    {
        NetAnimator.SetTrigger("StopAttack");
        isAttacking = false;
        Speed = StartingSpeed;
    }

    [Server]
    public virtual void CmdAttack()
    {
        if (Target.GetComponent<PlayerStats>() != null)
            CmdPlayerAttacked(Target);
            //Target.GetComponent<PlayerStats>().TakeDamage(Damage);
        if (Target.GetComponent<Core>() != null)
            Target.GetComponent<Core>().TakeDamage(Damage);
    }

    [Command]
    void CmdPlayerAttacked (GameObject target)
    {
        target.GetComponent<PlayerStats>().TakeDamage(Damage);
    }
}
