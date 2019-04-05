using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(PlayerWeaponManager))]
public class PlayerShootingController : NetworkBehaviour
{
    //References
    Animator animator;
    public int currentAmmoInClip;
    PlayerWeaponManager playerWeaponManager;
    [SerializeField]
    Camera playerCamera;

    private void Start()
    {
        //Verifying that there is a camera referenced for the player to perform the raycast
        if (playerCamera == null)
            throw new System.NullReferenceException("PlayerShootingController: playerCamera missing");

        //Get references in the player's game object
        animator = GetComponent<Animator>();
        playerWeaponManager = GetComponent<PlayerWeaponManager>();
    }
    
    private void Update()
    {
        //Weapon swapping through keys 1 to 6 (alphanumeric != numpad)
        if (Input.GetKeyDown(KeyCode.Alpha1))
            playerWeaponManager.EquipNextWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            playerWeaponManager.EquipNextWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            playerWeaponManager.EquipNextWeapon(2);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            playerWeaponManager.EquipNextWeapon(3);
        if (Input.GetKeyDown(KeyCode.Alpha5))
            playerWeaponManager.EquipNextWeapon(4);
        if (Input.GetKeyDown(KeyCode.Alpha6))
            playerWeaponManager.EquipNextWeapon(5);
        if (Input.GetKeyDown(KeyCode.R))
            animator.SetTrigger("Reload");
        
        //Fire1 is by default left ctrl in the input manager, changed for mouse 0 button in the project settings
        if (Input.GetButtonDown("Fire1") && currentAmmoInClip > 0)
            Fire();
    }

    //Raycast to hit targets, only used locally, therefore marked as "Client"
    [Client]
    void Fire()
    {
        animator.SetTrigger("Shoot");
        RaycastHit raycastHit;
        Vector3 raycastOrigin = playerCamera.transform.position;
        Vector3 raycastDirection = playerCamera.transform.forward;
        int raycastMask = LayerMask.GetMask("Ennemy", "Environment");

        if (Physics.Raycast(raycastOrigin, raycastDirection, out raycastHit, playerWeaponManager.currentPlayerWeapon.WeaponRange, raycastMask))
        {
            //Checks if an ennemy has been hit
            if (raycastHit.transform.gameObject.layer == LayerMask.NameToLayer("Ennemy"))
            {
                GameObject ennemyHit = raycastHit.transform.gameObject;
                CmdEnnemyShot(ennemyHit, playerWeaponManager.currentPlayerWeapon.WeaponDamage);
            }
        }

        //Removes one bullet after each click
        currentAmmoInClip -= 1;
    }

    //Server only method, therefore marked as "Command"
    [Command]
    void CmdEnnemyShot(GameObject ennemyHit, int damage)
    {
        Debug.Log(ennemyHit.name + " was hit");
        ennemyHit.GetComponent<Ennemy>().TakeDamage(damage);
    }

    //Refill magazine
    public void Reload()
    {
        currentAmmoInClip = playerWeaponManager.currentPlayerWeapon.WeaponClipSize;
    }
}
