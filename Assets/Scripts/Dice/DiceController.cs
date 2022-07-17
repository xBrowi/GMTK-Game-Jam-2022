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


    public EnemyController enemyPrefab;

    public float stoppingAngularVelocity;
    public float stoppingVelocity;

    public float minEnemyFlingForce;
    public float maxEnemyFlingForce;

    private Rigidbody rb;
    private new ParticleSystem particleSystem;

    private bool isRolling = false;
    private bool enemiesSpawned = false;
    private float selfDestructTimer = 3;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        particleSystem = GetComponent<ParticleSystem>();
        //Spin();
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

            SpawnEnemies(rollResult);
        }

        if (enemiesSpawned == true)
        {
            selfDestructTimer -= Time.deltaTime;

            if (selfDestructTimer < 0)
            {
                Destroy(this.gameObject);
            }
        }

    }


    private void SpawnEnemies(int amount)
    {
        particleSystem.Play();
        GetComponent<Collider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        

        for (int i = 0; i < amount; i++)
        {
            EnemyController ec = Instantiate(enemyPrefab);

            ec.transform.position = transform.position;
            ec.Rigidbody.velocity = new Vector3(Random.Range(minEnemyFlingForce, maxEnemyFlingForce), Random.Range(minEnemyFlingForce, maxEnemyFlingForce * 2), Random.Range(minEnemyFlingForce, maxEnemyFlingForce));

        }

        enemiesSpawned = true;
    }


    public void Launch(Vector3 initVelocity)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = initVelocity;
        rb.angularVelocity = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), Random.Range(-5, 5));
        isRolling = true;

    }

}
