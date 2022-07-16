using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupCollection : MonoBehaviour
{
    public float speedBonus;
    public float jumpBonus;
    public float accelerationBonus;
    public float dashBonus;
    public float damageBonus;

    private PlayerController PC;
    private void Start()
    {
        PC = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            switch (other.tag)
            {
                case "speedPowerup":
                    PC.maxSpeed += speedBonus;
                    PC.accelleration += accelerationBonus;
                    break;
                case "jumpPowerup":
                    PC.jumpForce += jumpBonus;

                    break;
                case "dashPowerup":
                    PC.dashTime += dashBonus;
                    break;
                case "damagePowerup":
                    PC.damage += damageBonus;
                    break;
                default:
                    break;
            }

            Destroy(other.gameObject);
        }
    }
}
