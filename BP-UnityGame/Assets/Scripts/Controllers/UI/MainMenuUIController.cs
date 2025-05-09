using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static SceneLoaderManager;

public class MainMenuUIController : MonoBehaviour
{
    public Button LoadGameButton;
    public TextMeshProUGUI VersionText;

    void Start()
    {
        VersionText.text = "Verze: " + Application.version;
        if (SaveLoadManager.Instance.Progress.SpawnScene == ActiveScene.None)
        {
            LoadGameButton.interactable = false;
        }
    }

    public void OnNewGameButtonPressed()
    {
        SaveLoadManager.Instance.ResetToDefaults(SaveLoadManager.SaveType.Progress);
        SaveLoadManager.Instance.Save(SaveLoadManager.SaveType.Progress);

        AudioManager.Instance.PlayClipByName("UI_Button_Click_1", AudioManager.Instance.AudioLibrary.UI, AudioManager.Instance.SFXAudioSource);
        SceneLoaderManager.Instance.LoadScene(ActiveScene.Intro);
        //SceneLoaderManager.Instance.LoadScene(ActiveScene.LobbyMenza);//skip intra
    }


    public void OnLoadGameButtonPressed()
    {
        SceneLoaderManager.Instance.LoadScene(SaveLoadManager.Instance.Progress.SpawnScene);

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
