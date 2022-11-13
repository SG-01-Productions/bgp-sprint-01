using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxRotation : MonoBehaviour
{
    [SerializeField] private Rigidbody playerShip;
    [SerializeField] private Vector3 skyboxRotationAxis;
    [SerializeField] private float skyboxRotationAngle;
    void FixedUpdate()
    {
        Vector3 targetVelocity = playerShip.velocity;
        Vector3 normTargetVelocity = targetVelocity.normalized;
        if (normTargetVelocity.magnitude > 0)
        {
            skyboxRotationAxis = new Vector3(normTargetVelocity.z, skyboxRotationAxis.y, -normTargetVelocity.x) * 0.01f;
            float rotationDampener = 0.005f;
            skyboxRotationAngle = skyboxRotationAngle + targetVelocity.magnitude * rotationDampener;
            RenderSettings.skybox.SetFloat("_Rotation", skyboxRotationAngle);
            RenderSettings.skybox.SetVector("_RotationAxis", skyboxRotationAxis);
        }
    }
}
