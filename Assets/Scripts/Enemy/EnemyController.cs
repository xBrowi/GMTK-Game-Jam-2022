using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float maxHealth;
    public float Health;
    public float EnemySpeed;

    public float minDist = 5;
    public float dist;

    public GameObject head;
    public GameObject body;
    public GameObject torso;

    public Transform playerTransform;

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
        playerTransform = GameObject.Find("Player").transform;
    }

    void Update()
    {
        currentState.OnStateUpdate();


        dist = Vector3.Distance(playerTransform.position, transform.position);
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
