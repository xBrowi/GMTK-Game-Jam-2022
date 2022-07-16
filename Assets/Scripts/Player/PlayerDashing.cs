using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashing : PlayerState
{
    float dashTimeLeft;
    GameObject dashTrail = GameObject.Find("dashTrailRendererObject");


    public PlayerDashing(PlayerController playerController) : base(playerController)
    {
        this.playerController = playerController;
    }

    public override void OnStateEnter()
    {
        dashTimeLeft = playerController.dashTime;

        playerController.rb.AddRelativeForce(new Vector3(0, 0, playerController.dashSpeed), ForceMode.Impulse);
        //velocity = playerController.transform.TransformDirection(Vector3.left) * playerController.dashSpeed;

        dashTrail.GetComponent<TrailRenderer>().emitting = true;


    }
    public override void OnStateExit()
    {
        playerController.dashCooldown = playerController.dashCooldownMax;

        dashTrail.GetComponent<TrailRenderer>().emitting = false;

    }
    public override void OnStateUpdate()
    {
        //playerController.controller.Move(velocity * Time.deltaTime);

        dashTimeLeft -= Time.deltaTime;
        if (dashTimeLeft <= 0)
        {
            playerController.ChangeState(new PlayerDriving(playerController));
        }
    }
}