using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudiencepersonController : MonoBehaviour
{
    public List<Sprite> possibleSprites;
    public float speedPerHypeMultiplier;
    public float minJumpTime;
    public float maxJumpTime;

    private int hype = 0;

    private Vector3 initialPosition;
    private Vector3 jumpVector;

    private bool isReturning = false;
    private float jumpTime;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = possibleSprites[Random.Range(0, possibleSprites.Count)];
        initialPosition = transform.position;
        Jump();
    }
    public void SetHype(int hype)
    {
        this.hype = hype;
    }

    private void Jump()
    {
        jumpTime = Random.Range(minJumpTime, maxJumpTime);
        jumpVector = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(0.5f, 1), Random.Range(-0.1f, 0.1f));
    }

    void Update()
    {

        jumpTime -= Time.deltaTime;
        if (jumpTime <= 0) isReturning = true;

        transform.position += (isReturning ? -1 : 1) * jumpVector * (speedPerHypeMultiplier * (hype + 1)) * Time.deltaTime;

        if (isReturning && transform.position.y < initialPosition.y)
        {
            isReturning = false;
            Jump();
        }
    }
}
