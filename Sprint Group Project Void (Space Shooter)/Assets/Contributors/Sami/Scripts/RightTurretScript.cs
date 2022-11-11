using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightTurretScript : MonoBehaviour
{
    [SerializeField] Camera playerCamera;

    // Update is called once per frame
    void Start()
    {
        playerCamera = Camera.main;
    }
    void FixedUpdate()
    {
        {
            Vector3 mousePos = Input.mousePosition;
            Vector2 mouseScreenPosition = playerCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, playerCamera.nearClipPlane));

            float mousePosX = mouseScreenPosition.x;
            float mousePosY = mouseScreenPosition.y;
            float playerPosX = GetComponentInParent<Transform>().position.x;
            float playerPosY = GetComponentInParent<Transform>().position.y;

            Vector2 Point_1 = new Vector2(mousePosX, mousePosY);
            Vector2 Point_2 = new Vector2(playerPosX, playerPosY);
            float rotation = Mathf.Atan2(Point_2.y - Point_1.y, Point_2.x - Point_1.x) * Mathf.Rad2Deg;
            Quaternion compensationAngle = GetComponentInParent<CharControl>().transform.rotation;
            Vector3 eulerCompensationAngle = compensationAngle.eulerAngles; //Compensation for the ship's angle to the turret rotation.
            Vector3 turretStartRotation = new Vector3(0f, 0f, rotation - eulerCompensationAngle.x);
            Quaternion quaternion = Quaternion.Euler(-turretStartRotation);

            transform.localRotation = quaternion;
            //Instantiate(playerProjectile, transform.position, quaternion);
        }
    }
}