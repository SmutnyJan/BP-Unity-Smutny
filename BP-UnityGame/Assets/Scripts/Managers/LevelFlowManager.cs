using UnityEngine;

public class LevelFlowManager : MonoBehaviour
{

    public static LevelFlowManager Instance;
    public SaveLoadManager.GameState SetGameStateAfterFinish;
    public SceneLoaderManager.ActiveScene LoadSceneAfterFinish;

    public GameObject Player;
    public Transform StartSpawningPoint;
    public float YDespawn;
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

        if (SaveLoadManager.Instance.Progress.LevelConfig.Scene == SceneLoaderManager.Instance.CurrentScene)
        {
            Player.transform.position = SaveLoadManager.Instance.Progress.LevelConfig.SpawnPoint;
            foreach(int chestIndex in SaveLoadManager.Instance.Progress.LevelConfig.ChestsOpenedIndexes)
            {
                GameObject.Find("Chest (" +  chestIndex + ")").GetComponent<OpenChestController>().SetStateToOpen();
            }
        }
        else
        {
            SaveLoadManager.Instance.Progress.LevelConfig = new LevelProgress()
            {
                Scene = SceneLoaderManager.Instance.CurrentScene,
                SpawnPoint = StartSpawningPoint.position,
                ChestsOpenedIndexes = new System.Collections.Generic.List<int>(),
            };
        }

        SaveLoadManager.Instance.Save(SaveLoadManager.SaveType.Progress);

        Player.transform.position = SaveLoadManager.Instance.Progress.LevelConfig.SpawnPoint;


    }

    void Update()
    {
        if (Player.transform.position.y < YDespawn)
        {
            Player.transform.position = SaveLoadManager.Instance.Progress.LevelConfig.SpawnPoint + new Vector3(0, 5, 0);
        }
    }

    public void EndLevel()
    {
        SaveLoadManager.Instance.Progress.GameState = SetGameStateAfterFinish;
        SaveLoadManager.Instance.Save(SaveLoadManager.SaveType.Progress);

        SceneLoaderManager.Instance.LoadScene(LoadSceneAfterFinish);

    }
}
