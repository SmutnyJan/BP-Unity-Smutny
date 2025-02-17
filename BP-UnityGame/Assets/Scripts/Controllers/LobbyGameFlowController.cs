using UnityEngine;

public class LobbyGameFlowController : MonoBehaviour
{
    public ArrowNavigationController ArrowNavigationController;
    public PathFinder PathFinder; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SceneLoaderManager.ActiveScene CurrentScene = SceneLoaderManager.Instance.CurrentScene;
        switch (SaveLoadManager.Instance.Progress.GameState)
        {
            case SaveLoadManager.GameState.Beggining:
                if (CurrentScene != SceneLoaderManager.ActiveScene.LobbyG)
                {
                    //PathFinder.BuildingEnter.SetActive(false);
                }
                TipsController.Instance.ShowMessages(new string[] { "Poøádnì ses prospal, ale teï je naèase odhodit splín stranou a pustit se do práce. Nejdøíve jdi na budovu G. Èervená šipka ti ukáže cestu.",
                "Ta velká budova pøed tebou je menza. To jen tak pro jistotu, jestli jsi to náhodou nezapomnìl. Až se vrátíš z budovy G, tak se tam zastavíš. Teï mají totiž zavøeno."});

                SaveLoadManager.Instance.Progress.GameState = SaveLoadManager.GameState.RoadToG;
                SaveLoadManager.Instance.Progress.SpawnScene = SceneLoaderManager.ActiveScene.LobbyMenza;
                SaveLoadManager.Instance.Save(SaveLoadManager.SaveType.Progress);
                ArrowNavigationController.StartNavigating(PathFinder.GetTransformOfTarget(SceneLoaderManager.ActiveScene.LobbyG));
                break;

            case SaveLoadManager.GameState.RoadToG:
                //PathFinder.BuildingEnter.SetActive(false);
                ArrowNavigationController.StartNavigating(PathFinder.GetTransformOfTarget(SceneLoaderManager.ActiveScene.LobbyG));
                break;

            case SaveLoadManager.GameState.GFinished:
                TipsController.Instance.ShowMessages(new string[] { "Jdi do Menzy", "Jupí" });


                SaveLoadManager.Instance.Progress.GameState = SaveLoadManager.GameState.RoadToMenza;
                SaveLoadManager.Instance.Progress.SpawnScene = SceneLoaderManager.ActiveScene.LobbyG;
                SaveLoadManager.Instance.Save(SaveLoadManager.SaveType.Progress);
                ArrowNavigationController.StartNavigating(PathFinder.GetTransformOfTarget(SceneLoaderManager.ActiveScene.LobbyMenza));
                break;

            case SaveLoadManager.GameState.RoadToMenza:
                if (CurrentScene == SceneLoaderManager.ActiveScene.Menza)
                {
                    TipsController.Instance.ShowMessages(new string[] { "Tohle je menza", "bla bla bla, kup si nìco" });
                    SaveLoadManager.Instance.Progress.GameState = SaveLoadManager.GameState.RoadToC;
                    SaveLoadManager.Instance.Progress.SpawnScene = SceneLoaderManager.ActiveScene.Menza;
                    SaveLoadManager.Instance.Save(SaveLoadManager.SaveType.Progress);
                    ArrowNavigationController.StartNavigating(PathFinder.GetTransformOfTarget(SceneLoaderManager.ActiveScene.LobbyC));

                }
                ArrowNavigationController.StartNavigating(PathFinder.GetTransformOfTarget(SceneLoaderManager.ActiveScene.LobbyMenza));
                break;

            case SaveLoadManager.GameState.RoadToC:
                ArrowNavigationController.StartNavigating(PathFinder.GetTransformOfTarget(SceneLoaderManager.ActiveScene.LobbyC));
                break;

            case SaveLoadManager.GameState.CFinished:
                TipsController.Instance.ShowMessages(new string[] { "Zvládl jsi to!", "Jupí, paráda" });

                SaveLoadManager.Instance.Progress.GameState = SaveLoadManager.GameState.RoadToA;
                SaveLoadManager.Instance.Progress.SpawnScene = SceneLoaderManager.ActiveScene.LobbyC;
                SaveLoadManager.Instance.Save(SaveLoadManager.SaveType.Progress);
                ArrowNavigationController.StartNavigating(PathFinder.GetTransformOfTarget(SceneLoaderManager.ActiveScene.LobbyAB));
                break;

            case SaveLoadManager.GameState.RoadToA:
                ArrowNavigationController.StartNavigating(PathFinder.GetTransformOfTarget(SceneLoaderManager.ActiveScene.LobbyAB));
                break;


        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

