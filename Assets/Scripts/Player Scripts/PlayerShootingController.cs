using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerWeaponManager))]
public class PlayerShootingController : NetworkBehaviour
{
    //References
    Animator animator;
    PlayerWeaponManager playerWeaponManager;
    public PlayerWeapons.Weapon equipedWeapon;
    public PlayerUI playerUI;

    [SerializeField]
    Camera playerCamera;
    [SerializeField]
    GameObject bulletPrefab;

    public bool isDead { get; set; }

    float timeSinceLastShot { get; set; }
    float currentRecoil { get; set; }

    private void Start()
    {
        //Verifying that there is a camera referenced for the player to perform the raycast
        if (playerCamera == null)
            throw new System.NullReferenceException("PlayerShootingController: playerCamera missing");

        //Get references in the player's game object
        animator = GetComponent<Animator>();
        playerWeaponManager = GetComponent<PlayerWeaponManager>();

        currentRecoil = 0;
    }
    
    private void Update()
    {
        if (isDead)
            return;

        timeSinceLastShot += Time.deltaTime;
        if (currentRecoil > 0)
        {
            currentRecoil -= Time.deltaTime * 45;
            if (currentRecoil < 0)
                currentRecoil = 0;
            playerUI.SetCrosshairScale(currentRecoil);
        }

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
                equipedWeapon.WeaponAmmoInClip -= 1;
                playerWeaponManager.currentPlayerWeaponModel.GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            //Fire1 is by default left ctrl in the input manager, changed for mouse 0 button in the project settings
            if (Input.GetButtonDown("Fire1") && equipedWeapon.WeaponAmmoInClip > 0 && timeSinceLastShot > equipedWeapon.WeaponFireRate)
            {
                if (equipedWeapon.WeaponClipSize == 8)
                {
                    for (int cpt = 0; cpt < 5; cpt++)
                        Fire();
                    equipedWeapon.WeaponAmmoInClip -= 1;
                    playerWeaponManager.currentPlayerWeaponModel.GetComponent<AudioSource>().Play();
                }
                else
                {
                    Fire();
                    equipedWeapon.WeaponAmmoInClip -= 1;
                    playerWeaponManager.currentPlayerWeaponModel.GetComponent<AudioSource>().Play();
                }
                    
            }
            
        }
        
    }

    //Raycast to hit targets, only used locally, therefore marked as "Client"
    [Client]
    void Fire()
    {
        if (SceneManager.GetActiveScene().name == "Lobby")
            return;

        animator.SetTrigger("Shoot");

        Vector3 gunMuzzlePosition = playerWeaponManager.currentPlayerWeaponModel.transform.Find("Muzzle").position;
        
        Vector3 raycastOrigin = playerCamera.transform.position;
        Vector3 raycastDirection = playerCamera.transform.forward;

        //Calculate Spread
        //Circle Coordinates: root(x^2 + y^2) = 1
        Vector2 circleCoordinate = Random.insideUnitCircle;
        //Spread degrees
        float spreadDegrees = Random.Range(0, currentRecoil);
        float xDegrees = spreadDegrees * circleCoordinate.x;
        float yDegrees = spreadDegrees * circleCoordinate.y;
        //Spread vector
        Vector3 spreadVectorX = playerCamera.transform.right * (raycastDirection.magnitude * Mathf.Tan(xDegrees * Mathf.Deg2Rad));
        Vector3 spreadVectorY = playerCamera.transform.up * (raycastDirection.magnitude * Mathf.Tan(yDegrees * Mathf.Deg2Rad));
        Vector3 modifiedRaycastDirection = raycastDirection + spreadVectorX + spreadVectorY;

        int raycastMask = LayerMask.GetMask("Ennemy", "Environment");
        RaycastHit raycastHit;

        if (Physics.Raycast(raycastOrigin, modifiedRaycastDirection, out raycastHit, equipedWeapon.WeaponRange, raycastMask))
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

        
        GameObject newBullet = Instantiate(bulletPrefab, gunMuzzlePosition, Quaternion.identity);
        Vector3 bulletDirection = raycastHit.point - gunMuzzlePosition;

        //If the player's shot doesn't hit anything
        if (raycastHit.point == Vector3.zero)
            bulletDirection = playerWeaponManager.currentPlayerWeaponModel.transform.Find("Muzzle").transform.up * -1;

        newBullet.GetComponent<Rigidbody>().AddForce(bulletDirection * 0.2f, ForceMode.Impulse);


        timeSinceLastShot = 0;
        currentRecoil += 5;
        playerUI.SetCrosshairScale(currentRecoil);
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
