using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashing : PlayerState
{
    int Dashing;
    Vector3 velocity;
    GameObject dashTrail = GameObject.Find("dashTrailRendererObject");
    Vector3 trailVectorPosition;
    public PlayerDashing(PlayerController playerController) : base(playerController)
    {
        this.playerController = playerController;
    }

    public override void OnStateEnter()
    {
        Dashing = playerController.dashTime;

        velocity = playerController.player.transform.TransformDirection(Vector3.forward) * playerController.dashSpeed;

        dashTrail.GetComponent<TrailRenderer>().emitting = true;


    }
    public override void OnStateExit()
    {
        playerController.dashCooldown = playerController.dashCooldownMax;

        dashTrail.GetComponent<TrailRenderer>().emitting = false;

    }
    public override void OnStateUpdate()
    {
        playerController.controller.Move(velocity * Time.deltaTime);
        Dashing--;
        if (Dashing <= 0)
        {
            playerController.ChangeState(new PlayerIdle(playerController));
        }
    }
}
