using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShooterScript : MonoBehaviour
//Shooter Script, attach this script to player ship.
//Very simple script, clicking left mouse button will instantiate and turn the gameObject towards where your mouse pointer is, when it creates a projectile. Another script will then give it properties, such as speed/velocity which pushes it forwards.
//I will use projectiles themselves, with their own scripts to differentiate them from other projectiles.

//Remember to change playerCamera perspective from perspective to ortographic, in order for this to work. We don't need perspective camera in our project, since it's going to be 2D hybrid.
//https://answers.unity.com/questions/1218955/comparing-orthographic-and-perspective-cameras.html for more information.
{
    [SerializeField] Camera playerCamera;
    [SerializeField] GameObject playerProjectile;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip weaponShootSound;
    float firerate;

    //Different bools to create states, which seperate different weapons from another. I.e. You can't use lasers when machineguns are equipped etc.
    bool machinegunsAreEquipped;
    bool lasersAreEquipped;
    bool missilesAreEquipped;

    bool shootCooldown;

    // Start is called before the first frame update
    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 1000, 30), "Click Left Click to Shoot! Press 1 to switch to blasters. 2 switch to Missiles"); //Comment this out if you don't want to see text on scene.
    }
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = weaponShootSound;
        audioSource.volume = 0.5f;
        playerCamera = Camera.main; //Setting Unity Main camera as playerCamera for this script.
        ChangeWeaponToChainBlaster();
        firerate = 0.05f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && shootCooldown == false)
        {
            ShootProjectile();
            StartCoroutine(ShootCooldown());
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeWeaponToChainBlaster();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeWeaponToMissile();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeWeaponToLaser();
        }

    }
    void ShootProjectile()
    {
        //Okay this is a bit more complicated. This calculates position between your mouse in the unity world space and player position, calculates
        //angle between them and then turns it into quaternion, so we can spawn GameObject in correct angle, so it points towards the mouse pointer.
        //Camera needs to be ortographic in order for this to work. Otherwise you're probably better off using raycast. Worst case scenario, make another camera for use on flat calculations or the like on 3D games for this, if the situation requires it.

        if (machinegunsAreEquipped == true)
        {
            Vector3 mousePos = Input.mousePosition;
            Vector2 mouseScreenPosition = playerCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, playerCamera.nearClipPlane));

            float mousePosX = mouseScreenPosition.x;
            float mousePosY = mouseScreenPosition.y;
            float playerPosX = GetComponentInParent<Transform>().position.x;
            float playerPosY = GetComponentInParent<Transform>().position.y;

            Vector2 Point_1 = new Vector2(mousePosX, mousePosY);
            Vector2 Point_2 = new Vector2(playerPosX, playerPosY);
            float rotation = Mathf.Atan2(Point_2.y - Point_1.y, Point_2.x - Point_1.x) * Mathf.Rad2Deg;
            Vector3 projectileStartRotation = new Vector3(0f, 0f, rotation + 90);
            Quaternion quaternion = Quaternion.Euler(projectileStartRotation);

            Instantiate(playerProjectile, transform.position, quaternion);
            audioSource.PlayOneShot(weaponShootSound); 
        }
        if (missilesAreEquipped == true)
        {
            Quaternion startingRotation = GetComponentInParent<CharControl>().transform.rotation;
            Instantiate(playerProjectile, transform.position, startingRotation);
            audioSource.PlayOneShot(weaponShootSound, 0.25f);
        }
    }
    //Preliminary Set Up, nothing meaningful below this... yet!
    void ChangeWeaponToChainBlaster()
    {
        lasersAreEquipped = false;
        missilesAreEquipped = false;
        machinegunsAreEquipped = true;
        Debug.Log("ChainBlaster Equipped!");
        var projectile = Resources.Load("ResourcesPrefabs/ChainBlasterProjectile") as GameObject;
        //var cubePrefab = Resources.Load("Prefabs/PrefabCube") as GameObject;
        playerProjectile = projectile;
        firerate = 0.05f; //Setting firerate.
        weaponShootSound = Resources.Load("ChainBlasterSound") as AudioClip;
    }
    void ChangeWeaponToMissile()
    {
        machinegunsAreEquipped = false;
        lasersAreEquipped = false;
        missilesAreEquipped = true;
        Debug.Log("Missiles Equipped!");
        var projectile = Resources.Load("ResourcesPrefabs/MissileProjectile") as GameObject;
        playerProjectile = projectile;
        firerate = 2f; //Setting firerate.
        weaponShootSound = Resources.Load("MissileLaunchSound") as AudioClip;
    }
    void ChangeWeaponToLaser()
    {
        machinegunsAreEquipped = false;
        missilesAreEquipped = false;
        lasersAreEquipped = true;
        Debug.Log("Lasers Equipped!");
        var projectile = Resources.Load("ResourcesPrefabs/TestProjectile") as GameObject;
        //var cubePrefab = Resources.Load("Prefabs/PrefabCube") as GameObject;
    }
    IEnumerator ShootCooldown()
    {
        shootCooldown = true;
        yield return new WaitForSeconds(firerate);
        shootCooldown = false;
    }
}