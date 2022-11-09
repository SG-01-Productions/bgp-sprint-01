using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharControl : MonoBehaviour
{
    /// <summary>
    /// Handling ship movement variables used
    /// </summary>
    private Vector2 InputVector;
    private Vector2 CurrentMovement;
    private Rigidbody ShipRigidbody;
    [SerializeField] private float Speed;
    [SerializeField] private float StrafeSpeed;
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

    /// <summary>
    /// Ship movement functions, acceleration affected by mass of ship
    /// Check if shift is pressed to choose between turning with torque 
    /// or strafing with sideways movement
    /// </summary>
    public void MoveShip()
    {
        ShipRigidbody.AddForce(CurrentMovement.y * Speed * ShipTransform.forward, ForceMode.Force);
        if (Keyboard.current.shiftKey.isPressed) {
            ShipRigidbody.AddForce(CurrentMovement.x * StrafeSpeed * ShipTransform.right, ForceMode.Force);
        } else
        {
            ShipRigidbody.AddTorque(0, 0, -CurrentMovement.x * RotationSpeed);
        }
        
        
    }
    public void CannonFire(InputAction.CallbackContext context)
    {
    }

    void Start()
    {
        
    }
    /// <summary>
    /// Getting the data from the player ship for movements
    /// </summary>
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
