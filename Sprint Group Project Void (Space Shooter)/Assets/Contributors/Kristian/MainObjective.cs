using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainObjective : MonoBehaviour
{
    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject spaceStationObject;

    [SerializeField] private float minSpaceStationDistance;
    [SerializeField] private float maxSpaceStationDistance;

    private GameObject previousSpaceStation = null;
    private GameObject currentSpaceStation = null;
    public void GenerateSpaceStation()
    {
        Vector3 direction = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)) * Vector3.right;
        Vector3 spawnPosition = playerObject.transform.position + direction * Random.Range(minSpaceStationDistance,maxSpaceStationDistance);
        if (previousSpaceStation != null)
        {
            Destroy(previousSpaceStation);
        }
        if (currentSpaceStation != null)
        {
            previousSpaceStation = currentSpaceStation;
        }
        currentSpaceStation = Instantiate(spaceStationObject, spawnPosition, Quaternion.identity);
    }

    private Vector3 GetPlayerPositionInSpace()
    {
        var playerpos = playerObject.transform.position;
        
        return new Vector3(playerpos.x, playerpos.y, 0f);
    }

    private void Start()
    {
        GenerateSpaceStation();
    }
}
