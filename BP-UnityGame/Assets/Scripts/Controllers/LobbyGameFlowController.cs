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
                TipsController.Instance.ShowMessages(new string[] {
                    "Tak co, vyspáno? Je naèase nechat noèní dobrodružství za sebou a pustit se do práce.",
                    "Pokud máš pocit, že si všechno nepamatuješ, klid — já ti to pøipomenu.",
                    "Tvùj první cíl je budova G. Sleduj èernou šipku nad sebou, dovede tì tam.",
                    "Ta velká budova pøed tebou je menza. Teï mají zavøeno, ale pozdìji se tam stavíš po návratu z G."
                });

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
                if (CurrentScene != SceneLoaderManager.ActiveScene.LobbyC && CurrentScene != SceneLoaderManager.ActiveScene.Menza && CurrentScene != SceneLoaderManager.ActiveScene.LobbyMenza)
                {
                    PathFinder.BuildingEnter.SetActive(false);
                }
                ArrowNavigationController.StartNavigating(PathFinder.GetTransformOfTarget(SceneLoaderManager.ActiveScene.LobbyC));
                break;

            case SaveLoadManager.GameState.CFinished:
                if (CurrentScene != SceneLoaderManager.ActiveScene.LobbyAB && CurrentScene != SceneLoaderManager.ActiveScene.Menza && CurrentScene != SceneLoaderManager.ActiveScene.LobbyMenza)
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
                if (CurrentScene != SceneLoaderManager.ActiveScene.LobbyAB && CurrentScene != SceneLoaderManager.ActiveScene.Menza && CurrentScene != SceneLoaderManager.ActiveScene.LobbyMenza)
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

