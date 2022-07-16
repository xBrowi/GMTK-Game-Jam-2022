using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// When the enemy itself has not yet spawned from the dice because it is still rolling.
/// </summary>
public class EnemyRolling : EnemyState
{
    public EnemyRolling(EnemyController enemyController) : base(enemyController)
    {

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
