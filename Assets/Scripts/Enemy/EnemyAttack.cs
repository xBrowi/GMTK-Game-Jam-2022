using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : EnemyState
{
    public EnemyAttack(EnemyController enemyController) : base(enemyController)
    {

    }

    public override void OnStateEnter()
    {
        enemyController.animator.SetBool("isSwiping", true);
        enemyController.animator.SetBool("isRunning", false);
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateUpdate()
    {
    }

    
}
