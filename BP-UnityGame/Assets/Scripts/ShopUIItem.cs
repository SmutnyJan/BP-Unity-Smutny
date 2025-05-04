using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUIItem : MonoBehaviour
{
    public ItemType ItemType;
    public MenzaShopController ShopController;
    public TextMeshProUGUI AmountText;
    public bool IsInventoryItem;

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

        if (IsInventoryItem)
        {
            AmountText.text = SaveLoadManager.Instance.Progress.Items.First(x => x.ItemType == ItemType).Amount.ToString();
        }
        else
        {
            AmountText.text = "";
        }
    }

    private void OnItemClick()
    {
        AudioManager.Instance.PlayClipByName("UI_Button_Click_2", AudioManager.Instance.AudioLibrary.UI, AudioManager.Instance.SFXAudioSource);
        if (IsInventoryItem)
        {
            ShopController.ChangeDetailsInventory(ItemType);
        }
        else
        {
            ShopController.ChangeDetailsShop(ItemType);
        }
    }
}
