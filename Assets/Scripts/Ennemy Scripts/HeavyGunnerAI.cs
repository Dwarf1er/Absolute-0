using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyGunnerAI : Ennemy
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject muzzle;

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

    public override void CmdAttack()
    {
        base.CmdAttack();
        Vector3 gunMuzzlePosition = muzzle.transform.position;
        GameObject newBullet = Instantiate(bulletPrefab, gunMuzzlePosition, Quaternion.identity);
        Vector3 direction = Target.transform.position - gunMuzzlePosition;
        newBullet.GetComponent<Rigidbody>().AddForce(direction * 0.2f, ForceMode.Impulse);
        muzzle.GetComponent<AudioSource>().Play();
    }
}
