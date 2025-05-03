using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIItem : MonoBehaviour
{
    public ItemType ItemType;
    public LobbyInventoryController LobbyInventoryController;
    public TextMeshProUGUI AmountText;

    private Sprite _UISprite;
    private Button _button;



    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => OnItemClick());
    }

    public void LoadValues()
    {
        _UISprite = ItemLibraryManager.Instance.UIItems[ItemType].Icon;

        this.GetComponent<Image>().sprite = _UISprite;

        AmountText.text = SaveLoadManager.Instance.Progress.Items.First(x => x.ItemType == ItemType).Amount.ToString();

    }

    private void OnItemClick()
    {
        AudioManager.Instance.PlayClipByName("UI_Button_Click_2", AudioManager.Instance.AudioLibrary.UI, AudioManager.Instance.SFXAudioSource);

        LobbyInventoryController.ChangeDetailsInventory(ItemType);

    }

    public void UpdateAmountText(int newAmount)
    {
        AmountText.text = newAmount.ToString();
    }
}
