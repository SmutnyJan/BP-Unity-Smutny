using UnityEngine;

public class LobbyMenuController : MonoBehaviour
{
    public GameObject Overlay;
    public GameObject MenuPanel;

    public LobbyInventoryController LobbyInventoryController;
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
            Overlay.SetActive(false);
        }
        else
        {
            MenuPanel.SetActive(true);
            Overlay.SetActive(true);
        }
    }

    public void CloseMenu()
    {
        MenuPanel.SetActive(false);
        Overlay.SetActive(false);
    }

    public void BackToMenu()
    {

    }
}
