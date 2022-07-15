using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerState currentState;

    void Update()
    {
        currentState.OnStateUpdate();
    }
}
