using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class VendingMachineController : NetworkBehaviour
{
    [SerializeField] GameObject WeaponsMenu;
    GameObject currentWeaponsMenu { get; set; }
    PlayerWeaponManager playerWeaponsManager { get; set; }

    Button BuyBtnM4 { get; set; }
    Button BuyBtnM4A1 { get; set; }
    Button BuyBtnM249 { get; set; }
    Button BuyBtnM110 { get; set; }
    Button BuyBtnSMAW { get; set; }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetReferences()
    {

    }

    void BuyWeapon(PlayerWeapons.Weapon weaponBought, Button button)
    {
        weaponBought.IsUnlocked = true;
        button.gameObject.SetActive(false);
    }

    void BuyWeapon(int weaponID)
    {
        Button buttonToDeactivate = null;
        switch (weaponID)
        {
            case 1:
                buttonToDeactivate = BuyBtnM4;
                break;
            case 2:
                buttonToDeactivate = BuyBtnM4A1;
                break;
            case 3:
                buttonToDeactivate = BuyBtnM249;
                break;
            case 4:
                buttonToDeactivate = BuyBtnM110;
                break;
            case 5:
                buttonToDeactivate = BuyBtnSMAW;
                break;
        }
        BuyWeapon(playerWeaponsManager.Weapons[weaponID], buttonToDeactivate);
    }
    
    public void PlayerEntered(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerNetworking>().isLocalPlayer && other.gameObject.GetComponent<PlayerController>() != null)
            CreateMenu();
    }

    public void PlayerLeft(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerNetworking>().isLocalPlayer && other.gameObject.GetComponent<PlayerController>() != null)
            Destroy(currentWeaponsMenu);
    }

    void CreateMenu()
    {
        currentWeaponsMenu = Instantiate(WeaponsMenu, GameObject.Find("PlayerUI").transform);

        PlayerUI playerUI = GameObject.Find("PlayerUI").GetComponent<PlayerUI>();
        playerWeaponsManager = playerUI.WeaponManager;

        BuyBtnM4 = currentWeaponsMenu.transform.Find("BuyBtnM4").GetComponent<Button>();
        BuyBtnM4A1 = currentWeaponsMenu.transform.Find("BuyBtnM4A1").GetComponent<Button>();
        BuyBtnM249 = currentWeaponsMenu.transform.Find("BuyBtnM249").GetComponent<Button>();
        BuyBtnM110 = currentWeaponsMenu.transform.Find("BuyBtnM110").GetComponent<Button>();
        BuyBtnSMAW = currentWeaponsMenu.transform.Find("BuyBtnSMAW").GetComponent<Button>();

        BuyBtnM4.onClick.AddListener(() => BuyWeapon(1));
        BuyBtnM4A1.onClick.AddListener(() => BuyWeapon(2));
        BuyBtnM249.onClick.AddListener(() => BuyWeapon(3));
        BuyBtnM110.onClick.AddListener(() => BuyWeapon(4));
        BuyBtnSMAW.onClick.AddListener(() => BuyWeapon(5));

        if (playerWeaponsManager.Weapons[1].IsUnlocked)
            Destroy(BuyBtnM4.gameObject);
        if (playerWeaponsManager.Weapons[2].IsUnlocked)
            Destroy(BuyBtnM4A1.gameObject);
        if (playerWeaponsManager.Weapons[3].IsUnlocked)
            Destroy(BuyBtnM249.gameObject);
        if (playerWeaponsManager.Weapons[4].IsUnlocked)
            Destroy(BuyBtnM110.gameObject);
        if (playerWeaponsManager.Weapons[5].IsUnlocked)
            Destroy(BuyBtnSMAW.gameObject);
    }
}
