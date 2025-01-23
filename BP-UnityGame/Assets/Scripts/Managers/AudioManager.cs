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

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            SaveLoadManager.Instance.OnSettingsLoaded += UpdateAudioSettings;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    void Start()
    {

    }

    void UpdateAudioSettings(MainMenuSettings settings)
    {
        AudioMixer.SetFloat("SFXVolume", settings.SFXVolume);
        AudioMixer.SetFloat("MusicVolume", settings.MusicVolume);
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