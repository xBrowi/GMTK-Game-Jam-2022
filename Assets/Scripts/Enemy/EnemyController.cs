using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject head;
    public GameObject body;

    [HideInInspector]
    public new Rigidbody rigidbody;

    private EnemyState currentState;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        currentState = new EnemyRolling(this);
        currentState.OnStateEnter();
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
}
