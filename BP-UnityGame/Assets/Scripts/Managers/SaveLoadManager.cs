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
    public Progress Progress;

    public delegate void SettingsLoadedHandler(MainMenuSettings settings);
    public event SettingsLoadedHandler OnSettingsLoaded;

    public delegate void ProgressLoadedHandler(Progress settings);
    public event ProgressLoadedHandler OnProgressLoaded;

    private const string _settingsFileName = "settings.json";
    private const string _progressFileName = "progress.json";



    public enum SaveType
    {
        Settings,
        LobbyBinding,
        GameBinding,
        Progress
    }

    public enum GameState
    {
        Beggining, // Hráè se poprvé objeví v lobby pøed menzou
    }


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Instance.Settings = null;
            Load(SaveType.Settings);
            Load(SaveType.Progress);
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
                case SaveType.Progress:
                    Progress = JsonUtility.FromJson<Progress>(json);
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
                    break;
                case SaveType.Progress:
                    Progress = new Progress
                    {
                        SpawnScene = SceneLoaderManager.ActiveScene.None,
                        GameState = GameState.Beggining
                    };
                    break;
            }
            Save(saveType);
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
            case SaveType.Progress:
                json = JsonUtility.ToJson(Progress, true);
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
            case SaveType.Progress:
                return Path.Combine(Application.persistentDataPath, _progressFileName);
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
            case SaveType.Progress:
                OnProgressLoaded?.Invoke(Progress);
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

public class Progress
{
    public SceneLoaderManager.ActiveScene SpawnScene;
    public SaveLoadManager.GameState GameState;

}
