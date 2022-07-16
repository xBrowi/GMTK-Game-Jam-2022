using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDriving : PlayerState
{

    [SerializeField] Vector3 velocity;

    public float turnSmoothTime = 1f;



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
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

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
            playerController.rb.AddRelativeForce(-z * playerController.accelleration, 0, 0, ForceMode.Acceleration);
        }

        if (Input.GetButtonDown("Jump") && playerController.isGrounded)
        {
            playerController.rb.velocity = new Vector3(playerController.rb.velocity.x, 0, playerController.rb.velocity.z);
            playerController.rb.AddForce(0, playerController.jumpForce, 0, ForceMode.VelocityChange);
        }


        playerController.transform.Rotate(new Vector3(0f, x * playerController.rotationSpeed, 0f));

        // start dashing
        if (Input.GetButtonDown("Fire3") && playerController.dashCooldown <= 0)
        {
            playerController.ChangeState(new PlayerDashing(playerController));
        }

        // sideways "drag"
        if (playerController.isGrounded)
        {
            Vector3 locVel = playerController.rb.transform.InverseTransformDirection(playerController.rb.velocity);
            int isNegative = Math.Sign(locVel.z);
            locVel.z += isNegative * playerController.sidewaysDrag * Time.deltaTime;

            if (isNegative == -1 && locVel.z > 0) locVel.z = 0;
            if (isNegative == 1 && locVel.z < 0) locVel.z = 0;
            playerController.rb.velocity = playerController.rb.transform.TransformDirection(locVel);
        }
    }
}
