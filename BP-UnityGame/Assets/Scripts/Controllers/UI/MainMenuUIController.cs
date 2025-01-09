using UnityEngine;

public class MainMenuUIController : MonoBehaviour
{
    void Start()
    {
    }

    void OnNewGameButtonPressed()
    {

    }

    void OnLoadGameButtonPressed()
    {

    }

    void OnSettingsButtonPressed()
    {

    }

    void OnQuitGameButtonPressed()
    {
        #if UNITY_STANDALONE
                Application.Quit();
        #endif
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
