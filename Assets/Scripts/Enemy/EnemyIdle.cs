using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : EnemyState
{
    public EnemyIdle(EnemyController enemyController) : base(enemyController)
    {

    }

    public override void OnStateEnter()
    {
        enemyController.animator.SetBool("isIdle", true);
    }

    public override void OnStateExit()
    {
        enemyController.animator.SetBool("isIdle", false);
    }

    public override void OnStateUpdate()
    {
    }
}
