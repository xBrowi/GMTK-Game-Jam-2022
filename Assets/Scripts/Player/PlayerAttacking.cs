using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacking : PlayerState
{

    public PlayerAttacking(PlayerController playerController) : base(playerController)
    {
        this.playerController = playerController;
    }

    public override void OnStateEnter()
    {

        playerController.playerAnimator.SetBool("isTurningLeft", false);
        playerController.playerAnimator.SetBool("isTurningRight", false);
        playerController.playerAnimator.SetBool("isAttacking", true);

        SoundBank.PlayAudioClip(SoundBank.GetInstance().playerAttackAudioClips, playerController.AudioSource);

    }

    public override void OnStateExit()
    {
        playerController.playerAnimator.SetBool("isAttacking", false);
        playerController.attackCooldown = playerController.attackCooldownMax;
        playerController.damageCollider.enabled = false;
    }
    public override void OnStateUpdate()
    {

    }
    public override void OnStateFixedUpdate()
    {

    }




}
