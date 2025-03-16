using TMPro;
using UnityEngine;

public class FPSDisplayController : MonoBehaviour
{
    public static FPSDisplayController Instance;

    public TextMeshProUGUI FpsText;
    private float deltaTime = 0.0f;
    private float updateInterval = 0.1f;
    private float nextUpdate = 0.0f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        SaveLoadManager.Instance.OnSettingsLoaded += UpdateFPSText;

    }

    public void UpdateFPSText(MainMenuSettings settings)
    {
        ToggleShow(settings.IsShowingFPS);
    }

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        if (Time.unscaledTime > nextUpdate)
        {
            nextUpdate = Time.unscaledTime + updateInterval;
            float fps = 1.0f / deltaTime;
            FpsText.text = $"FPS: {Mathf.Ceil(fps)}";
        }
    }

    public void ToggleShow(bool show)
    {
        FpsText.gameObject.SetActive(show);
    }

}
