using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Serializable]
    public class WaveDefinition
    {
        public int nrOfDiceCannonLeft;
        public int nrOfDiceCannonRight;
        public float timeUntilNextWave;
        public string waveName;
        public int waveIntensity;
        public int nrOfRewardPowerUpsLeft;
        public int nrOfRewardPowerUpsRight;
    }

    public int currentWave = 0;
    public List<WaveDefinition> waveDefinitions;
    public MusicController musicController;
    public AudienceController audienceController;

    public CannonController cannonLeft;
    public CannonController cannonRight;

    private bool hasEnteredArena = false;
    private float timeLeftUntilNextWave;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (hasEnteredArena)
        {
            timeLeftUntilNextWave -= Time.deltaTime;

            if (timeLeftUntilNextWave <= 0)
            {
                currentWave++;
                SpawnWave(currentWave);
            }
        }
    }

    private void SpawnWave(int wave)
    {
        timeLeftUntilNextWave = waveDefinitions[wave].timeUntilNextWave;
        musicController.SetIntensity(waveDefinitions[wave].waveIntensity);
        audienceController.SetHype(waveDefinitions[wave].waveIntensity);
        cannonLeft.LoadEnemyDice(waveDefinitions[wave].nrOfDiceCannonLeft);
        cannonRight.LoadEnemyDice(waveDefinitions[wave].nrOfDiceCannonRight);
        cannonLeft.LoadRewardDice(waveDefinitions[wave].nrOfRewardPowerUpsLeft);
        cannonRight.LoadRewardDice(waveDefinitions[wave].nrOfRewardPowerUpsRight);

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Game is being started!");
        hasEnteredArena = true;

        SpawnWave(0);
    }
}
