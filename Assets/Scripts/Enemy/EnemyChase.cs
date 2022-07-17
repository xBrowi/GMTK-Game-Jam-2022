using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : EnemyState
{
    public int wait = 0;

    public NavMeshAgent navMeshAgent;
    public EnemyChase(EnemyController enemyController) : base(enemyController)
    {

    }

    public override void OnStateEnter()
    {
        enemyController.animator.SetBool("isRunning", true);

        Debug.Log("chase enter");
    }

    public override void OnStateExit()
    {
        enemyController.animator.SetBool("isRunning", false);
    }

    public override void OnStateUpdate()
    {
        enemyController.Rigidbody.AddRelativeForce(Vector3.forward*4, ForceMode.Force);
        if (enemyController.Rigidbody.velocity.magnitude > enemyController.EnemySpeed)
        {
            enemyController.Rigidbody.velocity = enemyController.Rigidbody.velocity.normalized * enemyController.EnemySpeed;
        }


        if (enemyController.dist < enemyController.minDist && wait > 5)
        {
            enemyController.ChangeState(new EnemyAttack(enemyController));
            Debug.Log("AAAaAa");
        }

    wait++;
    }

}
