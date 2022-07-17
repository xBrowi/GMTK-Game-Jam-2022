using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBodyController : MonoBehaviour
{
    public EnemyController enemyController;
    public void OnSpawnAnimFinished()
    {
        enemyController.ChangeState(new EnemyIdle(enemyController));
    }
}
