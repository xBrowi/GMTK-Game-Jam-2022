using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The soundbank stores references to all sound effects.
/// Soundeffects that are interchangable (e.g. 'card draw' varations) can be fetched at random
/// </summary>
public class SoundBank : MonoBehaviour
{
    private static SoundBank instance;
    public static SoundBank GetInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<SoundBank>();
        }
        return instance;
    }

    public List<AudioClip> playerJumpAudioClips;
    public List<AudioClip> playerDashAudioClips;
    public List<AudioClip> playerDeathAudioClips;
    public List<AudioClip> playerImpactAudioClips;
    public List<AudioClip> playerUpgradeAudioClips;
    public List<AudioClip> playerAttackAudioClips;
    public List<AudioClip> cannonShotAudioClips;
    public List<AudioClip> diceSplitAudioClips;
    public List<AudioClip> diceImpactBigAudioClips;
    public List<AudioClip> diceImpactSmallAudioClips;


    /// <summary>
    /// Returns a random audio clip from a list of audio clips
    /// </summary>
    /// <param name="audioClips"></param>
    /// <returns></returns>
    private static AudioClip GetRandomAudioClip(List<AudioClip> audioClips)
    {
        if (audioClips == null || audioClips.Count == 0) return null;

        return audioClips[Random.Range(0, audioClips.Count)];
    }

    /// <summary>
    /// Plays a random audio clip from a list of clips on a given audioSource
    /// </summary>
    /// <param name="audioClips">The audioClip to play. E.g. pick any of the public Sound collections attached to the soundbank like this: SoundBank.GetInstance()...audioClips</param>
    /// <param name="audioSource">audioSource on which the clip is played (provided by and probably attached to the caller of this method)</param>
    public static void PlayAudioClip(List<AudioClip> audioClips, AudioSource audioSource)
    {
        audioSource.clip = GetRandomAudioClip(audioClips);
        audioSource.Play();
    }
}
