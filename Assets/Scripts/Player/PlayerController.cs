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

    public ForkliftIdleSoundController forkliftIdleSoundController;

    // Player movement variables
    public float rotationSpeed = 0.1f;
    public float accelleration = 8f;
    public float groundDistance = 1f;
    public float maxSpeed;
    public float sidewaysDrag;
    public float forwardsDrag;
    float x;
    float z;

    private bool jumpKeyWasPressed;

    public float jumpForce;
    public float wheelRotationSpeed;

    public GameObject groundCheck;
    public LayerMask groundMask;

    public float dashCooldownMax;
    public float dashTime;
    public float dashSpeed;

    public float attackCooldownMax;
    public float damage;

    [HideInInspector]
    public float dashCooldown = 0;
    [HideInInspector]
    public float attackCooldown = 0;
    
    [HideInInspector]
    public bool isGrounded = false;

    [HideInInspector]
    public Rigidbody rb;

    [HideInInspector]
    public Animator playerAnimator;

    private AudioSource audioSource;
    public AudioSource AudioSource
    {
        get
        {
            if (audioSource == null) audioSource = GetComponent<AudioSource>();
            return audioSource;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
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


        TurnWheel(wheelFrontLeft);
        TurnWheel(wheelFrontRight);


        // Cooldowns
        dashCooldown -= Time.deltaTime;
        attackCooldown -= Time.deltaTime;

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

    private void TurnWheel(GameObject wheel)
    {
        wheel.transform.localEulerAngles = new Vector3(0, 30*x, wheel.transform.rotation.eulerAngles.z);
    }

    private void movementUpdate ()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        if (x < -0.5)
        {
            playerAnimator.SetBool("isTurningLeft", true);
            playerAnimator.SetBool("isTurningRight", false);

        }
        else if (x > 0.5)
        {
            playerAnimator.SetBool("isTurningRight", true);
            playerAnimator.SetBool("isTurningLeft", false);
        }
        else
        {
            playerAnimator.SetBool("isTurningLeft", false);
            playerAnimator.SetBool("isTurningRight", false);
        }


        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpKeyWasPressed = true;
        }


        transform.Rotate(new Vector3(0f, x * rotationSpeed, 0f));

    }
    void jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(0, jumpForce, 0, ForceMode.VelocityChange);
        SoundBank.PlayAudioClip(SoundBank.GetInstance().playerJumpAudioClips, AudioSource);
    }
    void acceleration()
    {
        rb.AddRelativeForce(-z * accelleration, 0, 0, ForceMode.Acceleration);
        forkliftIdleSoundController.TurnOn();
    }

    void movementFixedUpdate()
    {
        if (jumpKeyWasPressed)
        {
            jump();
            jumpKeyWasPressed = false;
        }

        if (isGrounded)
        {
            acceleration();
        }

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
        if(newState.GetType() == currentState.GetType())
        {
            Debug.Log("Didn't change state because new state = old state");
            return;
        }

        Debug.Log($"Change state from {currentState.ToString()} to {newState.ToString()}");
        currentState.OnStateExit();
        currentState = newState;
        newState.OnStateEnter();
    }

    /// <summary>
    /// This is called when the end of the attack anim is reached
    /// </summary>
    public void OnAttackAnimationFinished()
    {
        ChangeState(new PlayerDriving(this));
    }

    /// <summary>
    /// 
    /// </summary>
    public void Die()
    {
        SoundBank.PlayAudioClip(SoundBank.GetInstance().playerDeathAudioClips, AudioSource);

    }
}
