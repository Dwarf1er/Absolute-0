using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerWeaponManager : NetworkBehaviour
{
    [SerializeField]
    private PlayerWeapons defaultPlayerWeapon;
    PlayerWeapons currentPlayerWeapon;

    void Start()
    {
        EquipNextWeapon(defaultPlayerWeapon);
    }

    void Update()
    {
        //Weapon swapping through keys 1 to 6 (alphanumeric != numpad)
        if (Input.GetKeyDown(KeyCode.Alpha1))
            EquipNextWeapon(PlayerWeapons.BenelliM4);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            EquipNextWeapon(PlayerWeapons.M4);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            EquipNextWeapon(PlayerWeapons.M110);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            EquipNextWeapon(PlayerWeapons.M249);
        if (Input.GetKeyDown(KeyCode.Alpha5))
            EquipNextWeapon(PlayerWeapons.MP5);
        if (Input.GetKeyDown(KeyCode.Alpha6))
            EquipNextWeapon(PlayerWeapons.SMAW);
    }

    //Gets the current weapon to be the new weapon for the player
    void EquipNextWeapon (PlayerWeapons newWeapon)
    {
        currentPlayerWeapon = newWeapon;

    }

    //Allows to get a reference to the currently equipped weapon of a LocalPlayer
    public PlayerWeapons GetCurrentPlayerWeapon ()
    {
        return currentPlayerWeapon;
    }
}
