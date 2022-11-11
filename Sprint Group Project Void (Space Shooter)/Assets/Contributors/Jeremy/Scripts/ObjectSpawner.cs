using System.Collections;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public static ObjectSpawner Instance {get; private set; }
    //I do not know with my knowledge why DestroyAsteroid Method in AsteroidHandler (Now in HealthManager) cannot access this. *Sami*
    //We need to either figure this out, or implement it differently, in a way we can access it.

    //Error Message
    //NullReferenceException: Object reference not set to an instance of an object
    //HealthManager.DestroyAsteroid() (at Assets/Contributors/Sami/Scripts/HealthManager.cs:41)

    public Rigidbody asteroidPrefab1;
    private Rigidbody asteroid;
    private Coroutine spawnerCoroutine;
    internal float asteroidMass;
    internal int asteroidCount = 0;

    [SerializeField] private float minSpawnDistance = 350f;
    [SerializeField] private float maxSpawnDistance = 500f;
    [SerializeField] private float minVelocity = -50f;
    [SerializeField] private float maxVelocity = -250f;

    [SerializeField] private float minAsteroidSize = 20f;
    [SerializeField] private float maxAsteroidSize = 200f;

    [SerializeField] private float minAsteroidMass = 1f;
    [SerializeField] private float maxAsteroidMass = 100f;

    [SerializeField] private float asteroidHealthMultiplier = 50f;

    [SerializeField] private int maxAsteroidCount = 10;

    [SerializeField] private float asteroidSpawnInterval = 4f;

    void Start()
    {
        Instance = this;
        spawnerCoroutine = StartCoroutine(AsteroidSpawner());
    }

    private void SpawnAsteroid()
    {
        asteroid = Instantiate(asteroidPrefab1);
        Vector2 randomPosition = Random.insideUnitCircle;
        /*If shit hits the fan, this if statement saves the fan from being covered in shit.*/
        if (randomPosition == Vector2.zero)
        {
            randomPosition = Vector2.up;
        }

        var randomPositionScaled = randomPosition.normalized * Random.Range(minSpawnDistance, maxSpawnDistance);
        asteroid.transform.position = transform.position + new Vector3(randomPositionScaled.x, randomPositionScaled.y, 0f);
        asteroid.velocity = randomPosition.normalized * Random.Range(minVelocity, maxVelocity);
        /*The below code gives a random scale value between (1, 1, 1) and (10, 10, 10)*/
        float asteroidMass = Random.Range(0f, 1f);
        asteroid.transform.localScale = Vector3.Lerp(new Vector3(minAsteroidSize, minAsteroidSize, minAsteroidSize), new Vector3(maxAsteroidSize, maxAsteroidSize, maxAsteroidSize), asteroidMass);
        asteroid.mass = Mathf.Lerp(minAsteroidMass, maxAsteroidMass, asteroidMass);
        float asteroidHealth = asteroidMass * 100f * asteroidHealthMultiplier;
        asteroid.GetComponent<HealthManager>().SetAsteroidHealth(asteroidHealth);
    }



    /*This co-routine waits 4 seconds, calls the Spawn Asteroid method, then waits 4 seconds again before repeating the while loop. We can change wait for seconds to meet our conditions
     for spawning a new asteroid as needed.*/
    private IEnumerator AsteroidSpawner()
    {
        yield return new WaitForSeconds(asteroidSpawnInterval);
        while (true)
        {
            if (asteroidCount < maxAsteroidCount)
            {
                SpawnAsteroid();
                asteroidCount++;
            }
            if (asteroidCount > maxAsteroidCount)
            {
                StopCoroutine(spawnerCoroutine);
            }
            yield return new WaitForSeconds(asteroidSpawnInterval);
        }
    }

    /*No need to bounce the asteroids off the collider now that we have the coroutine, so this will destroy asteroids that miss the ship so we can spawn new ones.*/
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Asteroid"))
        {
            Destroy(other.gameObject);
            asteroidCount--;
        }
    }
}
