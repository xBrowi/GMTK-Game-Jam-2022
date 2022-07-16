using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDriving : PlayerState
{
    float x;
    float z;


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
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        if (x < 0)
        {
            playerController.animator.SetBool("isTurningLeft", true);
            playerController.animator.SetBool("isTurningRight", false);

        }
        else if(x > 0){
            playerController.animator.SetBool("isTurningRight", true);
            playerController.animator.SetBool("isTurningLeft", false);
        }
        else
        {
            playerController.animator.SetBool("isTurningLeft", false);
            playerController.animator.SetBool("isTurningRight", false);
        }

        if (playerController.isGrounded) {
            acceleration();
        }

        if (Input.GetButtonDown("Jump") && playerController.isGrounded)
        {
            jump();
        }


        playerController.transform.Rotate(new Vector3(0f, x * playerController.rotationSpeed, 0f));

        // start dashing
        if (Input.GetButtonDown("Fire3") && playerController.dashCooldown <= 0)
        {
            playerController.ChangeState(new PlayerDashing(playerController));
        }

        
    }
    public override void OnStateFixedUpdate()
    {
        // drag
        if (playerController.isGrounded)
        {
            // sideways
            Vector3 locVel = playerController.rb.transform.InverseTransformDirection(playerController.rb.velocity);
            locVel.z *= playerController.sidewaysDrag;
            if (locVel.x < playerController.maxSpeed)
            {
                locVel.x *= playerController.forwardsDrag;
            }
            playerController.rb.velocity = playerController.rb.transform.TransformDirection(locVel);

        }


    }

    void jump()
    {
        playerController.rb.velocity = new Vector3(playerController.rb.velocity.x, 0, playerController.rb.velocity.z);
        playerController.rb.AddForce(0, playerController.jumpForce, 0, ForceMode.VelocityChange);
    }

    void acceleration()
    {
        playerController.rb.AddRelativeForce(-z * playerController.accelleration, 0, 0, ForceMode.Acceleration);
    }
}
