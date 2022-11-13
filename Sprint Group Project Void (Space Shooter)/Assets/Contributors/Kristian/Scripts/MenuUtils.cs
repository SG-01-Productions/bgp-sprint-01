using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MenuUtils : MonoBehaviour
{
    public void EnablePanel(GameObject obj)
    {
        obj.SetActive(true);
    }

    public void DisableUIElement(GameObject obj)
    {
        obj.SetActive(false);
    }
}