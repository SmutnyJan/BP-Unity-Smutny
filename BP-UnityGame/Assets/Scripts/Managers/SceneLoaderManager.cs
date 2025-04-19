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
        Ending
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



    void Start()
    {
        //Application.targetFrameRate = -1;
    }

    void Update()
    {

    }
}
