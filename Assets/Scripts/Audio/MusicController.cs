using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioClip intro;
    public AudioClip outro;

    public float timeBeforeMusicKicksIn;

    public List<MusicTrackController> tracks;

    private int currentIntensity = 0;
    private bool mainLoopStarted = false;

    private enum MusicState
    {
        Delay, Intro, Loop, Outro
    }

    private MusicState musicState = MusicState.Delay;

    private AudioSource audioSource;
    public AudioSource AudioSource
    {
        get
        {
            if (audioSource == null) audioSource = GetComponent<AudioSource>();
            return audioSource;
        }
    }


    void Update()
    {

        switch (musicState)
        {
            case MusicState.Delay:
                timeBeforeMusicKicksIn -= Time.deltaTime;
                if (timeBeforeMusicKicksIn <= 0)
                {
                    AudioSource.clip = intro;
                    AudioSource.loop = false;
                    AudioSource.Play();
                    musicState = MusicState.Intro;
                }

                break;
            case MusicState.Intro:
                if (!mainLoopStarted && !AudioSource.isPlaying)
                {
                    for (int i = 0; i < tracks.Count; i++)
                    {
                        tracks[i].Play();
                    }
                    mainLoopStarted = true;
                    musicState = MusicState.Loop;
                }
                break;
            case MusicState.Loop:
                break;
            case MusicState.Outro:
                break;
            default:
                break;
        }




        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            currentIntensity++;
            SetIntensity(currentIntensity);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            currentIntensity--;
            SetIntensity(currentIntensity);
        }
    }


    public void SetIntensity(int lvl)
    {
        Debug.Log($"Setting music intensity to {lvl}");
        for (int i = 0; i < tracks.Count; i++)
        {
            tracks[i].SetIntensity(lvl);
        }
    }


    public void PlayOutro()
    {
        AudioSource.clip = outro;
        AudioSource.Play();
        musicState = MusicState.Outro;

        for (int i = 0; i < tracks.Count; i++)
        {
            tracks[i].Stop();
        }
    }
}
