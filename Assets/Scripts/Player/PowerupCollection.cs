using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupCollection : MonoBehaviour
{


    private PlayerController PC;
    private void Start()
    {
        PC = GetComponent<PlayerController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision with " + collision.gameObject.layer);
        if (collision.gameObject.layer == 7) // PowerUp Layer
        {
            Debug.Log("Collected Powerup! " + collision.gameObject.name);

            PowerUpController puc = collision.gameObject.GetComponent<PowerUpController>();

            PC.maxSpeed += puc.speedBonus;
            PC.accelleration += puc.accelerationBonus;
            PC.jumpForce += puc.jumpBonus;
            PC.dashTime += puc.dashBonus;
            PC.damage += puc.damageBonus;

            PC.PlayUpgradeSound();
            Destroy(collision.gameObject);
        }


        
    }
}
