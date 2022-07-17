using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkliftIdleSoundController : MonoBehaviour
{
    public AudioClip startUpClip;
    public AudioClip loopClip;
    public AudioClip turnDownClip;


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
        
    }

    public void TurnOn()
    {

    }

    public void TurnOff()
    {

    }
}
