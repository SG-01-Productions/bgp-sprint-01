using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1.0f;
    [SerializeField] private Vector3 rotationVector = new(0f,1f,0f);

    private void FixedUpdate()
    {
        transform.Rotate(rotationVector, rotationSpeed);
    }
}
