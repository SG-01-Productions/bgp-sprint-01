using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreditsDoneBySami : MonoBehaviour
{
    private TMP_Text creditsText;
    [SerializeField] int credits;
    // Start is called before the first frame update
    void Start()
    {
        credits = 5;
        creditsText = gameObject.GetComponent<TMP_Text>();
        creditsText.text = "Test" + credits;
    }
}
