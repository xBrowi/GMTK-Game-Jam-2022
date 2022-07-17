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
    }

    public override void OnStateExit()
    {

        enemyController.transform.GetChild(1).GetComponent<BoxCollider>().enabled = false;
    }

    public override void OnStateUpdate()
    {
    }

    
}
