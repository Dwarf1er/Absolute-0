using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachineController : MonoBehaviour
{
    [SerializeField]
    GameObject WeaponsUI;
    WeaponsStoreController WeaponsStore { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        WeaponsStore = WeaponsUI.GetComponent<WeaponsStoreController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Est dedans");
        WeaponsStore.SetTrigger(true);
    }

}
