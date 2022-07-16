using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : PlayerState
{
    public Transform groundCheck;



    float x;
    float z;



    [SerializeField] Vector3 velocity;

    public float turnSmoothTime = 1f;
    float turnSmoothVelocity;

    public PlayerIdle(PlayerController playerController) : base(playerController)
    {
        this.playerController = playerController;
    }

    public override void OnStateEnter()
    {

        Cursor.lockState = CursorLockMode.Locked;
}
    public override void OnStateExit()
    {

    }


    public override void OnStateUpdate()
    {
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxis("Vertical");

        velocity = playerController.transform.TransformDirection(Vector3.left) * playerController.speed * z + new Vector3(0f, velocity.y, 0f); ;

        if (Input.GetButtonDown("Jump") && playerController.isGrounded)
        {
            velocity.y = Mathf.Sqrt(playerController.jumpHeight * -2 * playerController.gravity);
        }

        if (playerController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }


        velocity.y += playerController.gravity * Time.deltaTime;

        playerController.transform.Rotate(new Vector3(0f, x * playerController.rotationSpeed, 0f));
        playerController.controller.Move(velocity * Time.deltaTime);

        // start dashing
        if (Input.GetButtonDown("Fire3") && playerController.dashCooldown <= 0)
        {
            playerController.ChangeState(new PlayerDashing(playerController));
        }
    }
}
