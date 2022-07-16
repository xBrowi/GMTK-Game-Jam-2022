using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// When the enemy itself has not yet spawned from the dice because it is still rolling.
/// </summary>
public class EnemySpawn : EnemyState
{
    public EnemySpawn(EnemyController enemyController) : base(enemyController)
    {

    }

    public override void OnStateEnter()
    {
        enemyController.body.gameObject.SetActive(true);
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateUpdate()
    {

    }
}
