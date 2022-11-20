using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSystem : MonoBehaviour
{
    public static PlayerHealthSystem Instance;
    private GameObject playerObject;

    [SerializeField] private float playerHealth = 100f;
    [SerializeField] private float maxHealth = 100f;

    [SerializeField] private bool isDead = false;

    [SerializeField] private GameObject finalScoreScreen;
    // The maximum collision velocity that does not harm the player
    [SerializeField] private float safeCollisionVelocity = 65f;
    // Multiplier for finetuning the collision damage
    [SerializeField] private float collisionDamageMultiplier = 1f;

    [SerializeField] private RectTransform healthMaskSprite;

    private delegate void OnHealthUpdated();

    private OnHealthUpdated onHealthUpdated;

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
            onHealthUpdated();
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
        //Destroy(playerObject);
        //some scripts break if playerobject is destroyed, so better set active state
        playerObject.SetActive(false);
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
            onHealthUpdated();
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
            onHealthUpdated();
            return playerHealth;
        } else return 0f;
    }

    private void Awake()
    {
        Instance = this;
        playerObject = gameObject;
        finalScoreScreen.SetActive(false);
    }

    private void Start()
    {
        onHealthUpdated = UpdateHealthHUD;
        onHealthUpdated += DebugHealth;
    }

    private void DebugHealth()
    {
        Debug.Log($"Playerhealth is now {playerHealth}");
    }

    // This part is only for debugging
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.K))
    //    {
    //        var playerhealth = Instance.DoDamage(5.5543f);
    //        Debug.Log(playerhealth);
    //    }
    //}
    // ^ This part is only for debugging

    // Damage to the player will be done based on the relative velocity of the collision
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Player collided with velocity of {collision.relativeVelocity.magnitude}");
        if (collision.relativeVelocity.magnitude > safeCollisionVelocity)
        {
            var absoluteDamage = Mathf.Abs(collision.relativeVelocity.magnitude);
            var actualDamage = absoluteDamage / 10f * collisionDamageMultiplier;
            DoDamage(actualDamage);
        }
    }

    /// <summary>
    /// Updates player health bar in the HUD.
    /// </summary>
    private void UpdateHealthHUD()
    {
        var healthFraction = GetHealthFraction();
        float rectHeight = healthMaskSprite.rect.height;
        var padding = (1f - healthFraction) * rectHeight;
        //Debug.Log(padding);
        var maskRectMask = healthMaskSprite.GetComponent<RectMask2D>();
        maskRectMask.padding = new Vector4(0, 0, 0, padding);
    }
    /// <summary>
    /// Gets the fraction of player current health
    /// </summary>
    /// <returns>float fraction of the player remaining health</returns>
    private float GetHealthFraction()
    {
        return playerHealth / maxHealth;
    }
}
