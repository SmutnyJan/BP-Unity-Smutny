using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static AudioManager;


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
        OnSettingsLoaded += OnSettingsLoadedLocal;
        Load(SaveType.Settings);
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
                    InvokeLoadEvent(saveType);
                    break;
            }
        }
        else
        {
            switch (saveType)
            {
                case SaveType.Settings:            
                    Settings = new MainMenuSettings
                    {
                        SFXVolume = AudioManager.Instance.SFXVolume,
                        MusicVolume = AudioManager.Instance.MusicVolume,
                        IsFullScreen = Screen.fullScreen
                    };
                    Save(SaveType.Settings);
                    break;
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

    public void InvokeLoadEvent(SaveType saveType)
    {
        switch (saveType)
        {
            case SaveType.Settings:
                OnSettingsLoaded?.Invoke(Settings);
                break;
        }
    }

    private void OnSettingsLoadedLocal(MainMenuSettings settings)
    {
        Screen.fullScreen = settings.IsFullScreen;
    }

}



public class MainMenuSettings
{
    public float SFXVolume;
    public float MusicVolume;
    public bool IsFullScreen;
}
