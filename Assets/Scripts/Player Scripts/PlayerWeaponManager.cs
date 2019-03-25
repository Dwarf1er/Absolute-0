using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerWeaponManager : NetworkBehaviour
{
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

    //References
    private PlayerWeapons.Weapon defaultPlayerWeapon = PlayerWeapons.M4;
    PlayerWeapons.Weapon currentPlayerWeapon;
    GameObject currentPlayerWeaponModel;


    void Start()
    {
        currentPlayerWeaponModel = M4Model;
        EquipNextWeapon(defaultPlayerWeapon, currentPlayerWeaponModel);
    }

    void Update()
    {
        //Weapon swapping through keys 1 to 6 (alphanumeric != numpad)
        if (Input.GetKeyDown(KeyCode.Alpha1))
            EquipNextWeapon(PlayerWeapons.BenelliM4, BenelliM4Model);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            EquipNextWeapon(PlayerWeapons.M4, M4Model);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            EquipNextWeapon(PlayerWeapons.M110, M110Model);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            EquipNextWeapon(PlayerWeapons.M249, M249Model);
        if (Input.GetKeyDown(KeyCode.Alpha5))
            EquipNextWeapon(PlayerWeapons.MP5, MP5Model);
        if (Input.GetKeyDown(KeyCode.Alpha6))
            EquipNextWeapon(PlayerWeapons.SMAW, SMAWModel);
    }

    //Gets the current weapon to be the new weapon for the player
    void EquipNextWeapon (PlayerWeapons.Weapon newWeapon, GameObject newWeaponModel)
    {
        currentPlayerWeaponModel.SetActive(false);

        currentPlayerWeapon = newWeapon;
        currentPlayerWeaponModel = newWeaponModel;

        currentPlayerWeaponModel.SetActive(true);
    }

    //Allows to get a reference to the currently equipped weapon of a LocalPlayer
    public PlayerWeapons.Weapon GetCurrentPlayerWeapon ()
    {
        return currentPlayerWeapon;
    }
}
