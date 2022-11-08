using System.Collections;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{

    public Rigidbody asteroidPrefab1;
    private Rigidbody asteroid;
    private Coroutine spawnerCoroutine;
    private int asteroidCount = 0;

    void Start()
    {
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
        var randomPositionScaled = randomPosition.normalized * Random.Range(35f, 50f);
        asteroid.transform.position = transform.position + new Vector3(randomPositionScaled.x, randomPositionScaled.y, 0f);
        var randomOrientation = Random.insideUnitCircle;
        /*Same thing here but this time it's a ceiling fan.*/
        if (randomOrientation == Vector2.zero)
        {
            randomOrientation = Vector2.up;
        }
        asteroid.velocity = randomPosition.normalized * Random.Range(-1, -10);
        /*The below code gives a random scale value between (1, 1, 1) and (10, 10, 10)*/
        float asteroidMass = Random.Range(0f, 1f);
        asteroid.transform.localScale = Vector3.Lerp(new Vector3(1, 1, 1), new Vector3(10, 10, 10), asteroidMass);
        asteroid.mass = Mathf.Lerp(1, 100, asteroidMass);
    }



    /*This co-routine waits 4 seconds, calls the Spawn Asteroid method, then waits 4 seconds again before repeating the while loop. We can change wait for seconds to meet our conditions
     for spawning a new asteroid as needed.*/
    private IEnumerator AsteroidSpawner()
    {
        yield return new WaitForSeconds(4);
        while (true)
        {
            if (asteroidCount < 10)
            {
                SpawnAsteroid();
                asteroidCount++;
            }
            if (asteroidCount > 10)
            {
                StopCoroutine(spawnerCoroutine);
            }
            yield return new WaitForSeconds(4);
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
