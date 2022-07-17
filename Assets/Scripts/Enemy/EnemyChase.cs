using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : EnemyState
{
    public NavMeshAgent navMeshAgent;
    public Transform playerTransform;
    public EnemyChase(EnemyController enemyController) : base(enemyController)
    {

    }

    public override void OnStateEnter()
    {
        enemyController.GetComponent<NavMeshAgent>().enabled =  true;
        navMeshAgent = enemyController.GetComponent<UnityEngine.AI.NavMeshAgent>();
        playerTransform = GameObject.Find("Player").transform;
    }

    public override void OnStateExit()
    {

        enemyController.GetComponent<NavMeshAgent>().enabled = false;
    }

    public override void OnStateUpdate()
    {
        if (navMeshAgent.isOnNavMesh)
        navMeshAgent.destination = playerTransform.position;
    }
}
