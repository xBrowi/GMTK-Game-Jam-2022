using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacking : PlayerState
{

    float attackCooldown;

    public PlayerAttacking(PlayerController playerController) : base(playerController)
    {
        this.playerController = playerController;
    }

    public override void OnStateEnter()
    {

        playerController.playerAnimator.SetBool("isTurningLeft", false);
        playerController.playerAnimator.SetBool("isTurningRight", false);
        playerController.playerAnimator.SetBool("isAttacking", true);
        attackCooldown = playerController.attackCooldownMax;

    }
    public override void OnStateExit()
    {
        playerController.playerAnimator.SetBool("isAttacking", false);
    }
    public override void OnStateUpdate()
    {
        attackCooldown -= Time.deltaTime;
        if (attackCooldown <= 0)
        {
            playerController.ChangeState(new PlayerDriving(playerController));
        }
    }
    public override void OnStateFixedUpdate()
    {

    }




}
