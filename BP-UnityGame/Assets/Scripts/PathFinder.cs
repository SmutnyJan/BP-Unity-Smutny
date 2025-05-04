using UnityEngine;
using static SceneLoaderManager;

public class PathFinder : MonoBehaviour
{
    public GameObject LeftExit;
    public GameObject RightExit;
    public GameObject BuildingEnter;


    void Start()
    {

    }

    public Transform GetTransformOfTarget(ActiveScene DestinationScene)
    {
        if (SceneLoaderManager.Instance.CurrentScene == DestinationScene)
        {
            return BuildingEnter.gameObject.transform;
        }
        else if (SceneLoaderManager.Instance.CurrentScene > DestinationScene)
        {
            return LeftExit.gameObject.transform;
        }
        return RightExit.gameObject.transform;
    }
}
