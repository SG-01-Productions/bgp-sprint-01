using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidFieldManager : MonoBehaviour
{
    [SerializeField] private Rigidbody[] asteroidsPrefabs;
    private Rigidbody asteroid;
    
    [SerializeField] private BoxCollider asteroidSpawnField;

    [SerializeField] private float minAsteroidSize = 20f;
    [SerializeField] private float maxAsteroidSize = 200f;

    [SerializeField] private float minAsteroidMass = 1f;
    [SerializeField] private float maxAsteroidMass = 100f;

    [SerializeField] private float asteroidHealthMultiplier = 150f;

    [SerializeField] private int asteroidFieldCount = 3;

    [SerializeField] private int asteroidFieldObjectsCount = 10;

    [SerializeField] private int asteroidFieldRadius = 20;

    private void Start()
    {
        for (int i = 0; i < asteroidFieldCount; i++)
        {
            AsteroidFieldSpawner();
        }    
    }
    public Vector3 GetRandomPointInsideCollider(BoxCollider boxCollider)
    {
        Vector3 extents = boxCollider.size / 2f;
        Vector3 point = new Vector3(Random.Range(-extents.x, extents.x), Random.Range(-extents.y, extents.y), 0);

        return boxCollider.transform.TransformPoint(point);
    }

    void AsteroidFieldSpawner()
    {
        Vector3 position = GetRandomPointInsideCollider(asteroidSpawnField);
        for (int i = 0; i < asteroidFieldObjectsCount; i++)
        {
            Vector3 asteroidPosition = Random.insideUnitSphere * asteroidFieldRadius;
            asteroidPosition.z = 0;
            SpawnAsteroid(position + asteroidPosition);
        }
    }

    private void SpawnAsteroid(Vector3 position)
    {   
        asteroid = Instantiate(asteroidsPrefabs[Random.Range(0, asteroidsPrefabs.Length)]);
        asteroid.transform.position = position;
        float asteroidMass = Random.Range(0f, 1f);
        asteroid.transform.localScale = Vector3.Lerp(new Vector3(minAsteroidSize, minAsteroidSize, minAsteroidSize), new Vector3(maxAsteroidSize, maxAsteroidSize, maxAsteroidSize), asteroidMass);
        asteroid.mass = Mathf.Lerp(minAsteroidMass, maxAsteroidMass, asteroidMass);
        float asteroidHealth = asteroidMass * 100f * asteroidHealthMultiplier;
        asteroid.GetComponent<HealthManager>().SetAsteroidHealth(asteroidHealth);
    }
}
