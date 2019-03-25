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
    public Weapon BenelliM4 { get; set; }
    public Weapon M4 { get; set; }
    public Weapon M110 { get; set; }
    public Weapon M249 { get; set; }
    public Weapon MP5 { get; set; }
    public Weapon SMAW { get; set; }

    //Weapons graphics
    [SerializeField]
    GameObject BenelliM4Model;
    [SerializeField]
    GameObject M4Model;
    [SerializeField]
    GameObject M110Model;
    [SerializeField]
    GameObject M249Model;
    [SerializeField]
    GameObject MP5Model;
    [SerializeField]
    GameObject SMAWModel;

    private void Start()
    {
        CreateWeapons();
    }

    void CreateWeapons()
    {
        //Damage, range, fireRate
        BenelliM4 = new Weapon(1, 1, 1);
        M4 = new Weapon(1, 1, 1);
        M110 = new Weapon(1, 1, 1);
        M249 = new Weapon(1, 1, 1);
        MP5 = new Weapon(1, 1, 1);
        SMAW = new Weapon(1, 1, 1);
    }
}
