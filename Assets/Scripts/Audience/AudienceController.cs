using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceController : MonoBehaviour
{
    public AudiencepersonController[] audiencepersonControllers;
    public List<AudioClip> audienceAudioAtHype;

    private AudioSource audioSource;
    public AudioSource AudioSource
    {
        get
        {
            if(audioSource == null) audioSource = GetComponent<AudioSource>();
            return audioSource;
        }
    }

    private int hype = 0;

    void Start()
    {
        audiencepersonControllers = GameObject.FindObjectsOfType<AudiencepersonController>();
        SetHype(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            hype++;
            SetHype(hype);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            hype--;
            SetHype(hype);
        }
    }

    public void SetHype(int newHype)
    {
        //if (newHype >= audienceAudioAtHype.Count) return;
        //audioSource.clip = audienceAudioAtHype[newHype];
        hype = newHype;

        for (int i = 0; i < audiencepersonControllers.Length; i++)
        {
            audiencepersonControllers[i].SetHype(hype);
        }
    }
}
