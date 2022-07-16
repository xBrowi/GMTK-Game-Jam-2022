using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDriving : PlayerState
{



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

        // start dashing
        if (Input.GetKeyDown(KeyCode.LeftShift) && playerController.dashCooldown <= 0)
        {
            playerController.ChangeState(new PlayerDashing(playerController));
        }

        // start attacking
        if (Input.GetMouseButtonDown(0)) 
        {
            playerController.ChangeState(new PlayerAttacking(playerController));
        }
    }
    public override void OnStateFixedUpdate()
    {
    }



    
}
