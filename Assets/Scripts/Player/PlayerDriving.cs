using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDriving : PlayerState
{




    [SerializeField] Vector3 velocity;

    public float turnSmoothTime = 1f;
    float turnSmoothVelocity;

    public PlayerDriving(PlayerController playerController) : base(playerController)
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
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxis("Vertical");

        playerController.rb.AddRelativeForce(-z * playerController.accelleration, 0, 0, ForceMode.Acceleration);


        if (Input.GetButtonDown("Jump") && playerController.isGrounded)
        {
            velocity.y = Mathf.Sqrt(playerController.jumpHeight * -2 * playerController.gravity);
        }

        if (playerController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }


        playerController.transform.Rotate(new Vector3(0f, x * playerController.rotationSpeed, 0f));

        // start dashing
        if (Input.GetButtonDown("Fire3") && playerController.dashCooldown <= 0)
        {
            playerController.ChangeState(new PlayerDashing(playerController));
        }
    }
}
