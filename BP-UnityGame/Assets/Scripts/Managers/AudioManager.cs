using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using static UnityEngine.Rendering.DebugUI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }


    public AudioSource SFXAudioSource;
    public AudioSource MusicAudioSource;
    public AudioMixer AudioMixer;
    public AudioLibrary AudioLibrary;


    public float SFXVolume
    {
        get
        {
            float value = 0;
            AudioMixer.GetFloat("SFXVolume", out value);
            return value;
        }
        set
        {
            AudioMixer.SetFloat("SFXVolume", value);
        }
    }

    public float MusicVolume
    {
        get {
            float value = 0;
            AudioMixer.GetFloat("MusicVolume", out value);
            return value;
        }
        set
        {
            AudioMixer.SetFloat("MusicVolume", value);
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

    void UpdateAudioSettings(MainMenuSettings settings)
    {
        MusicVolume = AudioMixer.ConvertToDecibelValue(settings.MusicVolume);
        SFXVolume = AudioMixer.ConvertToDecibelValue(settings.SFXVolume);
    }

    void Update()
    {

    }

    public void PlayClipByName(string name, AudioSource audioSource)
    {
        AudioClip clip = null;

        foreach (var audioGroup in new[] { AudioLibrary.UI, AudioLibrary.Player })
        {
            NamedAudioClip namedClip = audioGroup.Clips.Find(clip => clip.Name == name);
            if (namedClip != null)
            {
                clip = namedClip.Clip;
                break;
            }
        }
        if (clip == null)
        {
            Debug.LogError("Clip not found: " + name);
            return;
        }
        audioSource.PlayOneShot(clip);
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
}

[System.Serializable]
public class AudioCategory
{
    public List<NamedAudioClip> Clips = new List<NamedAudioClip>();

    public AudioClip GetRandom()
    {
        if (Clips.Count == 0) return null;
        return Clips[Random.Range(0, Clips.Count)].Clip;
    }
}

[System.Serializable]
public class NamedAudioClip
{
    public string Name;
    public AudioClip Clip;
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