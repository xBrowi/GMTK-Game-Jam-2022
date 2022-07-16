using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerState currentState;
    public CharacterController controller;
    public GameObject player;

    // Player movement variables
    public float groundDistance = 1f;
    public float rotationSpeed = 0.1f;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3;
    public GameObject groundCheck;
    public LayerMask groundMask;

    public int dashCooldownMax;
    public int dashTime;
    public float dashSpeed;

    public int dashCooldown = 0;

    private 
    void Start()
    {
        currentState = new PlayerIdle(this);
        currentState.OnStateEnter();
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        if (currentState != null)
        {
            currentState.OnStateUpdate();
        }

        if (dashCooldown > 0)
        {
            dashCooldown--;
        }

    }
    public void ChangeState(PlayerState newState)
    {
        currentState.OnStateExit();
        currentState = newState;
        newState.OnStateEnter();
    }
}
