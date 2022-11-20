using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour //Old Credits script
{   //Credit stuff under
    [SerializeField] int credits = 0;

    // Missile stuff under

    
    
    public static ResourceManager Instance;

    private static int thrusterLevel = 1;
    private static int shipHullLevel = 1;
    private static int rotationalThrusterLevel = 1;

    [SerializeField] private int missilePrice = 10000;
    [SerializeField] private int missileAmount = 10;
    [SerializeField] private int repairPrice = 30000;
    [SerializeField] private int shipHullUpgradePrice = 45000 + shipHullLevel * 10000;
    [SerializeField] private int thrusterUpgradePrice = 30000 + thrusterLevel * 10000;
    [SerializeField] private int rotationalThrusterPrice = 25000 + rotationalThrusterLevel * 10000;

    [SerializeField] private TMP_Text creditsAmountElement;

    // Assign the correct price tags in the Unity Editor
    [SerializeField] private TMP_Text repairCredits;
    [SerializeField] private TMP_Text missileCredits;
    [SerializeField] private TMP_Text upgradeCreditsThruster;
    [SerializeField] private TMP_Text thrusterLevelText;
    [SerializeField] private TMP_Text upgradeCreditsRotationalThrusters;
    [SerializeField] private TMP_Text shipHullCreditText;
    [SerializeField] private TMP_Text shipHullLevelText;

    [SerializeField] private float speedBoostMultiplier = 1.05f;
    [SerializeField] private float turnBoostMultiplier = 1.05f;
    [SerializeField] private float shipHullUpgradeAmount = 20f;

    // Missile stuff over
    // Credit studd under
    // Credit stuff over
    // Missile stuff

    private void UpdateShopPricesAndLevels()
    {
        repairCredits.text = repairPrice.ToString();
        missileCredits.text = missilePrice.ToString();
        upgradeCreditsThruster.text = thrusterUpgradePrice.ToString();
        thrusterLevelText.text = thrusterLevel.ToString();
        upgradeCreditsRotationalThrusters.text = rotationalThrusterPrice.ToString();
        shipHullLevelText.text = shipHullLevel.ToString();
        shipHullCreditText.text = shipHullUpgradePrice.ToString();
    }

    private void Awake()
    {
        Instance = this;
        UpdateShopPricesAndLevels();
    }
    // *Sami*
    //Let's redo this Vili, much more sense to attach this to player ship, since we will interacting with Space Station in the future.
    //We will hold missile and credits values here and playerShip will access this script to see how many missiles are left in storage.
    //If there are no missiles, our ship cannot use missiles and need to buy more from the Space Station.
    public void BuyMissiles()
    {
        if (credits >= missilePrice)
        {
            credits -= missilePrice;
            missileAmount += 1;
            UpdatePlayerCredits();
            Debug.Log("We have this amount of credits: " + credits);
            Debug.Log("We have this amount of missiles: " + missileAmount);
        }
    }

    public void AsteroidFieldCredits(int incomingCredits)
    {
        Debug.Log("We have this amount of credits before transaction " + credits);
        credits += incomingCredits;
        Debug.Log("After transaction we have this much of credits " + credits);
        UpdatePlayerCredits();
    }

    public void BuyShipRepair()
    {
        if (credits >= repairPrice)
        {
            credits -= repairPrice;
            GetComponent<PlayerHealthSystem>().HealFull();
            UpdatePlayerCredits();
        }
    }

    public void UpdatePlayerCredits()
    {
        creditsAmountElement.text = credits.ToString();
    }

    public void UpgradeThrusters()
    {
        if (credits >= thrusterUpgradePrice)
        {
            credits -= thrusterUpgradePrice;
            UpdatePlayerCredits();
            var charcontrol = GetComponent<CharControl>();
            charcontrol.Speed = charcontrol.BaseSpeed * Mathf.Pow(speedBoostMultiplier, thrusterLevel);
            thrusterLevel += 1;
            UpdateShopPricesAndLevels();
        }
    }

    public void UpgradeRotationalThrusters()
    {
        if (credits >= rotationalThrusterPrice)
        {
            credits -= rotationalThrusterPrice;
            UpdatePlayerCredits();
            var charcontrol = GetComponent<CharControl>();
            charcontrol.RotationSpeed = charcontrol.BaseRotationSpeed * Mathf.Pow(turnBoostMultiplier, rotationalThrusterLevel);
            rotationalThrusterLevel += 1;
            UpdateShopPricesAndLevels();
        }
    }

    public void UpgradeShipHull()
    {
        if (credits >= shipHullUpgradePrice)
        {
            bool isUpgraded = GetComponent<PlayerHealthSystem>().UpgradeMaxHealth(shipHullUpgradeAmount);
            if (isUpgraded)
            {
                credits -= shipHullUpgradePrice;
                UpdatePlayerCredits();
                shipHullLevel += 1;
                UpdateShopPricesAndLevels();
            }
        }
    }
}