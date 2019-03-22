using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerShootingController : NetworkBehaviour
{
    
    //References
    public PlayerWeapons playerWeapon;
    [SerializeField] Camera playerCamera;

    private void Start()
    {
        //Verifying that there is a camera referenced for the player to perform the raycast
        if (playerCamera == null)
            throw new System.NullReferenceException("PlayerShootingController: playerCamera missing");
    }

    private void Update()
    {
        //Fire1 is by default left ctrl in the input manager, changed for mouse 0 button in the project settings
        if (Input.GetButtonDown("Fire1"))
            Fire();
    }

    //Raycast to hit targets, only used locally, therefore marked as "Client"
    [Client]
    void Fire()
    {
        RaycastHit raycastHit;
        Vector3 raycastOrigin = playerCamera.transform.position;
        Vector3 raycastDirection = playerCamera.transform.forward;
        int raycastMask = LayerMask.GetMask("Ennemy");

        if (Physics.Raycast(raycastOrigin, raycastDirection, out raycastHit, playerWeapon.weaponRange, raycastMask))
        {
            //Checks if an ennemy has been hit
            //if (raycastHit.transform.gameObject.layer == LayerMask.NameToLayer("Ennemy"))
                //CmdEnnemyShot(raycastHit);

        }
    }

    //Server only method, therefore marked as "Command"
    /*
    [Command]
    void CmdEnnemyShot(RaycastHit hit)
    {
        Debug.Log(hit.collider.name + " was hit");
        //hit.rigidbody.GetComponent<Ennemy>().TakeDamage;
    }*/
    
}
