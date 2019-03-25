using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    //Weapon will only be used inside PlayerWeapons to create our weapons
    public class Weapon
    {
        //Weapon attributes
        public int WeaponDamage { get; private set; }
        public int WeaponRange { get; private set; }
        public float WeaponFireRate { get; private set; }

        public Weapon(int weaponDamage, int weaponRange, float weaponFireRate)
        {
            WeaponDamage = weaponDamage;
            WeaponRange = weaponRange;
            WeaponFireRate = weaponFireRate;
        }
    }

    //Weapons
    public static Weapon BenelliM4 = new Weapon(1, 1, 1);
    public static Weapon M4 = new Weapon(25, 120, 5);
    public static Weapon M110 = new Weapon(1, 1, 1);
    public static Weapon M249 = new Weapon(1, 1, 1);
    public static Weapon MP5 = new Weapon(1, 1, 1);
    public static Weapon SMAW = new Weapon(1, 1, 1);

    /*
    private void Start()
    {
        CreateWeapons();
    }

    void CreateWeapons()
    {
        //Damage, range, fireRate
        BenelliM4 = new Weapon(1, 1, 1);
        M4 = new Weapon(25, 120, 5);
        M110 = new Weapon(1, 1, 1);
        M249 = new Weapon(1, 1, 1);
        MP5 = new Weapon(1, 1, 1);
        SMAW = new Weapon(1, 1, 1);
    }*/
}
