using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static SceneLoaderManager;


public class SaveLoadManager : MonoBehaviour
{
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
        RoadToG, //Hráø si pøeèetl titulky a má se dostat na G,
        GFinished, //Hráø splnil level G
        RoadToMenza, //Hráè je na cestì do Menzy
        RoadToC, //Hráè došel do Menzy, mùže si nakoupit a potom má jít na C
        CFinished, //Hráè dokonèil level C a šipka ho vede na finální level na budovì A
        RoadToA //Hráè je na cestì na A
    }


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Instance.Settings = null;
            OnSettingsLoaded += OnSettingsLoadedLocal;

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
        AudioManager.Instance.UpdateAudioSettings(Settings);
        FPSDisplayController.Instance.UpdateFPSText(Settings);

        string cpu = SystemInfo.processorType;
        int cpuCores = SystemInfo.processorCount;
        string gpu = SystemInfo.graphicsDeviceName;
        int gpuMemory = SystemInfo.graphicsMemorySize;
        int ram = SystemInfo.systemMemorySize;

        string version = Application.version;
        string currentDate = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        Debug.Log($"App Version: {version}");
        Debug.Log($"Current Date: {currentDate}");

        Debug.Log($"CPU: {cpu} ({cpuCores} cores)");
        Debug.Log($"GPU: {gpu} ({gpuMemory} MB VRAM)");
        Debug.Log($"RAM: {ram} MB");

        Debug.Log(JsonUtility.ToJson(Settings, true));
        Debug.Log(JsonUtility.ToJson(Progress, true));
    }

    public void Load(SaveType saveType)
    {
        string filePath = GetSettingsFilePath(saveType);

        if (File.Exists(filePath))
        {
            switch (saveType)
            {
                case SaveType.Settings:
                    Settings = JsonUtility.FromJson<MainMenuSettings>(File.ReadAllText(filePath));
                    InvokeLoadEvent(saveType);
                    break;
                case SaveType.Progress:
                    Progress = JsonUtility.FromJson<Progress>(File.ReadAllText(filePath));
                    InvokeLoadEvent(saveType);
                    break;
            }
        }
        else
        {
            ResetToDefaults(saveType);
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

    public void ResetToDefaults(SaveType saveType)
    {
        switch (saveType)
        {
            case SaveType.Settings:
                Settings = new MainMenuSettings
                {
                    SFXVolume = AudioManager.Instance.SFXVolume,
                    MusicVolume = AudioManager.Instance.MusicVolume,
                    IsFullScreen = Screen.fullScreen,
                    IsShowingFPS = false,
                    VSync = false,
                    Microphone = ""
                };
                break;
            case SaveType.Progress:
                Progress = new Progress
                {
                    SpawnScene = SceneLoaderManager.ActiveScene.None,
                    GameState = GameState.Beggining,
                    Money = UnityEngine.Random.Range(0, 100),
                    Items = new List<ItemAmount>()
                    {
                        new() { ItemType = ItemType.Pencil, Amount = 5},
                        new() { ItemType = ItemType.Pearl, Amount = 1},
                        new() { ItemType = ItemType.Hourglass, Amount = 1},
                        new() { ItemType = ItemType.Boots, Amount = 2},
                        new() { ItemType = ItemType.JumpCoil, Amount = 2},
                        new() { ItemType = ItemType.Binoculars, Amount = 2},

                    },
                    LevelConfig = new LevelProgress()
                };
                break;
        }
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
        QualitySettings.vSyncCount = settings.VSync ? 1 : 0;

        if (settings.IsFullScreen)
        {
            int width = Display.main.systemWidth;
            int height = Display.main.systemHeight;

            Screen.SetResolution(width, height, FullScreenMode.FullScreenWindow);
        }
        else
        {
            Screen.SetResolution(1280, 720, FullScreenMode.Windowed);
        }
    }
}



public class MainMenuSettings
{
    public float SFXVolume;
    public float MusicVolume;
    public bool IsFullScreen;
    public bool IsShowingFPS;
    public bool VSync;
    public string Microphone;
}

public class Progress
{
    public SceneLoaderManager.ActiveScene SpawnScene;
    public SaveLoadManager.GameState GameState;
    public int Money;
    public List<ItemAmount> Items;
    public LevelProgress LevelConfig;
}


[System.Serializable]
public class ItemAmount
{
    public ItemType ItemType;
    public int Amount;
}

[System.Serializable]
public class LevelProgress
{
    public ActiveScene Scene;
    public Vector3 SpawnPoint;
    public List<int> ChestsOpenedIndexes;
    public List<int> PlanesDestroyedIndexes;
    public List<ItemAmount> ItemsRevert;
    public int MoneyRevert;

}
