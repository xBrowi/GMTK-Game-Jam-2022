using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState
{
    public EnemyController enemyController;

    public EnemyState(EnemyController enemyController)
    {
        this.enemyController = enemyController;
    }
    public abstract void OnStateEnter();
    public abstract void OnStateExit();
    public abstract void OnStateUpdate();

}
