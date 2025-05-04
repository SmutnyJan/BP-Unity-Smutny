using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderManager : MonoBehaviour
{
    //C:\Users\smutn\AppData\LocalLow\FM TUL Smutny Strecanska\TULtimatní hra

    public static SceneLoaderManager Instance;
    public Animator SceneTransitionAnimator;
    public GameObject CrossfadeCanvas;

    [HideInInspector]
    public ActiveScene PreviousScene = ActiveScene.None;
    [HideInInspector]
    public ActiveScene CurrentScene = ActiveScene.None;

    private bool _firstSceneLoaded = false;

    public enum ActiveScene
    {
        None,
        MainMenu,
        Intro,
        LobbyG,
        LobbyMenza,
        LobbyC,
        LobbyAB,
        Settings,
        Test,
        PostTest,
        Menza,
        LevelG,
        LevelC,
        LevelA,
        Ending,
        Titles
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            CurrentScene = ActiveScene.MainMenu;

        }
        else
        {
            Destroy(gameObject);
        }

    }


    void Start()
    {
        SceneManager.sceneLoaded += MusicSwitcher;

    }
    private void MusicSwitcher(Scene arg0, LoadSceneMode arg1)
    {
        if (CurrentScene == ActiveScene.MainMenu && AudioManager.Instance.MusicAudioSource.clip.name != "Resonant Victory - Glbml")
        {
            AudioManager.Instance.PlayClipByName("Resonant Victory - Glbml", AudioManager.Instance.AudioLibrary.Music, AudioManager.Instance.MusicAudioSource, AudioManager.PlayType.Play);
        }
        else if ((CurrentScene == ActiveScene.LobbyMenza || CurrentScene == ActiveScene.LobbyG || CurrentScene == ActiveScene.LobbyC || CurrentScene == ActiveScene.LobbyAB || CurrentScene == ActiveScene.Menza) && AudioManager.Instance.MusicAudioSource.clip.name != "MAXAN - The Lost Time")
        {
            AudioManager.Instance.PlayClipByName("MAXAN - The Lost Time", AudioManager.Instance.AudioLibrary.Music, AudioManager.Instance.MusicAudioSource, AudioManager.PlayType.Play);
        }
        else if (CurrentScene == ActiveScene.LevelG && AudioManager.Instance.MusicAudioSource.clip.name != "Under the Neon Breeze")
        {
            AudioManager.Instance.PlayClipByName("Under the Neon Breeze", AudioManager.Instance.AudioLibrary.Music, AudioManager.Instance.MusicAudioSource, AudioManager.PlayType.Play);
        }
        else if (CurrentScene == ActiveScene.LevelC && AudioManager.Instance.MusicAudioSource.clip.name != "Drifting in Bliss")
        {
            AudioManager.Instance.PlayClipByName("Drifting in Bliss", AudioManager.Instance.AudioLibrary.Music, AudioManager.Instance.MusicAudioSource, AudioManager.PlayType.Play);
        }
        else if (CurrentScene == ActiveScene.LevelA && AudioManager.Instance.MusicAudioSource.clip.name != "Chillpeach - Purple")
        {
            AudioManager.Instance.PlayClipByName("Chillpeach - Purple", AudioManager.Instance.AudioLibrary.Music, AudioManager.Instance.MusicAudioSource, AudioManager.PlayType.Play);
        }
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        if (_firstSceneLoaded)
        {
            SceneTransitionAnimator.SetTrigger("End");
        }
        _firstSceneLoaded = true;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded -= MusicSwitcher;

    }

    public void LoadScene(ActiveScene scene)
    {
        PreviousScene = CurrentScene;
        CurrentScene = scene;
        FullScreenShaderManager.Instance.SwitchToShader(FullScreenShaderManager.FullScreenShader.NoneForced);
        StartCoroutine(LoadWithAnimationCoroutine(scene));
    }

    private IEnumerator LoadWithAnimationCoroutine(ActiveScene scene)
    {
        SceneTransitionAnimator.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(scene.ToString(), LoadSceneMode.Single);
    }
}
