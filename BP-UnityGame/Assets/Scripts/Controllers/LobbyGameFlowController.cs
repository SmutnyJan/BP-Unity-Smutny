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
                TipsController.Instance.ShowMessages(new string[] {
                    "Dobr� pr�ce. Prvn� zkou�ku m� za sebou.",
                    "Za jej� dokon�en� jsem ti v�noval tu�nou odm�nu.",
                    "Te� zami� do menzy � pr�v� otev�eli. T�eba tam najde� v�c ne� jen j�dlo."
                });


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
                    TipsController.Instance.ShowMessages(new string[] {
                        "Tady je menza a kant�na. Na ob�d je mo�n� brzo, ale v nab�dce najde� p�r opravdu u�ite�n�ch v�c�.",
                        "M��e� se sem vracet kdykoliv. To, co si tu po��d�, ti m��e pomoct prokousat se a� ke st�tnic�m."
                    });
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
                TipsController.Instance.ShowMessages(new string[] {
                    "V�born�. Dal�� v�zvu m� za sebou.",
                    "Te� m���me k budov� A. Zase sta�� sledovat �ipku � v�, jak na to."
                });

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

