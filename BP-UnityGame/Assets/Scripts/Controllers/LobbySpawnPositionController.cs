using UnityEngine;
using static SceneLoaderManager;

public class LobbySpawnPositionController : MonoBehaviour
{

    public SceneLoaderManager.ActiveScene RightScene;
    public SceneLoaderManager.ActiveScene LeftScene;
    public Transform RightSpawnPosition;
    public Transform LeftSpawnPosition;
    public GameObject Player;
    public LobbyExitColliderController LeftColliderController;
    public LobbyExitColliderController RightColliderController;

    public static LobbySpawnPositionController Instance;


    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (LeftScene != ActiveScene.None)
        {
            LeftColliderController.LoadToScene = LeftScene;
        }
        if (RightScene != ActiveScene.None)
        {
            RightColliderController.LoadToScene = RightScene;
        }

        if (SceneLoaderManager.Instance.PreviousScene == LeftScene)
        {
            Player.transform.position = LeftSpawnPosition.position;
            return;
        }
        else if (SceneLoaderManager.Instance.PreviousScene == RightScene)
        {
            Player.transform.position = RightSpawnPosition.position;
            return;
        }



    }
}
