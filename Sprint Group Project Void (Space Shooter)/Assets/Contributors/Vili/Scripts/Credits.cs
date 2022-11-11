using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Credits : MonoBehaviour
{   //Credit stuff under
    [SerializeField] int credits;
    // Missile stuff under
    int missilePrice = 50;
    int missileAmount;
    // Missile stuff over
    // Credit studd under
    // Credit stuff over
    // Missile stuff
    public void TransactionCall(int credits)
    {
        if (credits >= missilePrice)
        {
            credits -= missilePrice;
            missileAmount += +1;
        }
        if (missileAmount >= 1)
        {
            //This is an Accsessor
            GetComponentInChildren<ShooterScript>().ReceiveMissiles(missileAmount);
        }
    }
}