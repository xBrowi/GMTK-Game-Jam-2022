using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// When the enemy itself has not yet spawned from the dice because it is still rolling.
/// </summary>
public class EnemySpawn : EnemyState
{
    private float stoppingAngularVelocity = 0.1f;
    private float stoppingVelocity = 0.1f;

    private bool spawnAnimStarted = false;

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
        // Check if stopped rolling
        if(stoppingAngularVelocity > enemyController.rigidbody.angularVelocity.magnitude && stoppingVelocity > enemyController.rigidbody.velocity.magnitude)
        {
            enemyController.animator.SetBool("isSpawning", true);
            spawnAnimStarted = true;
        }

    }
}
