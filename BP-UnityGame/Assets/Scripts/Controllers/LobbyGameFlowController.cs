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
                    PathFinder.BuildingEnter.SetActive(false);
                }
                TipsController.Instance.ShowMessages(new string[] { "Poøádnì ses prospal, ale teï je naèase odhodit splín stranou a pustit se do práce.",
                    "Možná budeš mít trochu výpadky pamìti, ale to je normální. Já ti všechno pøipomenu.",
                    "Nejdøíve jdi na budovu G. Èerná šipka ti ukáže cestu.",
                "Ta velká budova pøed tebou je menza. Až se vrátíš z budovy G, tak se tam zastavíš. Teï mají totiž zavøeno."});

                SaveLoadManager.Instance.Progress.GameState = SaveLoadManager.GameState.RoadToG;
                SaveLoadManager.Instance.Progress.SpawnScene = SceneLoaderManager.ActiveScene.LobbyMenza;
                SaveLoadManager.Instance.Save(SaveLoadManager.SaveType.Progress);
                ArrowNavigationController.StartNavigating(PathFinder.GetTransformOfTarget(SceneLoaderManager.ActiveScene.LobbyG));
                break;

            case SaveLoadManager.GameState.RoadToG:
                if (CurrentScene != SceneLoaderManager.ActiveScene.LobbyG)
                {
                    PathFinder.BuildingEnter.SetActive(false);
                }
                ArrowNavigationController.StartNavigating(PathFinder.GetTransformOfTarget(SceneLoaderManager.ActiveScene.LobbyG));
                break;

            case SaveLoadManager.GameState.GFinished:
                if (CurrentScene != SceneLoaderManager.ActiveScene.LobbyMenza)
                {
                    PathFinder.BuildingEnter.SetActive(false);
                }
                TipsController.Instance.ShowMessages(new string[] { "Dobrá práce, teï už víš, jak to tu chodí",
                    "Teï vyraz do menzy. Už by mìli mít otevøeno" });


                SaveLoadManager.Instance.Progress.GameState = SaveLoadManager.GameState.RoadToMenza;
                SaveLoadManager.Instance.Progress.SpawnScene = SceneLoaderManager.ActiveScene.LobbyG;
                SaveLoadManager.Instance.Save(SaveLoadManager.SaveType.Progress);
                ArrowNavigationController.StartNavigating(PathFinder.GetTransformOfTarget(SceneLoaderManager.ActiveScene.LobbyMenza));
                break;

            case SaveLoadManager.GameState.RoadToMenza:
                if (CurrentScene != SceneLoaderManager.ActiveScene.LobbyMenza)
                {
                    PathFinder.BuildingEnter.SetActive(false);
                }
                if (CurrentScene == SceneLoaderManager.ActiveScene.Menza)
                {
                    TipsController.Instance.ShowMessages(new string[] { "Tohle je menza a kantýna. Na obìd je sice brzy, ale v kantýnì mají zajímavé vìci. Urèitì se ti nìco z toho bude hodit.", "Mùžeš si sem zajít kdykoliv budeš chtít. Vìci, co tu mají, ti pomùžou prokousat se až k akademickému úspìchu." });
                    SaveLoadManager.Instance.Progress.GameState = SaveLoadManager.GameState.RoadToC;
                    SaveLoadManager.Instance.Progress.SpawnScene = SceneLoaderManager.ActiveScene.Menza;
                    SaveLoadManager.Instance.Save(SaveLoadManager.SaveType.Progress);
                    ArrowNavigationController.StartNavigating(PathFinder.GetTransformOfTarget(SceneLoaderManager.ActiveScene.LobbyC));
                    PathFinder.BuildingEnter.SetActive(true);


                }
                ArrowNavigationController.StartNavigating(PathFinder.GetTransformOfTarget(SceneLoaderManager.ActiveScene.LobbyMenza));
                break;

            case SaveLoadManager.GameState.RoadToC:
                if (CurrentScene != SceneLoaderManager.ActiveScene.LobbyC)
                {
                    PathFinder.BuildingEnter.SetActive(false);
                }
                ArrowNavigationController.StartNavigating(PathFinder.GetTransformOfTarget(SceneLoaderManager.ActiveScene.LobbyC));
                break;

            case SaveLoadManager.GameState.CFinished:
                if (CurrentScene != SceneLoaderManager.ActiveScene.LobbyAB)
                {
                    PathFinder.BuildingEnter.SetActive(false);
                }
                TipsController.Instance.ShowMessages(new string[] { "Výbornì, další výzva za tebou.", "Teï nás èeká budova A. Znáš to, opìt staèí jít podle šipky." });


                SaveLoadManager.Instance.Progress.GameState = SaveLoadManager.GameState.RoadToA;
                SaveLoadManager.Instance.Progress.SpawnScene = SceneLoaderManager.ActiveScene.LobbyC;
                SaveLoadManager.Instance.Save(SaveLoadManager.SaveType.Progress);
                ArrowNavigationController.StartNavigating(PathFinder.GetTransformOfTarget(SceneLoaderManager.ActiveScene.LobbyAB));
                break;

            case SaveLoadManager.GameState.RoadToA:
                if (CurrentScene != SceneLoaderManager.ActiveScene.LobbyAB)
                {
                    PathFinder.BuildingEnter.SetActive(false);
                }
                ArrowNavigationController.StartNavigating(PathFinder.GetTransformOfTarget(SceneLoaderManager.ActiveScene.LobbyAB));
                break;


        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

