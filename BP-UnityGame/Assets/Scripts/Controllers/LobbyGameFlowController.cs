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
                    "Tak co, vysp�no? Je na�ase nechat no�n� dobrodru�stv� za sebou a pustit se do pr�ce.",
                    "Pokud m� pocit, �e si v�echno nepamatuje�, klid � j� ti to p�ipomenu.",
                    "Tv�j prvn� c�l je budova G. Sleduj �ernou �ipku nad sebou, dovede t� tam.",
                    "Ta velk� budova p�ed tebou je menza. Te� maj� zav�eno, ale pozd�ji se tam stav� po n�vratu z G."
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
                TipsController.Instance.ShowMessages(new string[] { "Dobr� pr�ce, te� u� v�, jak to tu chod�",
                    "Te� vyraz do menzy. U� by m�li m�t otev�eno" });


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
                    TipsController.Instance.ShowMessages(new string[] { "Tohle je menza a kant�na. Na ob�d je sice brzy, ale v kant�n� maj� zaj�mav� v�ci. Ur�it� se ti n�co z toho bude hodit.", "M��e� si sem zaj�t kdykoliv bude� cht�t. V�ci, co tu maj�, ti pom��ou prokousat se a� k akademick�mu �sp�chu." });
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
                TipsController.Instance.ShowMessages(new string[] { "V�born�, dal�� v�zva za tebou.", "Te� n�s �ek� budova A. Zn� to, op�t sta�� j�t podle �ipky." });


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

