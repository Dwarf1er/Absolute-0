using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    //References
    [SerializeField]
    public RectTransform hpBar;
    [SerializeField]
    Text ammoInClipTxt;
    [SerializeField]
    Text maxAmmoTxt;
    [SerializeField]
    Text healthTxt;

    PlayerStats Player { get; set; }
    PlayerController Controller { get; set; }
    public PlayerWeaponManager WeaponManager { get; set; }
    

    //Getting access to the stats of our player
    public void SetPlayer (PlayerStats player)
    {
        Player = player;
        Controller = Player.GetComponent<PlayerController>();
        WeaponManager = Player.GetComponent<PlayerWeaponManager>();

        player.gameObject.GetComponent<PlayerUtilities>().playerUI = gameObject;
    }

    void Update()
    {
        SetAmmoAmount(WeaponManager.currentPlayerWeapon.WeaponAmmoInClip, WeaponManager.currentPlayerWeapon.WeaponClipSize);
        SetHpAmmount();
    }

    /*public void SetHpValue(float hpPercentage)
    {
        //Changes the scale of the bar on the X axis to match the graphic's orientation
        if (hpPercentage == 0)
            return;
        hpBar.localScale = new Vector3(hpPercentage, 1f, 1f);
    }*/

    void SetAmmoAmount(int ammoInClip, int maxAmmo)
    {
        ammoInClipTxt.text = ammoInClip.ToString();
        maxAmmoTxt.text = maxAmmo.ToString();
    }

    void SetHpAmmount()
    {
        healthTxt.text = Player.currentHp.ToString();
    }
}
