using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(PlayerWeaponManager))]
public class PlayerShootingController : NetworkBehaviour
{
    //References
    Animator animator;
    PlayerWeaponManager playerWeaponManager;
    public PlayerWeapons.Weapon equipedWeapon;
    [SerializeField]
    Camera playerCamera;

    float timeSinceLastShot { get; set; }

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
        timeSinceLastShot += Time.deltaTime;

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
        {
            if (equipedWeapon.WeaponClipSize == 1)
                animator.SetTrigger("ReloadRocket");
            else
                animator.SetTrigger("Reload");

            transform.Find("ReloadSFX").GetComponent<AudioSource>().Play();
        }


        if (Input.GetButtonDown("Fire1") && equipedWeapon.WeaponAmmoInClip == 0)
            transform.Find("DryFireSFX").GetComponent<AudioSource>().Play();

        if (equipedWeapon.IsAuto)
        {
            //Fire1 is by default left ctrl in the input manager, changed for mouse 0 button in the project settings
            if (Input.GetButton("Fire1") && equipedWeapon.WeaponAmmoInClip > 0 && timeSinceLastShot > equipedWeapon.WeaponFireRate)
            {
                Fire();
                timeSinceLastShot = 0;
                playerWeaponManager.currentPlayerWeaponModel.GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            //Fire1 is by default left ctrl in the input manager, changed for mouse 0 button in the project settings
            if (Input.GetButtonDown("Fire1") && equipedWeapon.WeaponAmmoInClip > 0 && timeSinceLastShot > equipedWeapon.WeaponFireRate)
            {
                Fire();
                timeSinceLastShot = 0;
                playerWeaponManager.currentPlayerWeaponModel.GetComponent<AudioSource>().Play();
            }
        }
        
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

        if (Physics.Raycast(raycastOrigin, raycastDirection, out raycastHit, equipedWeapon.WeaponRange, raycastMask))
        {
            //Checks if an ennemy has been hit
            if (raycastHit.transform.gameObject.layer == LayerMask.NameToLayer("Ennemy"))
            {
                GameObject ennemyHit = raycastHit.transform.gameObject;
                bool isHeadshot = raycastHit.collider.name == "Head Collider";

                if (isHeadshot)
                    CmdEnnemyHeadshot(ennemyHit, equipedWeapon.WeaponDamage);
                else
                    CmdEnnemyShot(ennemyHit, equipedWeapon.WeaponDamage);
            }
        }

        //Removes one bullet after each click
        equipedWeapon.WeaponAmmoInClip -= 1;
    }

    //Server only method, therefore marked as "Command"
    [Command]
    void CmdEnnemyShot(GameObject ennemyHit, int damage)
    {
        Debug.Log(ennemyHit.name + " was hit");
        ennemyHit.GetComponent<Ennemy>().CmdTakeDamage(damage);
    }

    [Command]
    void CmdEnnemyHeadshot(GameObject ennemyHit, int damage)
    {
        Debug.Log(ennemyHit.name + " was hit");
        ennemyHit.GetComponent<Ennemy>().CmdTakeHeadshot(damage);
    }

    //Refill magazine
    public void Reload()
    {
        equipedWeapon.WeaponAmmoInClip = equipedWeapon.WeaponClipSize;
    }
}
