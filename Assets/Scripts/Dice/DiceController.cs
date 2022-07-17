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
    public List<GameObject> powerUps; 

    public float stoppingAngularVelocity;
    public float stoppingVelocity;

    public float minEnemyFlingForce;
    public float maxEnemyFlingForce;

    public bool IsPowerUp = false;

    private Rigidbody rb;
    private new ParticleSystem particleSystem;

    private bool isRolling = false;
    private bool enemiesSpawned = false;
    private float selfDestructTimer = 3;

    private AudioSource audioSource;
    public AudioSource AudioSource
    {
        get
        {
            if (audioSource == null) audioSource = GetComponent<AudioSource>();
            return audioSource;
        }
    }

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

            if (IsPowerUp)
            {
                SpawnPowerUps(rollResult);
            }
            else
            {
                SpawnEnemies(rollResult);
            }

            SoundBank.PlayAudioClip(SoundBank.GetInstance().diceSplitAudioClips, AudioSource);
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

    private void SpawnPowerUps(int nr)
    {
        particleSystem.Play();
        GetComponent<Collider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;

        GameObject powerUp;
        GameObject powerUp2 = null;

        switch (nr)
        {
            case 1:
                powerUp = Instantiate(powerUps[Random.Range(0, powerUps.Count)]);
                break;
            case 6:
                powerUp = Instantiate(powerUps[Random.Range(0, powerUps.Count)]);
                powerUp2 = Instantiate(powerUps[Random.Range(0, powerUps.Count)]);
                break;
            default:
                powerUp = Instantiate(powerUps[Random.Range(0, nr - 2)]);
                break;
        }

        powerUp.transform.position = transform.position;
        powerUp.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(minEnemyFlingForce, maxEnemyFlingForce), Random.Range(minEnemyFlingForce, maxEnemyFlingForce * 2), Random.Range(minEnemyFlingForce, maxEnemyFlingForce));

        if (powerUp2 != null)
        {
            powerUp2.transform.position = transform.position;
            powerUp2.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(minEnemyFlingForce, maxEnemyFlingForce), Random.Range(minEnemyFlingForce, maxEnemyFlingForce * 2), Random.Range(minEnemyFlingForce, maxEnemyFlingForce));
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


    private void OnCollisionEnter(Collision collision)
    {
        SoundBank.PlayAudioClip(SoundBank.GetInstance().diceImpactBigAudioClips, AudioSource);
    }

}
