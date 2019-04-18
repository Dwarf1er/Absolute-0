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
    private PlayerWeapons.Weapon defaultPlayerWeapon;

    public PlayerWeapons.Weapon currentPlayerWeapon;
    public GameObject currentPlayerWeaponModel;

    GameObject[] WeaponModels { get; set; }
    public PlayerWeapons.Weapon[] Weapons { get; set; }

    void Start()
    {
        defaultPlayerWeapon = PlayerWeapons.MP5;

        WeaponModels = new GameObject[] { MP5Model, BenelliM4Model, M4Model, M249Model, M110Model, SMAWModel };
        Weapons = new PlayerWeapons.Weapon[] { PlayerWeapons.MP5, PlayerWeapons.BenelliM4, PlayerWeapons.M4, PlayerWeapons.M249, PlayerWeapons.M110, PlayerWeapons.SMAW };

        for (int cpt = 1; cpt < Weapons.Length; cpt++)
            Weapons[cpt].IsUnlocked = false;

        currentPlayerWeaponModel = MP5Model;
        
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

        GetComponent<PlayerShootingController>().equipedWeapon = newWeapon;
    }

    public void EquipNextWeapon (int weaponID)
    {
        if (Weapons[weaponID].IsUnlocked)
            EquipNextWeapon(Weapons[weaponID], WeaponModels[weaponID]);
    }

    //Allows to get a reference to the currently equipped weapon of a LocalPlayer
    public PlayerWeapons.Weapon GetCurrentPlayerWeapon ()
    {
        return currentPlayerWeapon;
    }
}
