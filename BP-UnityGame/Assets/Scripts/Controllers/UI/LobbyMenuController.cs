using UnityEngine;

public class LobbyMenuController : MonoBehaviour
{
    public GameObject MenuPanel;
    void Start()
    {

    }

    public void ToggleMenu()
    {
        AudioManager.Instance.PlayClipByName("UI_Button_Click_1", AudioManager.Instance.AudioLibrary.UI, AudioManager.Instance.SFXAudioSource);
        if (MenuPanel.gameObject.activeSelf)
        {
            MenuPanel.SetActive(false);
        }
        else
        {
            MenuPanel.SetActive(true);
        }
    }


    public void BackToMenu()
    {
        AudioManager.Instance.PlayClipByName("UI_Button_Click_1", AudioManager.Instance.AudioLibrary.UI, AudioManager.Instance.SFXAudioSource);
        SceneLoaderManager.Instance.LoadScene(SceneLoaderManager.ActiveScene.MainMenu);
    }
}
