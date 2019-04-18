using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class VendingMachineController : NetworkBehaviour
{
    GameObject WeaponsMenu { get; set; }
    PlayerWeaponManager playerWeaponsManager { get; set; }

    Button BuyBtnM4 { get; set; }
    Button BuyBtnM4A1 { get; set; }
    Button BuyBtnM249 { get; set; }
    Button BuyBtnM110 { get; set; }
    Button BuyBtnSMAW { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        WeaponsMenu = GameObject.Find("WeaponsMenu").gameObject;
        
        BuyBtnM4 = WeaponsMenu.transform.Find("BuyBtnM4").GetComponent<Button>();
        BuyBtnM4A1 = WeaponsMenu.transform.Find("BuyBtnM4A1").GetComponent<Button>();
        BuyBtnM249 = WeaponsMenu.transform.Find("BuyBtnM249").GetComponent<Button>();
        BuyBtnM110 = WeaponsMenu.transform.Find("BuyBtnM110").GetComponent<Button>();
        BuyBtnSMAW = WeaponsMenu.transform.Find("BuyBtnSMAW").GetComponent<Button>();

        WeaponsMenu.SetActive(false);
    }

    public void SetReferences()
    {
        PlayerUI playerUI = GameObject.Find("PlayerUI").GetComponent<PlayerUI>();
        playerWeaponsManager = playerUI.WeaponManager;


        //BuyBtnM4.onClick.AddListener(() => Debug.Log("TA TU VU JAI CLIQUÉ TABARNAK"));
        BuyBtnM4.onClick.AddListener(() => BuyWeapon(1));
        BuyBtnM4A1.onClick.AddListener(() => BuyWeapon(playerWeaponsManager.Weapons[2], BuyBtnM4A1));
        BuyBtnM249.onClick.AddListener(() => BuyWeapon(playerWeaponsManager.Weapons[3], BuyBtnM249));
        BuyBtnM110.onClick.AddListener(() => BuyWeapon(playerWeaponsManager.Weapons[4], BuyBtnM110));
        BuyBtnSMAW.onClick.AddListener(() => BuyWeapon(playerWeaponsManager.Weapons[5], BuyBtnSMAW));
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
        {
            WeaponsMenu.SetActive(true);
            SetReferences();
        }
    }

    public void PlayerLeft(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerNetworking>().isLocalPlayer && other.gameObject.GetComponent<PlayerController>() != null)
        {
            WeaponsMenu.SetActive(false);
        }
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerNetworking>().isLocalPlayer && other.gameObject.GetComponent<PlayerController>() != null)
        {
            WeaponsMenu.SetActive(true);
            SetReferences();
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerNetworking>().isLocalPlayer && other.gameObject.GetComponent<PlayerController>() != null)
        {
            WeaponsMenu.SetActive(false);
        }
    }
    */

}
