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
    }

    public override void OnStateExit()
    {
        enemyController.animator.SetBool("isSpawning", false);
    }

    public override void OnStateUpdate()
    {
        // Check if stopped rolling
        if(!spawnAnimStarted && stoppingAngularVelocity > enemyController.Rigidbody.angularVelocity.magnitude && stoppingVelocity > enemyController.Rigidbody.velocity.magnitude)
        {
            enemyController.body.gameObject.SetActive(true);
            enemyController.animator.SetBool("isSpawning", true);
            enemyController.body.transform.rotation = new Quaternion(0, Random.Range(0, 360), 0, 0);
            enemyController.body.transform.position = enemyController.head.transform.position - new Vector3(0, 1.5f, 0);

            enemyController.head.GetComponent<Collider>().enabled = false;
            enemyController.bodyCollider.enabled = true;
            enemyController.head.transform.SetParent(enemyController.torso.transform);
            enemyController.Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

            spawnAnimStarted = true;
        }

    }
}
