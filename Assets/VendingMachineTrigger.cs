using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachineTrigger : MonoBehaviour
{
    VendingMachineController parentController { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        parentController = GetComponentInParent<VendingMachineController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        parentController.PlayerEntered(other);
    }

    private void OnTriggerExit(Collider other)
    {
        parentController.PlayerLeft(other);
    }
}
