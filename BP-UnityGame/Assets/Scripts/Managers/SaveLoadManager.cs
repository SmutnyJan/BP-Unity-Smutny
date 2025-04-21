using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using static SceneLoaderManager;


public class SaveLoadManager : MonoBehaviour
{
    // C:\Users\smutn\AppData\LocalLow\FM TUL Smutny Strecanska\TULtimátní hra
    public static SaveLoadManager Instance;
    public MainMenuSettings Settings;
    public Progress Progress;
    public TextMeshProUGUI SavingText;
    public TextMeshProUGUI LoadingText;

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

    }

    void Update()
    {

    }

    public void Load(SaveType saveType)
    {
        //LoadingText.gameObject.SetActive(true);
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

        //StartCoroutine(DisableTextAfter(LoadingText));
    }


    public void Save(SaveType saveType)
    {
        //SavingText.gameObject.SetActive(true);

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
        //StartCoroutine(DisableTextAfter(SavingText));


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
        Screen.fullScreen = settings.IsFullScreen;
        QualitySettings.vSyncCount = settings.VSync ? 1 : 0;

    }

    private IEnumerator DisableTextAfter(TextMeshProUGUI text)
    {
        yield return new WaitForSeconds(2.5f);
        text.gameObject.SetActive(false);

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
