using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AD1815
{
    public class ShooterScript : MonoBehaviour
    //Shooter Script, attach this script to player ship.
    //Very simple script, clicking left mouse button will instantiate and turn the gameObject towards where your mouse pointer is, when it creates a projectile. Another script will then give it properties, such as speed/velocity which pushes it forwards.
    //I will use projectiles themselves, with their own scripts to differentiate them from other projectiles.

    //Remember to change playerCamera perspective from perspective to ortographic, in order for this to work. We don't need perspective camera in our project, since it's going to be 2D hybrid.
    //https://answers.unity.com/questions/1218955/comparing-orthographic-and-perspective-cameras.html for more information.
    {
        [SerializeField]
        Camera playerCamera;
        [SerializeField]
        GameObject playerProjectile;

        //Different bools to create states, which seperate different weapons from another. I.e. You can't use lasers when machineguns are equipped etc.
        bool machinegunsAreEquipped;
        bool lasersAreEquipped;
        bool missilesAreEquipped;
        // Start is called before the first frame update
        void OnGUI()
        {
            GUI.Label(new Rect(10, 10, 1000, 30), "Click Left Click to Shoot!"); //Comment this out if you don't want to see text on scene.
        }
        void Start()
        {
            playerCamera = Camera.main; //Setting Unity Main camera as playerCamera for this script.
            DefaultWeaponSetUp(); 
        }

        // Update is called once per frame
        void Update()
        {
            ShootProjectile();
        }
        void ShootProjectile()
        {
            //Okay this is a bit more complicated. This calculates position between your mouse in the unity world space and player position, calculates
            //angle between them and then turns it into quaternion, so we can spawn GameObject in correct angle, so it points towards the mouse pointer.
            //Camera needs to be ortographic in order for this to work. Otherwise you're probably better off using raycast. Worst case scenario, make another camera for use on flat calculations or the like on 3D games for this, if the situation requires it.
            if (Input.GetKeyDown(KeyCode.Mouse0))
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
            }
        }
        // Default Weapon Set up for start function.
        void DefaultWeaponSetUp() //Setting gameObjects that exist in resources folder in the fields manually, instead of going through editor.
                                  //Why? I am going to assume, these weapons are going to be switched a lot as equipment or weapons, can't do that through editor feasibly.
        {
            var projectile = Resources.Load("ResourcesPrefabs/TestProjectile") as GameObject; //setting projectile variable as GameObject.
            playerProjectile = projectile; //setting projectile variable as playerProjectile GameObject.
            machinegunsAreEquipped = true;
            Debug.Log("Default Weapon (Machinegun) Set Up!"); // Testing stuff.
        }
        //Preliminary Set Up, nothing meaningful below this... yet!
        void ChangeWeaponToMachineGun()
        {
            lasersAreEquipped = false;
            missilesAreEquipped = false;
            machinegunsAreEquipped = true;
            var projectile = Resources.Load("ResourcesPrefabs/TestProjectile") as GameObject;
            //var cubePrefab = Resources.Load("Prefabs/PrefabCube") as GameObject;
            playerProjectile = projectile;
            Debug.Log("Machinegun Equipped!");
        }

        void ChangeWeaponToMissile()
        {
            machinegunsAreEquipped = false;
            lasersAreEquipped = false;
            missilesAreEquipped = true;
            Debug.Log("Missiles Equipped!");
            var projectile = Resources.Load("ResourcesPrefabs/TestProjectile") as GameObject;
            //var cubePrefab = Resources.Load("Prefabs/PrefabCube") as GameObject;
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
    } 
}

