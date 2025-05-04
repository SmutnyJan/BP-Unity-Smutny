using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FPSDisplayController : MonoBehaviour
{
    public static FPSDisplayController Instance;
    public TextMeshProUGUI FpsText;
    public bool IsFpsOn;

    private float deltaTime = 0.0f;
    private float updateInterval = 0.1f;
    private float nextUpdate = 0.0f;
    private Canvas _fpsCanvas;

    private string _currentScene;
    private Dictionary<string, List<float>> _fpsData = new();
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            _fpsCanvas = GetComponent<Canvas>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        _currentScene = SceneManager.GetActiveScene().name;
        _fpsData[_currentScene] = new List<float>();

        SaveLoadManager.Instance.OnSettingsLoaded += UpdateFPSText;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _currentScene = scene.name;
        if (!_fpsData.ContainsKey(_currentScene))
        {
            _fpsData[_currentScene] = new List<float>();
        }
    }

    public void UpdateFPSText(MainMenuSettings settings)
    {
        ToggleShow(settings.IsShowingFPS);
    }

    void Update()
    {
        if (!IsFpsOn) return;

        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        if (Time.unscaledTime > nextUpdate)
        {
            nextUpdate = Time.unscaledTime + updateInterval;
            float fps = 1.0f / deltaTime;
            FpsText.text = $"FPS: {Mathf.Ceil(fps)}";
            _fpsData[_currentScene].Add(fps);
        }
    }

    void OnApplicationQuit()
    {
        foreach (KeyValuePair<string, List<float>> kvp in _fpsData)
        {
            string sceneName = kvp.Key;
            foreach (float fps in kvp.Value)
            {
                Debug.Log($"{sceneName} FPS: {fps}");
            }
        }
    }

    public void ToggleShow(bool show)
    {
        _fpsCanvas.enabled = show;
        IsFpsOn = show;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SaveLoadManager.Instance.OnSettingsLoaded -= UpdateFPSText;
    }
}
