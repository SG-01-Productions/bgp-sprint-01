using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
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
        //ObjectSpawner.Instance.asteroidCount--; 
        //This^ is the issue. Unity can't find referenced value for some reason, or something like that.
        //I have no idea why. I tried to troubleshoot, but only way I could fix it, would be to do my own implementation of the asteroid count. Which would be enroaching upon Jeremy's Territory.
        Destroy(gameObject);
    }
}
