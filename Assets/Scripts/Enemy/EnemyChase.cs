using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : EnemyState
{
    public NavMeshAgent navMeshAgent;
    public Transform playerTransform;
    public float minDist = 5;
    public EnemyChase(EnemyController enemyController) : base(enemyController)
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
        enemyController.Rigidbody.AddRelativeForce(Vector3.forward*4, ForceMode.Force);
        if (enemyController.Rigidbody.velocity.magnitude > enemyController.EnemySpeed)
        {
            enemyController.Rigidbody.velocity = enemyController.Rigidbody.velocity.normalized * enemyController.EnemySpeed;
        }
    }
}
