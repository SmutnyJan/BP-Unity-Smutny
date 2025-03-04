using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }


    public AudioSource SFXAudioSource;
    public AudioSource MusicAudioSource;
    public AudioMixer AudioMixer;
    public AudioLibrary AudioLibrary;

    public enum PlayType
    {
        Play,
        PlayOneShot
    }


    public float SFXVolume
    {
        get
        {
            float value = 0;
            AudioMixer.GetFloat("SFXVolume", out value);
            return AudioMixer.ConvertToNormalizedValue(value);
        }
        set
        {
            AudioMixer.SetFloat("SFXVolume", AudioMixer.ConvertToDecibelValue(value));
        }
    }

    public float MusicVolume
    {
        get
        {
            float value = 0;
            AudioMixer.GetFloat("MusicVolume", out value);
            return AudioMixer.ConvertToNormalizedValue(value);
        }
        set
        {
            AudioMixer.SetFloat("MusicVolume", AudioMixer.ConvertToDecibelValue(value));
        }
    }



    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);


        }
        else
        {
            Destroy(gameObject);
        }

    }

    void Start()
    {
        SaveLoadManager.Instance.OnSettingsLoaded += UpdateAudioSettings;

    }

    public void UpdateAudioSettings(MainMenuSettings settings)
    {
        MusicVolume = settings.MusicVolume;
        SFXVolume = settings.SFXVolume;
    }

    void Update()
    {

    }

    public void PlayClipByName(string name, AudioCategory audioCategory, AudioSource audioSource, PlayType playType = PlayType.PlayOneShot)
    {
        NamedAudioClip namedClip = audioCategory.Clips.Find(clip => clip.Name == name);

        if (namedClip == null)
        {
            Debug.LogError("Clip not found: " + name);
            return;
        }
        switch (playType)
        {
            case PlayType.PlayOneShot:
                audioSource.PlayOneShot(namedClip.Clip);
                break;

            case PlayType.Play:
                audioSource.clip = namedClip.Clip;
                audioSource.Play();
                break;
        }

    }

    private void PlayCustomClipOnAudioSource(AudioClip clip, AudioSource audioSource)
    {
        audioSource.PlayOneShot(clip);
    }

    public void PlayRandomSFXFromCategory(AudioCategory audioCategory)
    {
        AudioClip randomClip = audioCategory.GetRandom();
        PlayCustomClipOnAudioSource(randomClip, SFXAudioSource);
    }
}

[System.Serializable]
public class AudioLibrary
{
    public AudioCategory UI;
    public AudioCategory Player;
    public AudioCategory Music;
}

[System.Serializable]
public class AudioCategory
{
    public List<NamedAudioClip> Clips = new();

    public AudioClip GetRandom()
    {
        if (Clips.Count == 0) return null;
        return Clips[Random.Range(0, Clips.Count)].Clip;
    }
}

[System.Serializable]
public class NamedAudioClip
{
    public AudioClip Clip;

    [HideInInspector]
    public string Name
    {
        get
        {
            return Clip != null ? Clip.name : string.Empty;
        }
    }
}


public static class AudioMixerExtensions
{

    public static float ConvertToNormalizedValue(this AudioMixer audioMixer, float dBVolume)
    {
        dBVolume = Mathf.Clamp(dBVolume, -80f, 0f);
        return (dBVolume + 80f) / 80f;
    }

    public static float ConvertToDecibelValue(this AudioMixer audioMixer, float normalizedValue)
    {
        normalizedValue = Mathf.Clamp01(normalizedValue);
        return (normalizedValue * 80f) - 80f;
    }
}