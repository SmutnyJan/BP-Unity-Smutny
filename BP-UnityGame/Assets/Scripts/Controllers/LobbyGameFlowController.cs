using UnityEngine;

public class LobbyGameFlowController : MonoBehaviour
{
    public ArrowNavigationController ArrowNavigationController;
    public PathFinder PathFinder;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (SaveLoadManager.Instance.Progress.GameState == SaveLoadManager.GameState.Beggining)
        {          
            TipsController.Instance.ShowMessages(new string[] { "Poøádnì ses prospal, ale teï je naèase odhodit splín stranou a pustit se do práce. Nejdøíve jdi na budovu G. Èervená šipka ti ukáže cestu.",
            "Ta velká budova pøed tebou je menza. To jen tak pro jistotu, jestli jsi to náhodou nezapomnìl. Až se vrátíš z budovy G, tak se tam zastavíš. Teï mají totiž zavøeno."});

            SaveLoadManager.Instance.Progress.GameState = SaveLoadManager.GameState.RoadToG;
            SaveLoadManager.Instance.Save(SaveLoadManager.SaveType.Progress);
            ArrowNavigationController.StartNavigating(PathFinder.GetTransformOfTarget(SceneLoaderManager.ActiveScene.LobbyG));
        }
        else if(SaveLoadManager.Instance.Progress.GameState == SaveLoadManager.GameState.RoadToG)
        {
            ArrowNavigationController.StartNavigating(PathFinder.GetTransformOfTarget(SceneLoaderManager.ActiveScene.LobbyG));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

