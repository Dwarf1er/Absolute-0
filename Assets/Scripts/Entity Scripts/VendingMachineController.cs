using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachineController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameObject PlayerUI = other.GetComponent<PlayerUtilities>().playerUI;
        GameObject WeaponsMenu = PlayerUI.transform.Find("WeaponsMenu").gameObject;
        WeaponsMenu.SetActive(true);

    }
    private void OnTriggerExit(Collider other)
    {
        GameObject PlayerUI = other.GetComponent<PlayerUtilities>().playerUI;
        GameObject WeaponsMenu = PlayerUI.transform.Find("WeaponsMenu").gameObject;
        WeaponsMenu.SetActive(false);
    }


}
