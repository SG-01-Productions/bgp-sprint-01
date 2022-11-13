using System;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    private float asteroidFullHealth;
    public float asteroidHealth;
    public int bigAsteroidHealth;
    public int bigAsteroidCreditValue;
    // Start is called before the first frame update
    void Start()
    {
        bigAsteroidHealth = Convert.ToInt32(asteroidHealth);
        bigAsteroidCreditValue = bigAsteroidHealth;
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
        int incomingCredits = bigAsteroidHealth;
        AsteroidFieldManager.Instance.asteroidDestroyed(transform.position, asteroidFullHealth * 0.33f);
        Debug.Log("We are trying to give these Credits: " + incomingCredits);
        GameObject.Find("SpaceshipWithTurrets").GetComponent<ResourceManager>().AsteroidFieldCredits(incomingCredits);
        Destroy(gameObject);
    }

}
