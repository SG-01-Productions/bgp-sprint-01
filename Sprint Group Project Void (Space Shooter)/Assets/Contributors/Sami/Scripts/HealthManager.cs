using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    private float asteroidFullHealth;
    public float asteroidHealth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    public void SetAsteroidHealth(float health)
    {
        asteroidFullHealth = health;
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
        AsteroidFieldManager.Instance.asteroidDestroyed(transform.position, asteroidFullHealth * 0.33f);

        Destroy(gameObject);
    }
}
