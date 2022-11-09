using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Salavge : MonoBehaviour
{
    private TMP_Text salvageText;
    [SerializeField] int salvage;
    // Start is called before the first frame update
    void Start()
    {
        salvage = 9;
        salvageText = gameObject.GetComponent<TMP_Text>();
    }
    void Update()
    {
        salvageText.text = "You have this much salvage : " + salvage;
    }
}