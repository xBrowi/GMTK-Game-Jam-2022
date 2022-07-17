using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Serializable]
    public class WaveDefinition
    {
        public int nrOfDice;
        public float timeUntilNextWave;
    }

    public int currentWave = 0;
    public List<WaveDefinition> waveDefinitions;
    public MusicController musicController;
    public AudienceController audienceController;

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

        }
    }

    public void StartWaves()
    {
        hasEnteredArena = true;
        timeLeftUntilNextWave = waveDefinitions[0].timeUntilNextWave;
    }

    
}
