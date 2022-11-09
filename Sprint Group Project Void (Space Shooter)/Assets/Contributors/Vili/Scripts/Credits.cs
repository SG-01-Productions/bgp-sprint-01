using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Credits : MonoBehaviour
{
    private TMP_Text creditsText;
    [SerializeField] int credits;
    // Start is called before the first frame update
    void Start()
    {
        credits = 10;
        creditsText = gameObject.GetComponent<TMP_Text>();
    }
    void Update()
    {
        creditsText.text = "You have this much of credits : " + credits;
    }
}