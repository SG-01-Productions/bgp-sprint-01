using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainObjective : MonoBehaviour
{
    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject spaceStationObject;

    [SerializeField] private float minSpaceStationDistance;
    [SerializeField] private float maxSpaceStationDistance;

    private GameObject previousSpaceStation = null;
    private GameObject currentSpaceStation = null;

    [SerializeField] private RectTransform pointerRectTransform;
    private Vector3 targetPosition;

    [SerializeField] private float hudArrowpadding = 100f;

    [SerializeField] private TMP_Text distancetext;
    public void GenerateSpaceStation()
    {
        var tempSpaceStation = previousSpaceStation;

        previousSpaceStation = currentSpaceStation;
        Vector3 direction = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)) * Vector3.right;
        Vector3 spawnPosition = playerObject.transform.position + direction * Random.Range(minSpaceStationDistance,maxSpaceStationDistance);

        tempSpaceStation.transform.SetPositionAndRotation(spawnPosition, Quaternion.identity);
        tempSpaceStation.transform.Find("SpaceStation").localRotation = Random.rotation;
        tempSpaceStation.GetComponent<SpaceStationVisited>().Visited = false;
        currentSpaceStation = tempSpaceStation;
    }

    private void InitializeSpaceStations()
    {
        var TempPosition = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y, -4000f);
        if (previousSpaceStation == null)
        {
            previousSpaceStation = Instantiate(spaceStationObject, TempPosition, Quaternion.identity);
        }
        if (currentSpaceStation == null)
        {
            currentSpaceStation = Instantiate(spaceStationObject, TempPosition, Quaternion.identity);
        }
    }
    private void Awake()
    {
        InitializeSpaceStations();
        GenerateSpaceStation();
    }

    // CodeMonkey Code (partly modified)
    // https://unitycodemonkey.com/video.php?v=dHzeHh-3bp4
    private void FixedUpdate()
    {
        targetPosition = currentSpaceStation.transform.position;
        Vector3 toPosition = new Vector3(targetPosition.x, targetPosition.y, 0);
        Vector3 fromPosition = Camera.main.transform.position;
        fromPosition.z = 0f;
        Vector3 direction = (toPosition - fromPosition).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);

        Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(targetPosition);
        bool offscreen = targetPositionScreenPoint.x <= 0 || targetPositionScreenPoint.x >= Screen.width || targetPositionScreenPoint.y <= 0 || targetPosition.y >= Screen.height;

        if (offscreen)
        {
            float padding = 100f;
            Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;
            if (cappedTargetScreenPosition.x <= padding) cappedTargetScreenPosition.x = padding;
            if (cappedTargetScreenPosition.x >= Screen.width - padding) cappedTargetScreenPosition.x = Screen.width - padding;
            if (cappedTargetScreenPosition.y <= padding) cappedTargetScreenPosition.y = padding;
            if (cappedTargetScreenPosition.y >= Screen.height) cappedTargetScreenPosition.y = Screen.height - padding;

            pointerRectTransform.position = cappedTargetScreenPosition;
        }
        // (partly modified) CodeMonkey Code ends here

        float distance = Vector3.Distance(playerObject.transform.position, currentSpaceStation.transform.position);
        float fixedDistance = distance - 1000f;
        if (fixedDistance > 0f)
        {
            distancetext.text = $"{fixedDistance:0}";
        } else distancetext.text = "";

    }
}
