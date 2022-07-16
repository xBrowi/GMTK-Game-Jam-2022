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
    float x;
    float z;

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
        if (currentState.GetType() == typeof(PlayerDriving) || currentState.GetType() == typeof(PlayerAttacking))
        {
            movementUpdate();
        }
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

        if (currentState.GetType() == typeof(PlayerDriving) || currentState.GetType() == typeof(PlayerAttacking))
        {
            movementFixedUpdate();
        }
    }
    private void RotateWheel(GameObject wheel)
    {
        wheel.transform.Rotate(new Vector3(0, 0, Time.deltaTime * wheelRotationSpeed * rb.velocity.magnitude), Space.Self);
    }

    private void movementUpdate ()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        if (x < 0)
        {
            animator.SetBool("isTurningLeft", true);
            animator.SetBool("isTurningRight", false);

        }
        else if (x > 0)
        {
            animator.SetBool("isTurningRight", true);
            animator.SetBool("isTurningLeft", false);
        }
        else
        {
            animator.SetBool("isTurningLeft", false);
            animator.SetBool("isTurningRight", false);
        }

        if (isGrounded)
        {
            acceleration();
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jump();
        }


        transform.Rotate(new Vector3(0f, x * rotationSpeed, 0f));

        // start dashing
        if (Input.GetButtonDown("Fire3") && dashCooldown <= 0)
        {
            ChangeState(new PlayerDashing(this));
        }

    }
    void jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(0, jumpForce, 0, ForceMode.VelocityChange);
    }
    void acceleration()
    {
        rb.AddRelativeForce(-z * accelleration, 0, 0, ForceMode.Acceleration);
    }

    void movementFixedUpdate()
    {
        // drag
        if (isGrounded)
        {
            // sideways
            Vector3 locVel = rb.transform.InverseTransformDirection(rb.velocity);
            locVel.z *= sidewaysDrag;

            // forwards
            if (locVel.x < maxSpeed)
            {
                locVel.x *= forwardsDrag;
            }
            rb.velocity = rb.transform.TransformDirection(locVel);
        }
    }

    public void ChangeState(PlayerState newState)
    {
        Debug.Log($"Change state from {currentState.ToString()} to {newState.ToString()}");
        currentState.OnStateExit();
        currentState = newState;
        newState.OnStateEnter();
    }
}
