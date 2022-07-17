using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    private float Health;
    void Start()
    {
        Health = maxHealth;
    }

    void Update()
    {
        if (Health <= 0)
        {
            Die();
        }
    }
    
    void Die()
    {

    }
}
