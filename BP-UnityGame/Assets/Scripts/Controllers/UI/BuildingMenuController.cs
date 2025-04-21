using System.Linq;
using UnityEngine;

public class BuildingMenuController : MonoBehaviour
{
    public GameObject MenuPanel;
    public LevelFlowManager LevelFlowManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToggleMenu()
    {
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
        if (SceneLoaderManager.Instance.CurrentScene == SceneLoaderManager.ActiveScene.LevelA)
        {
            Debug.LogError("pozor, pøedìlat, nelze používat LoadSceneAfterFinish");
            return;
        }

        SaveLoadManager.Instance.Progress.Items = SaveLoadManager.Instance.Progress.LevelConfig.ItemsRevert.Select(item => new ItemAmount
        {
            ItemType = item.ItemType,
            Amount = item.Amount,
        }).ToList();
        SaveLoadManager.Instance.Progress.Money = SaveLoadManager.Instance.Progress.LevelConfig.MoneyRevert;

        SaveLoadManager.Instance.Progress.LevelConfig.Scene = SceneLoaderManager.ActiveScene.None;
        SaveLoadManager.Instance.Save(SaveLoadManager.SaveType.Progress);

        SceneLoaderManager.Instance.LoadScene(LevelFlowManager.LoadSceneAfterFinish);

    }

    public void BackToMenu()
    {

        SaveLoadManager.Instance.Load(SaveLoadManager.SaveType.Progress);
        SceneLoaderManager.Instance.LoadScene(SceneLoaderManager.ActiveScene.MainMenu);
    }

    public void ResetLevel()
    {
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
