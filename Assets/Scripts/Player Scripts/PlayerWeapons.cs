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
        public int WeaponClipSize { get; private set; }
        public int WeaponAmmoInClip { get; set; }
        public bool IsUnlocked { get; private set; }

        public Weapon(int weaponDamage, int weaponRange, float weaponFireRate, int weaponClipSize)
        {
            WeaponDamage = weaponDamage;
            WeaponRange = weaponRange;
            WeaponFireRate = weaponFireRate;
            WeaponClipSize = weaponClipSize;
            WeaponAmmoInClip = weaponClipSize; //Starts with full mag
        }
    }

    //Weapons
    public static Weapon BenelliM4 = new Weapon(1, 1, 1,8); //semi-auto
    public static Weapon M4 = new Weapon(25, 120, 5,30); //auto
    public static Weapon M110 = new Weapon(1, 1, 1,10); //semi-auto
    public static Weapon M249 = new Weapon(1, 1, 1,30); //auto
    public static Weapon MP5 = new Weapon(1, 1, 1,30); //auto
    public static Weapon SMAW = new Weapon(1, 1, 1,1); //Rocket-launcher... lel
}
