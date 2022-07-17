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

        navMeshAgent.enabled=false;
    }

    public override void OnStateUpdate()
    {
        
    }
}
