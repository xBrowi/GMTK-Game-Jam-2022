using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Serializable]
    public class WaveDefinition
    {
        public int nr;
        public int nrOfDice;
    }

    public int wave = 1;


    public List<WaveDefinition> waveDefinitions;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
