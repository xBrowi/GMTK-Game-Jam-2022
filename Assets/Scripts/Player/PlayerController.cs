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
    public float rotationSpeed = 0.1f;
    public float accelleration = 8f;
    public float groundDistance = 1f;
    public float maxSpeed;
    public float sidewaysDrag;
    public float forwardsDrag;

    public float jumpForce;
    public float wheelRotationSpeed;

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

    [HideInInspector]
    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
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
            isGrounded = Physics.CheckBox(groundCheck.transform.position, new Vector3 (0.5f, groundDistance, 0.4f),transform.rotation ,groundMask);
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
    void FixedUpdate()
    {
        if (currentState != null)
        {
            currentState.OnStateFixedUpdate();
        }
    }
    private void RotateWheel(GameObject wheel)
    {
        wheel.transform.Rotate(new Vector3(0, 0, Time.deltaTime * wheelRotationSpeed * rb.velocity.magnitude), Space.Self);
    }

    private void movement ()
    {

    }

    public void ChangeState(PlayerState newState)
    {
        Debug.Log($"Change state from {currentState.ToString()} to {newState.ToString()}");
        currentState.OnStateExit();
        currentState = newState;
        newState.OnStateEnter();
    }
}
