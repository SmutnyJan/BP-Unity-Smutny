using UnityEngine;

public class LevelFlowManager : MonoBehaviour
{

    public static LevelFlowManager Instance;
    public SaveLoadManager.GameState SetGameStateAfterFinish;
    public SceneLoaderManager.ActiveScene LoadSceneAfterFinish;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }


    void Start()
    {
        SaveLoadManager.Instance.Progress.SpawnScene = SceneLoaderManager.Instance.CurrentScene;
        SaveLoadManager.Instance.Save(SaveLoadManager.SaveType.Progress);
    }

    void Update()
    {
        
    }

    public void EndLevel()
    {
        SaveLoadManager.Instance.Progress.GameState = SetGameStateAfterFinish;
        SaveLoadManager.Instance.Save(SaveLoadManager.SaveType.Progress);

        SceneLoaderManager.Instance.LoadScene(LoadSceneAfterFinish);

    }
}
