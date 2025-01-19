using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource DefaultSFXAudioSource;
    public AudioSource DefaultMusicAudioSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DefaultSFXAudioSource = GetComponents<AudioSource>()[0];
            DefaultMusicAudioSource = GetComponents<AudioSource>()[1];
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

    void Update()
    {
        
    }
}
