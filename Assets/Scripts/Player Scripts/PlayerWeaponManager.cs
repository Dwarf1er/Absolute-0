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
    PlayerShootingController ShootingController { get; set; }
    private PlayerWeapons.Weapon defaultPlayerWeapon;

    public PlayerWeapons.Weapon currentPlayerWeapon;
    GameObject currentPlayerWeaponModel;

    GameObject[] WeaponModels { get; set; }
    PlayerWeapons.Weapon[] Weapons { get; set; }

    void Start()
    {
        ShootingController = GetComponent<PlayerShootingController>();
        defaultPlayerWeapon = PlayerWeapons.M4;

        WeaponModels = new GameObject[] { BenelliM4Model, M4Model, M110Model, M249Model, MP5Model, SMAWModel };
        Weapons = new PlayerWeapons.Weapon[] { PlayerWeapons.BenelliM4, PlayerWeapons.M4, PlayerWeapons.M110, PlayerWeapons.M249, PlayerWeapons.MP5, PlayerWeapons.SMAW };

        currentPlayerWeaponModel = M4Model;
        
        EquipNextWeapon(defaultPlayerWeapon, currentPlayerWeaponModel);
    }

    void Update()
    {
        
    }

    //Gets the current weapon to be the new weapon for the player
    void EquipNextWeapon (PlayerWeapons.Weapon newWeapon, GameObject newWeaponModel)
    {
        currentPlayerWeaponModel.SetActive(false);

        currentPlayerWeapon = newWeapon;
        currentPlayerWeaponModel = newWeaponModel;

        currentPlayerWeaponModel.SetActive(true);

        //Keeps the good amount of ammo after swaps
        ShootingController.currentAmmoInClip = currentPlayerWeapon.WeaponClipSize;
    }

    public void EquipNextWeapon (int weaponID)
    {
        EquipNextWeapon(Weapons[weaponID], WeaponModels[weaponID]);
    }

    //Allows to get a reference to the currently equipped weapon of a LocalPlayer
    public PlayerWeapons.Weapon GetCurrentPlayerWeapon ()
    {
        return currentPlayerWeapon;
    }
}
