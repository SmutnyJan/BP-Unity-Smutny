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
        AudioManager.Instance.PlayClipByName("UI_Button_Click_1", AudioManager.Instance.AudioLibrary.UI, AudioManager.Instance.SFXAudioSource);
        SceneLoaderManager.Instance.LoadScene(ActiveScene.Intro);
    }


    public void OnLoadGameButtonPressed()
    {
        AudioManager.Instance.PlayClipByName("UI_Button_Click_1", AudioManager.Instance.AudioLibrary.UI, AudioManager.Instance.SFXAudioSource);
    }

    public void OnSettingsButtonPressed()
    {
        AudioManager.Instance.PlayClipByName("UI_Button_Click_1", AudioManager.Instance.AudioLibrary.UI, AudioManager.Instance.SFXAudioSource);
        SceneLoaderManager.Instance.LoadScene(ActiveScene.Settings);
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
