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

        
    }
    public override void OnStateFixedUpdate()
    {
    }



    
}
