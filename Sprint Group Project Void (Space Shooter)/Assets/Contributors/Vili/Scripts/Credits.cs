using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Credits : MonoBehaviour
{
    private TMP_Text creditsText;
    [SerializeField] int credits;
    int missilePrice = 50;
    int missile;
    void Start()
    {
        credits = 10;
        creditsText = gameObject.GetComponent<TMP_Text>();
    }
    void Update()
    {
        creditsText.text = "You have this much of credits : " + credits;
    }
    public void TransactionCall(float credits)
    {
        if (credits => missilePrice)
        {
            credits - missilePrice;
            missile = +1;
        }
        if (missile => 1)
        { 
            //Tähän tulee acsesser

        }
    }
}