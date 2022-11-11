using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Credits : MonoBehaviour
{   //Credit stuff under
    private TMP_Text creditsText;
    [SerializeField] int credits;
    // Missile stuff under
    int missilePrice = 50;
    int missileAmount;
    // Missile stuff over
    // Credit studd under
    void Start()
    {
        creditsText = gameObject.GetComponent<TMP_Text>();
    }
    void Update()
    {
        creditsText.text = "You have this much of credits : " + credits;
    }
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