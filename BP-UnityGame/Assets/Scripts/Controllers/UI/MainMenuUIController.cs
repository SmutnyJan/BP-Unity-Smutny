using System.Collections;
using UnityEngine;
using static SceneLoaderManager;

public class MainMenuUIController : MonoBehaviour
{
    void Start()
    {
    }

    public void OnNewGameButtonPressed()
    {
        SceneLoaderManager.Instance.LoadScene(ActiveScene.Intro);
    }


    public void OnLoadGameButtonPressed()
    {

    }

    public void OnSettingsButtonPressed()
    {

    }

    public void OnQuitGameButtonPressed()
    {
        #if UNITY_STANDALONE
                Application.Quit();
        #endif
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
