using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float maxHealth;
    public float Health;

    public GameObject head;
    public GameObject body;
    public GameObject torso;

    private EnemyState currentState;

    public Animator animator;

    public Collider bodyCollider;

    private new Rigidbody rigidbody;
    public Rigidbody Rigidbody
    {
        get
        {
            if (rigidbody == null) rigidbody = GetComponent<Rigidbody>();
            return rigidbody;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        currentState = new EnemySpawn(this);
        currentState.OnStateEnter();
        Health = maxHealth;
    }

    void Update()
    {
        currentState.OnStateUpdate();
    }

    public void ChangeState(EnemyState newState)
    {
        currentState.OnStateExit();
        currentState = newState;
        currentState.OnStateEnter();
    }

    public void Die ()
    {

    }
}
