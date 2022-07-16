using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerState currentState;

    public GameObject wheelFrontLeft;
    public GameObject wheelFrontRight;
    public GameObject wheelBackLeft;
    public GameObject wheelBackRight;

    // Player movement variables
    public float groundDistance = 1f;
    public float rotationSpeed = 0.1f;
    public float accelleration = 12f;
    public float maxSpeed;

    public float gravity = -9.81f;
    public float jumpHeight = 3;
    public float wheelRotationSpeed;
    public float driftFriction;

    public GameObject groundCheck;
    public LayerMask groundMask;

    public float dashCooldownMax;
    public float dashTime;
    public float dashSpeed;

    [HideInInspector]
    public float dashCooldown = 0;

    [HideInInspector]
    public bool isGrounded = false;

    [HideInInspector]
    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentState = new PlayerDriving(this);
        currentState.OnStateEnter();        
    }
    void Update()
    {
        // Speed Limit
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        // Check if grounded
        if (groundCheck != null)
        {
            isGrounded = Physics.CheckSphere(groundCheck.transform.position, groundDistance, groundMask);
        }

        // Call the current state's update
        if (currentState != null)
        {
            currentState.OnStateUpdate();
        }

        // Rotate the wheels
        RotateWheel(wheelBackLeft);
        RotateWheel(wheelBackRight);
        RotateWheel(wheelFrontLeft);
        RotateWheel(wheelFrontRight);

        // Cooldowns
        dashCooldown -= Time.deltaTime;
    }

    private void RotateWheel(GameObject wheel)
    {
        wheel.transform.Rotate(new Vector3(0, 0, Time.deltaTime * wheelRotationSpeed * rb.velocity.magnitude), Space.Self);
    }

    public void ChangeState(PlayerState newState)
    {
        currentState.OnStateExit();
        currentState = newState;
        newState.OnStateEnter();
    }
}
