using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundId
{
    AdventureLoop = 1,
    CatMusic,
    CatStrike,
    FishingStrike,
    HowlingWind,
    JumpUp,
    JumpDown,
    NekomancerBoth,
    NekomancerClarinet,
    NekomancerViolin,
    PickUpBone,
    RavenMusic,
    RavenStrike,
    WalkLeft,
    WalkRight,
}

public class SoundController : MonoBehaviour
{

    public bool ThrowExceptionWhenSoundNotFound = true;

    public AudioSource NoSound;
    public AudioSource AdventureLoop;
    public AudioSource CatMusic;
    public AudioSource CatStrike;
    public AudioSource FishingStrike;
    public AudioSource HowlingWind;
    public AudioSource JumpDown;
    public AudioSource JumpUp;
    public AudioSource NekomancerBoth;
    public AudioSource NekomancerClarinet;
    public AudioSource NekomancerViolin;
    public AudioSource PickUpBone;
    public AudioSource RavenMusic;
    public AudioSource RavenStrike;
    public AudioSource WalkLeft;
    public AudioSource WalkRight;


    private Dictionary<SoundId, AudioSource> audioSourceFromSoundId;

    void Awake()
    {
        audioSourceFromSoundId = new Dictionary<SoundId, AudioSource>() 
        {
            { SoundId.AdventureLoop, AdventureLoop },
            { SoundId.CatMusic, CatMusic },
            { SoundId.CatStrike, CatStrike },
            { SoundId.FishingStrike, FishingStrike },
            { SoundId.HowlingWind, HowlingWind },
            { SoundId.JumpDown, JumpDown },
            { SoundId.JumpUp, JumpUp },
            { SoundId.NekomancerBoth, NekomancerBoth },
            { SoundId.NekomancerClarinet, NekomancerClarinet },
            { SoundId.NekomancerViolin, NekomancerViolin },
            { SoundId.PickUpBone, PickUpBone },
            { SoundId.RavenMusic, RavenMusic },
            { SoundId.RavenStrike, RavenStrike },
            { SoundId.WalkLeft, WalkLeft },
            { SoundId.WalkRight, WalkRight},
        };
    }

    private AudioSource getAudioSource(SoundId soundId)
    {
        if (audioSourceFromSoundId.ContainsKey(soundId))
        {
            return audioSourceFromSoundId[soundId];
        }

        if (ThrowExceptionWhenSoundNotFound)
        {
            throw new System.Exception($"Sound Id {soundId} is not known");
        }
        else
        {
            Debug.LogError($"Sound Id {soundId} is not known");
            return NoSound;
        }
    }
    public bool IsPlaying(SoundId soundId)
    {
        AudioSource audioSource = getAudioSource(soundId);
        return audioSource.isPlaying;
    }


    public void Play(SoundId soundId, bool restart = false)
    {
        AudioSource audioSource = getAudioSource(soundId);
        if(audioSource.isPlaying && restart)
        {
            audioSource.Stop();
        }

        if(!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void PlayAfter(SoundId soundId, TimeSpan delay)
    {
        AudioSource audioSource = getAudioSource(soundId);

        new System.Threading.Timer((id) =>
        {
            SoundId sid = (SoundId)id;
            Play(sid, false);
        },
        audioSource, delay, TimeSpan.Zero);
    }

    public void Stop(SoundId soundId)
    {
        AudioSource audioSource = getAudioSource(soundId);
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    /// <summary>
    /// newVolume is a float between 0 and 1
    /// </summary>
    /// <param name="newVolume"></param>
    public void SetVolume(SoundId soundId, float newVolume)
    {
        AudioSource audioSource = getAudioSource(soundId);
        audioSource.volume = newVolume;
    }

    public void SetLoop(SoundId soundId, bool loop)
    {
        AudioSource audioSource = getAudioSource(soundId);
        audioSource.loop = loop;
    }

    public void SetMute(SoundId soundId, bool mute)
    {
        AudioSource audioSource = getAudioSource(soundId);
        audioSource.mute = mute;
    }

    /// <summary>
    /// The lower priority number, the higher the play priority.
    /// </summary>
    /// <param name="soundId"></param>
    /// <param name="priority">An integer between 0 and 256</param>
    public void SetPriority(SoundId soundId, int priority)
    {
        AudioSource audioSource = getAudioSource(soundId);
        audioSource.priority = priority;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="soundId"></param>
    /// <param name="pitch">a float between -3 and 3</param>
    public void SetPitch(SoundId soundId, float pitch)
    {
        AudioSource audioSource = getAudioSource(soundId);
        audioSource.pitch = pitch;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="soundId"></param>
    /// <param name="panStereo">a float between -1 (left) and +1 (right), 0 = center</param>
    public void SetStereoPan(SoundId soundId, float panStereo)
    {
        AudioSource audioSource = getAudioSource(soundId);
        audioSource.panStereo = panStereo;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="soundId"></param>
    /// <param name="spatialBlend">a float between 0 (2D) and 1 (3D)</param>
    public void SetSpatialBlend(SoundId soundId, float spatialBlend)
    {
        AudioSource audioSource = getAudioSource(soundId);
        audioSource.spatialBlend = spatialBlend;
    }
}
