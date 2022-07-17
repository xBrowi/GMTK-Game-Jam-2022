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


        //velocity = playerController.transform.TransformDirection(Vector3.left) * playerController.dashSpeed;
        dashTrail.GetComponent<TrailRenderer>().emitting = true;
        SoundBank.PlayAudioClip(SoundBank.GetInstance().playerDashAudioClips, playerController.AudioSource);


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
        Debug.Log(dashTimeLeft);
        if (dashTimeLeft <= 0)
        {
            playerController.ChangeState(new PlayerDriving(playerController));
        }


        Vector3 dashVector = playerController.transform.forward * playerController.dashSpeed;

        dashVector = Quaternion.Euler(0, -90, 0) * dashVector;

        playerController.rb.velocity = dashVector;
    }

    public override void OnStateFixedUpdate()
    {
    }
}
