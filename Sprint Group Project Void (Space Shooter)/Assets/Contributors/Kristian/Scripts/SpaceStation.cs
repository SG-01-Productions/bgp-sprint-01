using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceStation : MonoBehaviour
{
    [SerializeField] private GameObject ShopNotification;
    [SerializeField] private GameObject ShopPanel;
    private bool playerIsInShopArea = false;
    private bool shopIsOpen = false;
    void Start()
    {
        ShopNotification.SetActive(false);
        ShopPanel.SetActive(false);
        Debug.Log($"Player in shop area: {playerIsInShopArea}");
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Spacestation"))
        {
            var station = other.GetComponent<SpaceStationVisited>();
            if (!station.Visited)
            {
                station.Visited = true;
                GameObject.Find("EventSystem").GetComponent<MainObjective>().GenerateSpaceStation();
            }
            Debug.Log("Player entered shop area");
            ShopNotification.SetActive(true);
            playerIsInShopArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Spacestation"))
        {
            Debug.Log("Player exited shop area");
            ShopNotification.SetActive(false);
            playerIsInShopArea = false;
            CloseShop();
        }
    }
    public void HandleShopOpen(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (playerIsInShopArea)
            {
                if (!shopIsOpen)
                {
                    OpenShop();
                } else
                {
                    CloseShop();
                }
            }
        }
    }

    private void OpenShop()
    {
        ShopNotification.SetActive(false);
        ShopPanel.SetActive(true);
        shopIsOpen = true;
    }

    private void CloseShop()
    {
        ShopPanel.SetActive(false);
        shopIsOpen = false;
        if (playerIsInShopArea)
        {
            ShopNotification.SetActive(true);
        }
    }
}