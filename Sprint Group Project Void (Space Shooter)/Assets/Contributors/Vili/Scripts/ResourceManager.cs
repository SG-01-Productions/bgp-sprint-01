using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceManager : MonoBehaviour //Old Credits script
{   //Credit stuff under
    [SerializeField] int credits;
    // Missile stuff under
    int missilePrice = 50;
    int missileAmount;
    // Missile stuff over
    // Credit studd under
    // Credit stuff over
    // Missile stuff

    void Update()
    {
        
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
            Debug.Log("We have this amount of credits: " + credits);
            Debug.Log("We have this amount of missiles: " + missileAmount);
        }
    }
}