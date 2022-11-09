using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidHandler : MonoBehaviour
{
    public float asteroidHealth;
    public Rigidbody asteroidPrefab;
    private Rigidbody asteroid;
    [SerializeField] private GameObject playerShip;

    private void Start()
    {
        ObjectSpawner.Instance.asteroidCount++;
    }
    public void SetAsteroidHealth(float health)
    {
        asteroidHealth = health;
    }

    public float ReceiveDamage(float damage)
    {
        asteroidHealth -= damage;

        if (asteroidHealth <= 0)
        {
            DestroyAsteroid();
            return 0;
        }
        else
        {
            return asteroidHealth;
        }
    }

    public void DestroyAsteroid()
    {
        Destroy(gameObject);
        ObjectSpawner.Instance.asteroidCount--;
    }

    
}
