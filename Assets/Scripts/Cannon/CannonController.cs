using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public DiceController dicePrefab;
    public Transform Spawnpoint;

    public float minLaunchVelocityX;
    public float minLaunchVelocityY;
    public float minLaunchVelocityZ;
    public float maxLaunchVelocityX;
    public float maxLaunchVelocityY;
    public float maxLaunchVelocityZ;
    public float maxChargeTime = 2;

    private bool isCharging = false;
    private float chargeTime = 0;
    private float nrOfDiceLoaded = 0;
    private float nrOfPowerUpsLoaded = 0;

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
       
    }

    // Update is called once per frame
    void Update()
    {
        if (isCharging && (nrOfDiceLoaded > 0 || nrOfPowerUpsLoaded > 0))
        {
            chargeTime += Time.deltaTime;

            if (chargeTime >= maxChargeTime)
            {
                SoundBank.PlayAudioClip(SoundBank.GetInstance().cannonShotAudioClips, AudioSource);

                DiceController dc = Instantiate(dicePrefab);
                dc.transform.position = Spawnpoint.position;
                if (nrOfDiceLoaded <= 0)
                {
                    dc.IsPowerUp = true;
                    nrOfPowerUpsLoaded--;
                }
                else
                {
                    nrOfDiceLoaded--;
                }
                
                dc.Launch(new Vector3(Random.Range(minLaunchVelocityX, maxLaunchVelocityX), Random.Range(minLaunchVelocityY, maxLaunchVelocityY), Random.Range(minLaunchVelocityZ, maxLaunchVelocityZ)));

                chargeTime = 0;
                isCharging = false;
            }
        }
        else
        {
            // Shoot at all times L0l
            ChargeAndShoot();
        }
    }

    /// <summary>
    /// First charges the cannon up and then shoots out a dice.
    /// Returns false if the cannon is already 'busy'
    /// </summary>
    public bool ChargeAndShoot()
    {
        if (isCharging) return false;


        isCharging = true;
        return true;
    }

    public void LoadEnemyDice(int nrOfDice)
    {
        nrOfDiceLoaded += nrOfDice;
    }

    public void LoadRewardDice(int nrOfDice)
    {
        nrOfPowerUpsLoaded += nrOfDice;
    }
}
