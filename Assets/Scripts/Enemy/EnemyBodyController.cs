using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBodyController : MonoBehaviour
{
    public EnemyController enemyController;
    public void OnSpawnAnimFinished()
    {
        enemyController.ChangeState(new EnemyChase(enemyController));

        enemyController.Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        enemyController.GetComponent<EnemyLookAtPlayer>().enabled = true;
    }

    public void AttackAnimationFinish()
    {
        Debug.Log("test");

        enemyController.animator.SetBool("isSwiping", false);

            enemyController.ChangeState(new EnemyChase(enemyController));

    }

    public void EnemyDamage()
    {
        enemyController.transform.GetChild(1).GetComponent<BoxCollider>().enabled = true;
    }
}
