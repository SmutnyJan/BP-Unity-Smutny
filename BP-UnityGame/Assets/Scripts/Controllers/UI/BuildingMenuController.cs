using System.Linq;
using UnityEngine;

public class BuildingMenuController : MonoBehaviour
{
    public GameObject MenuPanel;
    public LevelFlowManager LevelFlowManager;

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

    public void BackToLobby()
    {
        AudioManager.Instance.PlayClipByName("UI_Button_Click_1", AudioManager.Instance.AudioLibrary.UI, AudioManager.Instance.SFXAudioSource);
        SaveLoadManager.Instance.Progress.Items = SaveLoadManager.Instance.Progress.LevelConfig.ItemsRevert.Select(item => new ItemAmount
        {
            ItemType = item.ItemType,
            Amount = item.Amount,
        }).ToList();
        SaveLoadManager.Instance.Progress.Money = SaveLoadManager.Instance.Progress.LevelConfig.MoneyRevert;

        SaveLoadManager.Instance.Progress.LevelConfig.Scene = SceneLoaderManager.ActiveScene.None;
        SaveLoadManager.Instance.Save(SaveLoadManager.SaveType.Progress);

        if (SceneLoaderManager.Instance.CurrentScene == SceneLoaderManager.ActiveScene.LevelA)
        {
            SceneLoaderManager.Instance.LoadScene(SceneLoaderManager.ActiveScene.LobbyAB);
            return;
        }
        SceneLoaderManager.Instance.LoadScene(LevelFlowManager.LoadSceneAfterFinish);
    }

    public void BackToMenu()
    {
        AudioManager.Instance.PlayClipByName("UI_Button_Click_1", AudioManager.Instance.AudioLibrary.UI, AudioManager.Instance.SFXAudioSource);
        SaveLoadManager.Instance.Load(SaveLoadManager.SaveType.Progress);
        SceneLoaderManager.Instance.LoadScene(SceneLoaderManager.ActiveScene.MainMenu);
    }

    public void ResetLevel()
    {
        AudioManager.Instance.PlayClipByName("UI_Button_Click_1", AudioManager.Instance.AudioLibrary.UI, AudioManager.Instance.SFXAudioSource);
        SaveLoadManager.Instance.Progress.LevelConfig.Scene = SceneLoaderManager.ActiveScene.None;
        SaveLoadManager.Instance.Progress.Items = SaveLoadManager.Instance.Progress.LevelConfig.ItemsRevert.Select(item => new ItemAmount
        {
            ItemType = item.ItemType,
            Amount = item.Amount,
        }).ToList();
        SaveLoadManager.Instance.Progress.Money = SaveLoadManager.Instance.Progress.LevelConfig.MoneyRevert;
        SaveLoadManager.Instance.Save(SaveLoadManager.SaveType.Progress);

        SceneLoaderManager.Instance.LoadScene(SceneLoaderManager.Instance.CurrentScene);
    }
}
