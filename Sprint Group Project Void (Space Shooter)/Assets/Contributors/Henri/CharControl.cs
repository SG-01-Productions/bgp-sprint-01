using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharControl : MonoBehaviour
{
    /// <summary>
    /// Handling ship movement
    /// </summary>
    private Vector2 InputVector;
    private Vector2 CurrentMovement;
    private Rigidbody ShipRigidbody;
    [SerializeField] private float Speed;
    [SerializeField] private float RotationSpeed;
    private Transform ShipTransform;
    public void Fire(InputAction.CallbackContext context)
        {
        Debug.Log("Fire!");
    }
    public void Move(InputAction.CallbackContext context)
    {
        InputVector = context.ReadValue<Vector2>();
        CurrentMovement.x = InputVector.x;
        CurrentMovement.y = InputVector.y;
    }

    public void MoveShip()
    {
        ShipRigidbody.AddForce(CurrentMovement.y * Speed * ShipTransform.forward, ForceMode.Force);
        ShipRigidbody.AddTorque(0, 0, -CurrentMovement.x * RotationSpeed);

    }
    public void CannonFire(InputAction.CallbackContext context)
    {
    }

    //public void Fire(InputAction.CallbackContext context)
    //{
    //}
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        ShipRigidbody = GetComponent<Rigidbody>();
        ShipTransform = GetComponent<Transform>();

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        MoveShip();
    }
}
