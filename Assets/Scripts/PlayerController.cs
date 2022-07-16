using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerState currentState;
    public CharacterController controller;

    public GameObject wheelFrontLeft;
    public GameObject wheelFrontRight;
    public GameObject wheelBackLeft;
    public GameObject wheelBackRight;

    // Player movement variables
    public float groundDistance = 1f;
    public float rotationSpeed = 0.1f;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3;
    public float wheelRotationSpeed;

    public GameObject groundCheck;
    public LayerMask groundMask;

    public int dashCooldownMax;
    public int dashTime;
    public float dashSpeed;

    public int dashCooldown = 0;

    public bool isGrounded = false;


    private Rigidbody rb;

    void Start()
    {
        currentState = new PlayerIdle(this);
        currentState.OnStateEnter();
        controller = GetComponent<CharacterController>();
        
    }
    void Update()
    {
        if (groundCheck != null)
        {
            isGrounded = Physics.CheckSphere(groundCheck.transform.position, groundDistance, groundMask);
        }


        if (currentState != null)
        {
            currentState.OnStateUpdate();
        }

        if (dashCooldown > 0)
        {
            dashCooldown--;
        }

        RotateWheel(wheelBackLeft);
        RotateWheel(wheelBackRight);
        RotateWheel(wheelFrontLeft);
        RotateWheel(wheelFrontRight);

    }

    private void RotateWheel(GameObject wheel)
    {
        wheel.transform.Rotate(new Vector3(0, 0, Time.deltaTime * wheelRotationSpeed * controller.velocity.magnitude), Space.Self);
    }

    public void ChangeState(PlayerState newState)
    {
        currentState.OnStateExit();
        currentState = newState;
        newState.OnStateEnter();
    }
}
