using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MissileProjectile : MonoBehaviour
//This script will give speed to the projectile and decide how much damage it does.
//Should be attached to the projectile. It should already be attached.
{
    SphereCollider targetingCollider;
    Transform targetTransform;
    AudioSource audioSource;
    AudioClip audioClip;

    [SerializeField] float damage;
    [SerializeField] float speed;
    [SerializeField] float missileTurnrate;
    float selfDestroyDelay;
    bool isDestroyed;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioClip = Resources.Load("MissileExplosionSound") as AudioClip;
        audioSource.clip = audioClip;
        audioSource.volume = 0.5f;

        missileTurnrate = 0.025f;
        damage = 5000; //Insert amount of damage here. If you want to test stuff, leave damage at zero.
        speed = 500; //Insert amount of speed here. How fast the projectile travels.
        selfDestroyDelay = 5; //Times it takes for the projectile to destroy itself, Default 5 seconds;
        Invoke("DestroySelf", selfDestroyDelay);
        targetingCollider = gameObject.AddComponent<SphereCollider>();
        targetingCollider.isTrigger = true;
        MissileTargetingRoutine();
        
    }

    // Update is called once per frame
    void FixedUpdate() 
        // REALLY big difference in missile turnrate, depending if this script run on Regular update or fixed update. Fixed Update is better, because it is always 50 frames per second (50 times second), no matter what.
        //Update is all over the place, difference in WebGL implementation is most apparent.
    {
        if (isDestroyed == false) //Checking if the missile has been destroyed, so we can do stuff after "destruction"
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0f); // So missile forever stays in 0 in z axis. Hack, so Quaternion.Slerp stays in 0 position in Z axis
            if (targetTransform == null)
            {
                MissileTargetingRoutine();
                //Debug.Log("Starting MissileTargetingRoutine");
                transform.Translate(new Vector3(0f, 0f, 1f) * speed * Time.deltaTime);
            }
            else if (targetTransform != null && !targetTransform.Equals(null)) //I.e We have found the enemy!
            {
                Vector2 enemyScreenPosition = targetTransform.position;

                float enemyPosX = enemyScreenPosition.x;
                float enemyPosY = enemyScreenPosition.y;
                float playerPosX = gameObject.transform.position.x;
                float playerPosY = gameObject.transform.position.y;

                Vector2 Point_1 = new Vector2(enemyPosX, enemyPosY);
                Vector2 Point_2 = new Vector2(playerPosX, playerPosY);
                float rotation = Mathf.Atan2(Point_2.y - Point_1.y, Point_2.x - Point_1.x) * Mathf.Rad2Deg;
                Vector3 projectileStartRotation = new Vector3(rotation, -90f, 0f);
                Quaternion quaternion = Quaternion.Euler(projectileStartRotation);
                transform.rotation = Quaternion.Slerp(transform.rotation, quaternion, missileTurnrate);

                //This moves projectile forwards.
                //Debug.Log("Missile is homing towards Enemy!");
                transform.Translate(speed * Time.deltaTime * new Vector3(0f, 0f, 1f));
            } 
        }
    }
    void DestroySelf()
    {
        if (isDestroyed == false)
        {
            isDestroyed = true;
            Collider[] colliders = GetComponents<Collider>(); //Gets all colliders from  gameObject and makes them into an array.
            foreach (Collider collider in colliders) // disables all colliders, in the array. To prevent colliders destroying objects, after the missile has been disabled
            {
                collider.enabled = false;
            }
            gameObject.transform.Find("missile").gameObject.SetActive(false);
            gameObject.transform.Find("fire").gameObject.SetActive(false);
            gameObject.GetComponentInChildren<ParticleSystem>().Stop();
            audioSource.Play();
            StartCoroutine(Countdown());
            IEnumerator Countdown()
            {
                yield return new WaitForSeconds(5f);
                Destroy(gameObject);
            } 
        }
    }
    void MissileTargetingRoutine()
    {
        targetingCollider.enabled = true;
        PingEnemy();

        void PingEnemy()
        {
            targetingCollider.radius += 20f;
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Asteroid") || collision.gameObject.CompareTag("FieldAsteroid") && targetingCollider.enabled == true)
        {
            targetingCollider.enabled = false;
            targetingCollider.radius = 0;
            targetTransform = collision.gameObject.GetComponent<Transform>();
            //Debug.Log("Missile has detected an enemy! " + collision.name + " It's location is at " + targetTransform.position);
            
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Asteroid") || other.gameObject.CompareTag("FieldAsteroid") && targetingCollider.enabled == false)
        {
            other.gameObject.GetComponent<HealthManager>().ReceiveDamage(damage);
            DestroySelf();
        }
    }
    //Much better to implement this with enabling and disabling of components, rather than destroy object and create a DeathSoundPlayer
    /*
    private void OnDestroy()
    {
        GameObject deathSoundPlayerObject = new GameObject("DeathSoundPlayer");
        deathSoundPlayerObject.AddComponent<DelayedSelfDestroyerScript>();
        AudioSource audioSource = deathSoundPlayerObject.AddComponent<AudioSource>();
        AudioClip audioClip = Resources.Load("MissileExplosionSound") as AudioClip;
        audioSource.clip = audioClip;
        audioSource.volume = 0.25f;
        audioSource.Play();
        
    }
    */
}
