using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupCollection : MonoBehaviour
{
    public float speedBonus;
    public float jumpBonus;
    public float accelerationBonus;

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
                default:
                    break;
            }

            Destroy(other.gameObject);
        }
    }
}
