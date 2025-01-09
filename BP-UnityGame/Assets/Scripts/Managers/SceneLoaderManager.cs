using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderManager : MonoBehaviour
{
    public static SceneLoaderManager Instance;

    public enum Scene
    {
        MainMenu
    }

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

    public void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(nameof(scene), LoadSceneMode.Single);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
