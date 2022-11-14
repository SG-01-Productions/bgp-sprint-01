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
    [SerializeField] float missileTurnrate = 0.01f;
    float selfDestroyDelay;
    bool isDestroyed;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioClip = Resources.Load("MissileExplosionSound") as AudioClip;
        audioSource.clip = audioClip;
        audioSource.volume = 0.25f;

        damage = 5000; //Insert amount of damage here. If you want to test stuff, leave damage at zero.
        speed = 500; //Insert amount of speed here. How fast the projectile travels.
        selfDestroyDelay = 5; //Times it takes for the projectile to destroy itself, Default 5 seconds;
        Invoke("DestroySelf", selfDestroyDelay);
        targetingCollider = gameObject.AddComponent<SphereCollider>();
        targetingCollider.isTrigger = true;
        MissileTargetingRoutine();
        
    }

    // Update is called once per frame
    void Update()
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
        isDestroyed = true;
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
    void MissileTargetingRoutine()
    {
        targetingCollider.enabled = true;
        PingEnemy();

        void PingEnemy()
        {
            targetingCollider.radius += 10f;
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Asteroid") || collision.gameObject.CompareTag("FieldAsteroid") && targetingCollider.enabled == false)  // Better identifier now. If object has enemy tag, this will work on all entities, that have Enemy and Asteroid tag.
        {
            if (collision.gameObject.transform == targetTransform)
            {
                collision.gameObject.GetComponent<HealthManager>().ReceiveDamage(damage);
                DestroySelf();
            }
            else
            {
                targetTransform = collision.gameObject.GetComponent<Transform>();
            }
            //Debug.Log("Missile hit Enemy Target! " + collision.name);
        }
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Asteroid") || collision.gameObject.CompareTag("FieldAsteroid") && targetingCollider.enabled == true)
        {
            targetingCollider.enabled = false;
            targetingCollider.radius = 0;
            targetTransform = collision.gameObject.GetComponent<Transform>();
            //Debug.Log("Missile has detected an enemy! " + collision.name + " It's location is at " + targetTransform.position);
            
        }
    }
    //Much better to implemethis with enabling and disabling of components, rather than destroy object and create a DeathSoundPlayer
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
