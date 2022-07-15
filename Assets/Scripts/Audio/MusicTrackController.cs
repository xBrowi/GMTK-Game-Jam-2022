using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrackController : MonoBehaviour
{
    /// <summary>
    /// The volume change per second
    /// </summary>
    public float volumeChangePerSecond;

    private AudioSource audioSource;
    public AudioSource AudioSource{
        get{
            if (audioSource == null) audioSource = GetComponent<AudioSource>();
            return audioSource;
        }
    }

    private int currentIntensity = 0;

    /// <summary>
    /// The lowest intensity is 0 and the highest is the length of this list. For each intensity the volume can 
    /// </summary>
    public List<float> volumeAtIntensityLevel = new List<float>();


    void Update()
    {
        // Fade to wanted volume if it is not already reached
        if (audioSource.volume > volumeAtIntensityLevel[currentIntensity])
        {
            audioSource.volume -= Time.deltaTime * volumeChangePerSecond;
            if (audioSource.volume < volumeAtIntensityLevel[currentIntensity]) audioSource.volume = volumeAtIntensityLevel[currentIntensity];
        }
        else if(audioSource.volume < volumeAtIntensityLevel[currentIntensity])
        {
            audioSource.volume += Time.deltaTime * volumeChangePerSecond;
            if (audioSource.volume > volumeAtIntensityLevel[currentIntensity]) audioSource.volume = volumeAtIntensityLevel[currentIntensity];
        }
    }
}
