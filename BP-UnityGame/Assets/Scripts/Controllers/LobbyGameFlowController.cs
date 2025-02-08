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
                TipsController.Instance.ShowMessages(new string[] { "Po��dn� ses prospal, ale te� je na�ase odhodit spl�n stranou a pustit se do pr�ce. Nejd��ve jdi na budovu G. �erven� �ipka ti uk�e cestu.",
                "Ta velk� budova p�ed tebou je menza. To jen tak pro jistotu, jestli jsi to n�hodou nezapomn�l. A� se vr�t� z budovy G, tak se tam zastav�. Te� maj� toti� zav�eno."});

                SaveLoadManager.Instance.Progress.GameState = SaveLoadManager.GameState.RoadToG;
                SaveLoadManager.Instance.Progress.SpawnScene = SceneLoaderManager.ActiveScene.LobbyMenza;
                SaveLoadManager.Instance.Save(SaveLoadManager.SaveType.Progress);
                ArrowNavigationController.StartNavigating(PathFinder.GetTransformOfTarget(SceneLoaderManager.ActiveScene.LobbyG));
                break;

            case SaveLoadManager.GameState.RoadToG:
                //PathFinder.BuildingEnter.SetActive(false);
                ArrowNavigationController.StartNavigating(PathFinder.GetTransformOfTarget(SceneLoaderManager.ActiveScene.LobbyG));
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

