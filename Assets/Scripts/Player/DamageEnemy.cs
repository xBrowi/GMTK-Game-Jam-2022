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

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out EnemyController EC))
        {
            EC.Health -= playerController.damage;
            Debug.Log("Damaged an enemy");
            if (EC.Health <= 0)
            {
                EC.Die();
            }
        }
    }
}
