using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    public PlayerState(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    public PlayerController playerController;

    public abstract void OnStateEnter();
    public abstract void OnStateExit();
    public abstract void OnStateUpdate();
}
