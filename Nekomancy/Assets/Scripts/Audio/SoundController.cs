using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

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
    [Header("Sound Map")]
    [SerializeField()] private AudioSource NoSound;
    [SerializeField()] private AudioSource AdventureLoop;
    [SerializeField()] private AudioSource CatMusic;
    [SerializeField()] private AudioSource CatStrike;
    [SerializeField()] private AudioSource FishingStrike;
    [SerializeField()] private AudioSource HowlingWind;
    [SerializeField()] private AudioSource JumpDown;
    [SerializeField()] private AudioSource JumpUp;
    [SerializeField()] private AudioSource NekomancerBoth;
    [SerializeField()] private AudioSource NekomancerClarinet;
    [SerializeField()] private AudioSource NekomancerViolin;
    [SerializeField()] private AudioSource PickUpBone;
    [SerializeField()] private AudioSource RavenMusic;
    [SerializeField()] private AudioSource RavenStrike;
    [SerializeField()] private AudioSource WalkLeft;
    [SerializeField()] private AudioSource WalkRight;

    [Header("Behaviors")]
    [Tooltip("Throw Exception When Sound Not Found")]
    [SerializeField()] private bool ThrowExceptionWhenSoundNotFound = true;


    private Dictionary<SoundId, AudioSource> audioSourceFromSoundId;
    private Dictionary<SoundId, float> volumeSettingBySoundId;

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

        volumeSettingBySoundId = new Dictionary<SoundId, float>();
        foreach(SoundId id in audioSourceFromSoundId.Keys)
        {
            if (audioSourceFromSoundId[id] != null)
            {
                volumeSettingBySoundId[id] = audioSourceFromSoundId[id].volume;
            }
        }
    }

    private AudioSource getAudioSource(SoundId id)
    {
        if (audioSourceFromSoundId.ContainsKey(id))
        {
            return audioSourceFromSoundId[id];
        }

        if (ThrowExceptionWhenSoundNotFound)
        {
            throw new System.Exception($"Sound Id {id} is not known");
        }
        else
        {
            Debug.LogError($"Sound Id {id} is not known");
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
        if (audioSource.isPlaying && restart)
        {
            audioSource.Stop();
        }

        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
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
        volumeSettingBySoundId[soundId] = newVolume;
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

    public void FadeIn(SoundId soundId, float fadeTime)
    {
        if (!IsPlaying(soundId))
        {
            Coroutine c = StartCoroutine(fadeInCoroutine(soundId, volumeSettingBySoundId[soundId], fadeTime));
        }
    }

    public void FadeOut(SoundId soundId, float fadeTime)
    {
        if (IsPlaying(soundId))
        {
            Coroutine c = StartCoroutine(fadeOutCoroutine(Guid.NewGuid(), soundId, volumeSettingBySoundId[soundId], fadeTime));
        }
    }

    public IEnumerator fadeOutCoroutine(Guid coroutineKey, SoundId soundId, float normalVolume, float FadeTime)
    {
        AudioSource audioSource = getAudioSource(soundId);
        while (audioSource.volume > 0)
        {
            audioSource.volume -= normalVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = normalVolume;
    }

    public IEnumerator fadeInCoroutine(SoundId soundId, float normalVolume, float FadeTime)
    {
        AudioSource audioSource = getAudioSource(soundId);

        audioSource.volume = 0;
        audioSource.Play();

        while (audioSource.volume < normalVolume)
        {
            audioSource.volume += normalVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.volume = normalVolume;
    }

}
