using UnityEngine;

public class LobbyMenuController : MonoBehaviour
{
    public Canvas MenuPanelCanvas;
    void Start()
    {

    }

    public void ToggleMenu()
    {
        AudioManager.Instance.PlayClipByName("UI_Button_Click_1", AudioManager.Instance.AudioLibrary.UI, AudioManager.Instance.SFXAudioSource);
        MenuPanelCanvas.enabled = !MenuPanelCanvas.enabled;
    }


    public void BackToMenu()
    {
        AudioManager.Instance.PlayClipByName("UI_Button_Click_1", AudioManager.Instance.AudioLibrary.UI, AudioManager.Instance.SFXAudioSource);
        SceneLoaderManager.Instance.LoadScene(SceneLoaderManager.ActiveScene.MainMenu);
    }
}
