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
        public bool IsUnlocked { get; set; }
        public bool IsAuto { get; private set; }

        public Weapon(int weaponDamage, int weaponRange, float weaponFireRate, int weaponClipSize, bool isAuto)
        {
            WeaponDamage = weaponDamage;
            WeaponRange = weaponRange;
            WeaponFireRate = weaponFireRate;
            WeaponClipSize = weaponClipSize;
            WeaponAmmoInClip = weaponClipSize; //Starts with full mag
            IsAuto = isAuto;
        }
    }

    //Weapons
    public static Weapon BenelliM4 = new Weapon(60, 100, 0.6f, 8, false); //semi-auto, 100 RPM   //To be added later, will fire 10 pellets for 10 dmg each
    public static Weapon M4 = new Weapon(35, 100, 0.1f, 30, true); //auto, 600 RPM
    public static Weapon M110 = new Weapon(80, 100, 0.8f, 10, false); //semi-auto, 100 RPM
    public static Weapon M249 = new Weapon(25, 100, 0.07f, 45, true); //auto, 850 RPM
    public static Weapon MP5 = new Weapon(20, 100, 0.075f, 30, true); //auto, 800 RPM
    public static Weapon SMAW = new Weapon(0, 100, 1, 1, false); //N/A  //To be added later, will fire a rocket for 80 dmg
}
