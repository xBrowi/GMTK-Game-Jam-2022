using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceController : MonoBehaviour
{
    public Transform up;
    public Transform down;
    public Transform left;
    public Transform right;
    public Transform front;
    public Transform back;

    public float stoppingAngularVelocity;
    public float stoppingVelocity;

    private Rigidbody rb;
    private bool isRolling = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Spin();
    }


    public void Spin()
    {
        rb.velocity = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f));
        rb.angularVelocity = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f));
        isRolling = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (isRolling && stoppingAngularVelocity > rb.angularVelocity.magnitude && stoppingVelocity > rb.velocity.magnitude)
        {
            float highestY = up.position.y;
            int rollResult = 6;

            if (down.position.y > highestY) {
                highestY = down.position.y;
                rollResult = 1;
            }
            if (left.position.y > highestY)
            {
                highestY = left.position.y;
                rollResult = 2;
            }
            if (right.position.y > highestY)
            {
                highestY = right.position.y;
                rollResult = 4;
            }
            if (front.position.y > highestY)
            {
                highestY = front.position.y;
                rollResult = 5;
            }
            if (back.position.y > highestY)
            {
                rollResult = 3;
            }

            Debug.Log($"Dice landed on a {rollResult}!");
            isRolling = false;
        }
    }
}
