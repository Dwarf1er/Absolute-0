using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(PlayerWeaponManager))]
public class PlayerShootingController : NetworkBehaviour
{/*
    
    //References
    public PlayerWeapons currentPlayerWeapon;
    PlayerWeaponManager playerWeaponManager;
    [SerializeField]
    Camera playerCamera;

    private void Start()
    {
        //Verifying that there is a camera referenced for the player to perform the raycast
        if (playerCamera == null)
            throw new System.NullReferenceException("PlayerShootingController: playerCamera missing");

        //Gets the reference to the PlayerWeaponManager
        playerWeaponManager = GetComponent<PlayerWeaponManager>();
    }

    private void Update()
    {
        //Gets the current player weapon from the playerWeaponManager
        currentPlayerWeapon = playerWeaponManager.GetCurrentPlayerWeapon();
        
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

        if (Physics.Raycast(raycastOrigin, raycastDirection, out raycastHit, currentPlayerWeapon.weaponRange, raycastMask))
        {
            //Checks if an ennemy has been hit
            if (raycastHit.transform.gameObject.layer == LayerMask.NameToLayer("Ennemy"))
            {
                GameObject ennemyHit = raycastHit.transform.gameObject;
                CmdEnnemyShot(ennemyHit, currentPlayerWeapon.weaponDamage);
            }
        }
    }

    //Server only method, therefore marked as "Command"
    [Command]
    void CmdEnnemyShot(GameObject ennemyHit, int damage)
    {
        Debug.Log(ennemyHit.name + " was hit");
        ennemyHit.GetComponent<Ennemy>().TakeDamage(damage);
    }
*/  
}
