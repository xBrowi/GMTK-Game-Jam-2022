using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : PlayerState
{
    public PlayerIdle(PlayerController playerController) : base(playerController)
    {
        this.playerController = playerController;
    }

    public override void OnStateEnter()
    {

    }

    public override void OnStateExit()
    {
    }

    public override void OnStateUpdate()
    {
    }
}
