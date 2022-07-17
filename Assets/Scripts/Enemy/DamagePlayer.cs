using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out PlayerController PC))
        {
            PC.health -= PC.damageTaken;
            Debug.Log("Damaged an enemy");
            PC.updateHeathBar();
            if (PC.health <= 0)
            {
                GameObject.Find("GameController").GetComponent<GameController>().Lose();
            }
        }
    }
}
