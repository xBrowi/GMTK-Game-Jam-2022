using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemy : MonoBehaviour
{

    public PlayerController playerController;
    private void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>(); ;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.TryGetComponent(out EnemyController EC))
        {
            EC.Health -= playerController.damage;
        }
    }
}
