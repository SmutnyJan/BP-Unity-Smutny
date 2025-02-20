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



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => OnItemClick());

    }




    // Update is called once per frame
    void Update()
    {

    }

    public void LoadValues()
    {
        _UISprite = ItemLibraryManager.Instance.UIItems[ItemType].Icon;

        this.GetComponent<Image>().sprite = _UISprite;

        AmountText.text = SaveLoadManager.Instance.Progress.Items.First(x => x.ItemType == ItemType).Amount.ToString();

    }

    private void OnItemClick()
    {

        LobbyInventoryController.ChangeDetailsInventory(ItemType);

    }
}
