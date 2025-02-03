using UnityEngine;
using static SceneLoaderManager;

public class PathFinder : MonoBehaviour
{
    public Transform LeftExit;
    public Transform RightExit;
    public Transform BuildingEnter;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
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
