using System;

using UnityEngine;

public class SoundController : MonoBehaviour
{
    public BoolVariable IsMuteSound;
    public BoolVariable IsMuteMusic;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        IsMuteMusic.Raise();
        IsMuteSound.Raise();
    }

    // private void Update()
    // {
    //     IsMuteMusic.Raise();
    //     IsMuteSound.Raise();
    // }

    public AudioSource Music;
    public AudioSource Sound;

    public void PlaySoundEvent(AudioClip getAudioClip)
    {
        Sound.PlayOneShot(getAudioClip);
    }

    public void PlayMusicEvent(AudioClip getAudioClip)
    {
        Music.clip = getAudioClip;
        Music.Play();
    }

    // public SoundConfig SoundConfig => ConfigController.Sound;
    //
    // private void Awake()
    // {
    //     DontDestroyOnLoad(this);
    // }
    //
    // public void Start()
    // {
    //     Setup();
    //
    //     EventController.OnSoundChanged += Setup;
    // }
    //
    // public void Setup()
    // {
    //     BackgroundAudio.mute = !Data.BgSoundState;
    //     FxAudio.mute = !Data.FxSoundState;
    // }
    //
    // public void PlayFX(SoundType soundType)
    // {
    //     SoundData soundData = SoundConfig.GetSoundDataByType(soundType);
    //
    //     if (soundData != null)
    //     {
    //         FxAudio.PlayOneShot(soundData.GetRandomAudioClip());
    //     }
    //     else
    //     {
    //         Debug.LogWarning("Can't found sound data");
    //     }
    // }
    //
    // public void PlayBackground(SoundType soundType)
    // {
    //     SoundData soundData = SoundConfig.GetSoundDataByType(soundType);
    //
    //     if (soundData != null)
    //     {
    //         BackgroundAudio.clip = soundData.GetRandomAudioClip();
    //         BackgroundAudio.Play();
    //     }
    //     else
    //     {
    //         Debug.LogWarning("Can't found sound data");
    //     }
    // }
    //
    // public void PauseBackground()
    // {
    //     if (BackgroundAudio)
    //     {
    //         BackgroundAudio.Pause();
    //     }
    // }
}