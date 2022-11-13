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

    /// <summary>
    /// Particle system variables
    /// </summary>
    [SerializeField] private ParticleSystem leftBooster;
    [SerializeField] private ParticleSystem rightBooster;
    private ParticleSystem.MainModule leftBoosterMain;
    private ParticleSystem.MainModule rightBoosterMain;

    [SerializeField] private ParticleSystem rightSideBooster;
    [SerializeField] private ParticleSystem leftSideBooster;
    private ParticleSystem.MainModule rightSideBoosterMain;
    private ParticleSystem.MainModule leftSideBoosterMain;

    [SerializeField] private ParticleSystem rightSideBooster2;
    [SerializeField] private ParticleSystem leftSideBooster2;
    private ParticleSystem.MainModule rightSideBoosterMain2;
    private ParticleSystem.MainModule leftSideBoosterMain2;

    [SerializeField] private ParticleSystem frontRightBooster;
    [SerializeField] private ParticleSystem frontLeftBooster;
    private ParticleSystem.MainModule frontRightBoosterMain;
    private ParticleSystem.MainModule frontLeftBoosterMain;

    private float alpha;

    //public void Fire(InputAction.CallbackContext context)
    //{
    //    Debug.Log("Fire!");
    //}
    public void Move(InputAction.CallbackContext context)
    {
        InputVector = context.ReadValue<Vector2>();
        CurrentMovement.x = InputVector.x;
        CurrentMovement.y = InputVector.y;
    }

    private void AnimateParticles()
    {
        if (InputVector.y > 0)
        {
            leftBoosterMain.startLifetime = 1f;
            rightBoosterMain.startLifetime = 1f;
            leftBoosterMain.startSize = 11f;
            rightBoosterMain.startSize = 11f;
            frontLeftBooster.Stop();
            frontRightBooster.Stop();
        } else if (InputVector.y == 0)
        {
            alpha = Mathf.MoveTowards(1f, 0.01f, (1 / 0.1f) * Time.deltaTime);
            leftBoosterMain.startLifetime = alpha;
            rightBoosterMain.startLifetime = alpha;
            leftBoosterMain.startSize = 9f;
            rightBoosterMain.startSize = 9f;
            frontLeftBooster.Stop();
            frontRightBooster.Stop();
        } else if (InputVector.y < 0)
        {
            leftBoosterMain.startLifetime = 0;
            rightBoosterMain.startLifetime = 0;
            frontLeftBooster.Play();
            frontRightBooster.Play();
        }
        if (InputVector.x > 0)
        {
            leftSideBooster.Play();
            leftSideBooster2.Play();
            rightSideBooster.Stop();
            rightSideBooster2.Stop();
        } else if (InputVector.x == 0)
        {
            leftSideBooster.Stop();
            leftSideBooster2.Stop();
            rightSideBooster.Stop();
            rightSideBooster2.Stop();
        } else
        {
            leftSideBooster.Stop();
            leftSideBooster2.Stop();
            rightSideBooster.Play();
            rightSideBooster2.Play();
        }
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
        leftBoosterMain = leftBooster.main;
        rightBoosterMain = rightBooster.main;
        frontLeftBoosterMain = frontLeftBooster.main;
        frontRightBoosterMain = frontRightBooster.main;
        rightSideBoosterMain = rightSideBooster.main;
        rightSideBoosterMain2 = rightSideBooster2.main;
        leftSideBoosterMain = leftSideBooster.main;
        leftSideBoosterMain2 = leftSideBooster2.main;
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
        AnimateParticles();
    }
}
