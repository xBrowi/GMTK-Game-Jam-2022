using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3;
    public float rotationSpeed;

    float x;
    float z;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    [SerializeField] Vector3 velocity;
    public bool isGrounded;

    public float turnSmoothTime = 1f;
    float turnSmoothVelocity;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxis("Vertical");
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        velocity = transform.TransformDirection(Vector3.forward) * speed * z + new Vector3(0f, velocity.y, 0f); ;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }


        velocity.y += gravity * Time.deltaTime;

    }

    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0f, x * rotationSpeed, 0f));
        controller.Move(velocity * Time.deltaTime);
    }
}