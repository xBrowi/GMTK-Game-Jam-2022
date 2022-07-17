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

    private enum ForkliftIdleState
    {
        StartUp, Loop, TurnDown
    }

    private ForkliftIdleState state = ForkliftIdleState.TurnDown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case ForkliftIdleState.StartUp:
                if (!AudioSource.isPlaying)
                {
                    AudioSource.clip = loopClip;
                    AudioSource.loop = true;
                    AudioSource.Play();
                }
                break;
            case ForkliftIdleState.Loop:
                break;
            case ForkliftIdleState.TurnDown:
                break;
            default:
                break;
        }
    }

    public void TurnOn()
    {
        if (state == ForkliftIdleState.TurnDown)
        {
            Debug.Log("Turning engine sound on");
            state = ForkliftIdleState.StartUp;
            AudioSource.clip = startUpClip;
            AudioSource.loop = false;
            AudioSource.Play();
        }
    }

    public void TurnOff()
    {
        if (state != ForkliftIdleState.TurnDown)
        {
            Debug.Log("Turning engine sound off");
            state = ForkliftIdleState.TurnDown;
            AudioSource.clip = turnDownClip;
            AudioSource.loop = false;
            AudioSource.Play();
        }
    }
}
