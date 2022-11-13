using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    public static PlayerHealthSystem Instance;
    private GameObject playerObject;

    [SerializeField] private float playerHealth = 100f;
    [SerializeField] private float maxHealth = 100f;

    [SerializeField] private bool isDead = false;

    [SerializeField] private GameObject finalScoreScreen;

    /// <summary>
    /// Function to damage player health specified amount. If player health drops to 0, player will die.
    /// </summary>
    /// <param name="damage">The amount of damage to do to the player</param>
    /// <returns>Players health after damage</returns>
    public float DoDamage(float damage)
    {
        if (!isDead)
        {
            playerHealth -= damage;

            if (playerHealth < 0)
            {
                PlayerDie();
                return 0;
            }
            else return playerHealth;
        }
        else return 0;
    }
    /// <summary>
    /// Function that happens when player health drops to 0;
    /// </summary>
    public void PlayerDie()
    {
        isDead = true;
        Destroy(playerObject);
        finalScoreScreen.SetActive(true);
    }
    /// <summary>
    /// Player will receive the heal amount of health, capping to maxhealth value.
    /// </summary>
    /// <param name="heal">The amout of health that the player will heal</param>
    /// <returns>Player's health after heal</returns>
    public float ReceiveHealth(float heal)
    {
        if (!isDead)
        {
            if (playerHealth + heal > maxHealth)
            {
                playerHealth = maxHealth;
            }
            else
            {
                playerHealth += heal;
            }
            return playerHealth;
        }
        else return 0f;
    }
    /// <summary>
    /// Player will heal to full if the player is not dead 
    /// </summary>
    /// <returns>Player health after heal</returns>
    public float HealFull()
    {
        if (!isDead)
        {
            playerHealth = maxHealth;
            return playerHealth;
        } else return 0f;
    }

    private void Awake()
    {
        Instance = this;
        playerObject = gameObject;
        finalScoreScreen.SetActive(false);
    }

    // This part is only for debugging
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            var playerhealth = Instance.DoDamage(5.5543f);
            Debug.Log(playerhealth);
        }
    }
    // ^ This part is only for debugging
}