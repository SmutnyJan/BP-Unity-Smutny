using System.IO;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    // C:\Users\smutn\AppData\LocalLow\DefaultCompany\BP-UnityGame
    public static SaveLoadManager Instance;
    public MainMenuSettings Settings;

    public delegate void SettingsLoadedHandler(MainMenuSettings settings);
    public event SettingsLoadedHandler OnSettingsLoaded;


    private const string _settingsFileName = "settings.json";


    public enum SaveType
    {
        Settings,
        LobbyBinding,
        GameBinding,
        Game_Progress
    }


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Instance.Settings = null;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        Load(SaveType.Settings);
        Save(SaveType.Settings);

    }

    void Update()
    {
        
    }

    public void Load(SaveType saveType)
    {
        string filePath = GetSettingsFilePath(saveType);

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            switch (saveType)
            {
                case SaveType.Settings:
                    Settings = JsonUtility.FromJson<MainMenuSettings>(json);
                    OnSettingsLoaded?.Invoke(Settings);
                    return;
            }

        }
        else
        {

            switch (saveType)
            {
                case SaveType.Settings:
                    float sfxVolume = 0;
                    AudioManager.Instance.AudioMixer.GetFloat("SFXVolume", out sfxVolume);
            
                    float musicVolume = 0;
                    AudioManager.Instance.AudioMixer.GetFloat("MusicVolume", out musicVolume);
            
                    Settings = new MainMenuSettings
                    {
                        SFXVolume = sfxVolume,
                        MusicVolume = musicVolume
                    };
                    return;
            }

        }


    }


    public void Save(SaveType saveType)
    {
        string json = "";
        switch (saveType)
        {
            case SaveType.Settings:
                json = JsonUtility.ToJson(Settings, true);
                break;
        }

        File.WriteAllText(GetSettingsFilePath(saveType), json);
    }

    private string GetSettingsFilePath(SaveType saveType)
    {
        switch (saveType)
        {
            case SaveType.Settings:
                return Path.Combine(Application.persistentDataPath, _settingsFileName);
            default:
                return "";
        }
    }

}

public class MainMenuSettings
{
    public float SFXVolume;
    public float MusicVolume;
}
