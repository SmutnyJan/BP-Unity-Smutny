<<<<<<< Updated upstream
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource DefaultSFXAudioSource;
    public AudioSource DefaultMusicAudioSource;
=======
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioSource AudioSourceSFX;
    public AudioSource AudioSourceMusic;
    public AudioMixer AudioMixer;

    public Dictionary<string, AudioClip> AudioClips;

    const string MIXER_MUSIX = "MusicVolume";
    const string MIXER_SFX = "SFXVolume";



    public static AudioManager Instance;
>>>>>>> Stashed changes

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
<<<<<<< Updated upstream
            DefaultSFXAudioSource = GetComponents<AudioSource>()[0];
            DefaultMusicAudioSource = GetComponents<AudioSource>()[1];
=======
            AudioSourceSFX = GetComponents<AudioSource>()[0];
            AudioSourceMusic = GetComponents<AudioSource>()[1];
>>>>>>> Stashed changes
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
<<<<<<< Updated upstream

    void Start()
    {
        
=======
    void Start()
    {

>>>>>>> Stashed changes
    }

    void Update()
    {
<<<<<<< Updated upstream
        
    }
}
=======

    }

    public void PlaySoundByName(string name, AudioSource audioSource)
    {
        AudioClip clip = AudioClips[name];
        if (clip == null)
        {
            throw new FileNotFoundException();
        }
        PlaySound(clip);


    }

    private void PlaySound(AudioClip clip)
    {
        AudioSourceSounds.PlayOneShot(clip);
    }

    public void PlayRandomSoundFromCategory(SoundCategory soundCategory)
    {
        switch (soundCategory)
        {
            case SoundCategory.Bounce:
                AudioClip randomBoucingClip = BounceSounds[Random.Range(0, BounceSounds.Count)];
                PlaySound(randomBoucingClip);
                break;

            case SoundCategory.Lose:
                AudioClip randomLosingClip = LoseSounds[Random.Range(0, LoseSounds.Count)];
                PlaySound(randomLosingClip);
                break;

            case SoundCategory.Click:
                AudioClip randomClickingClip = ClickSounds[Random.Range(0, ClickSounds.Count)];
                PlaySound(randomClickingClip);
                break;
        }
    }




    public enum SoundCategory
    {
        Bounce,
        Lose,
        Click
    }

}


public class AudioLibrary
{

}
>>>>>>> Stashed changes
