using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenzaShopController : MonoBehaviour
{
    private ItemType _selectedItem;

    public GameObject ItemShopGrid;
    public GameObject InventoryGrid;
    public GameObject ShopItemPrefab;
    public TextMeshProUGUI MoneyText;

    #region Item Details
    public GameObject ItemDetailsPanel;
    public TextMeshProUGUI TitleText;
    public Image Icon;
    public TextMeshProUGUI PriceText;
    public TextMeshProUGUI AmountText;
    public TextMeshProUGUI SubtitleText;
    public TextMeshProUGUI DescriptionText;
    public Button BuyButton;

    public GameObject ItemPricePanel;
    public GameObject AmountPanel;
    #endregion

    void Start()
    {
        LoadUserInventory();
        LoadItemShop();
        BuyButton.onClick.AddListener(() => OnBuyButtonClick());

    }



    // Update is called once per frame
    void Update()
    {

    }

    private void LoadItemShop()
    {
        foreach (ItemType item in ItemLibraryManager.Instance.UIItems.Keys)
        {

            GameObject newItem = Instantiate(ShopItemPrefab, ItemShopGrid.transform);


            ShopUIItem itemController = newItem.GetComponent<ShopUIItem>();
            itemController.ShopController = this;
            itemController.IsInventoryItem = false;
            itemController.ItemType = item;

            itemController.LoadValues();

        }
    }

    private void LoadUserInventory()
    {
        foreach (Transform child in InventoryGrid.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (ItemAmount item in SaveLoadManager.Instance.Progress.Items)
        {

            GameObject newItem = Instantiate(ShopItemPrefab, InventoryGrid.transform);

            ShopUIItem itemController = newItem.GetComponent<ShopUIItem>();
            itemController.ShopController = this;
            itemController.IsInventoryItem = true;
            itemController.ItemType = item.ItemType;


            itemController.LoadValues();

        }
    }

    public void ChangeDetailsShop(ItemType itemType)
    {
        _selectedItem = itemType;
        if (!ItemDetailsPanel.activeSelf) ItemDetailsPanel.SetActive(true);

        AmountPanel.SetActive(false);
        ItemPricePanel.SetActive(true);
        BuyButton.gameObject.SetActive(true);


        UIItem ItemClicked = ItemLibraryManager.Instance.UIItems[itemType];
        TitleText.text = ItemClicked.Title;
        Icon.sprite = ItemClicked.Icon;
        PriceText.text = ItemClicked.UnitPrice.ToString();
        SubtitleText.text = ItemClicked.Subtitle;
        DescriptionText.text = ItemClicked.Description;
    }

    public void ChangeDetailsInventory(ItemType itemType)
    {
        _selectedItem = itemType;
        if (!ItemDetailsPanel.activeSelf) ItemDetailsPanel.SetActive(true);


        AmountPanel.SetActive(true);
        ItemPricePanel.SetActive(false);
        BuyButton.gameObject.SetActive(false);


        UIItem ItemClicked = ItemLibraryManager.Instance.UIItems[itemType];
        TitleText.text = ItemClicked.Title;
        Icon.sprite = ItemClicked.Icon;
        AmountText.text = SaveLoadManager.Instance.Progress.Items.First(x => x.ItemType == itemType).Amount.ToString();
        SubtitleText.text = ItemClicked.Subtitle;
        DescriptionText.text = ItemClicked.Description;
    }

    private void OnBuyButtonClick()
    {
        int price = ItemLibraryManager.Instance.UIItems[_selectedItem].UnitPrice;
        if (price > SaveLoadManager.Instance.Progress.Money)
        {
            AudioManager.Instance.PlayClipByName("Item_Error", AudioManager.Instance.AudioLibrary.Player, AudioManager.Instance.SFXAudioSource);
            return;
        }
        AudioManager.Instance.PlayClipByName("UI_Button_Click_1", AudioManager.Instance.AudioLibrary.UI, AudioManager.Instance.SFXAudioSource);

        SaveLoadManager.Instance.Progress.Money -= price;

        System.Collections.Generic.List<ItemAmount> userItems = SaveLoadManager.Instance.Progress.Items;
        if (userItems.Any(x => x.ItemType == _selectedItem))
        {
            userItems.First(x => x.ItemType == _selectedItem).Amount++;
        }
        else
        {
            userItems.Add(new ItemAmount() { ItemType = _selectedItem, Amount = 1 });
        }

        MoneyText.text = SaveLoadManager.Instance.Progress.Money.ToString();

        LoadUserInventory();
    }
}
