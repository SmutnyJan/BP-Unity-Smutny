using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyInventoryController : MonoBehaviour
{
    public GameObject InventoryGrid;
    public GameObject InventoryItemPrefab;
    public GameObject InventoryWrapper;
    public ActiveUIItem ActiveUIItem;
    public bool IsIngameInventory;

    #region Item Details
    public GameObject ItemDetailsPanel;
    public TextMeshProUGUI TitleText;
    public Image Icon;
    public TextMeshProUGUI AmountText;
    public TextMeshProUGUI SubtitleText;
    public TextMeshProUGUI DescriptionText;

    #endregion


    void Start()
    {
        LoadUserInventory();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UseItem()
    {
        if (ActiveUIItem.gameObject.activeSelf)
        {
            ItemAmount item = SaveLoadManager.Instance.Progress.Items.FirstOrDefault(x => x.ItemType == ActiveUIItem.ItemType);
            if (item != null && item.Amount > 0)
            {
                ItemLibraryManager.Instance.InGameItems[ActiveUIItem.ItemType].UseItem();
                ActiveUIItem.StartCooldown(ItemLibraryManager.Instance.UIItems[ActiveUIItem.ItemType].UpTime);
                item.Amount--;

                ActiveUIItem.UpdateAmountValue(item.Amount);
                UpdateInventoryAmount(item.Amount);
            }
            if (item.Amount == 0)
            {
                ItemLibraryManager.Instance.InGameItems[ActiveUIItem.ItemType].UnselectItem();
                SaveLoadManager.Instance.Progress.Items.RemoveAll(x => x.ItemType == item.ItemType);
                ActiveUIItem.gameObject.SetActive(false);
                ItemDetailsPanel.gameObject.SetActive(false);

                LoadUserInventory();
            }
        }
    }

    public void ToggleInventory()
    {
        InventoryWrapper.SetActive(!InventoryWrapper.activeSelf);

        if(InventoryWrapper.activeSelf)
        {
            AudioManager.Instance.PlayClipByName("UI_Backpack_Open", AudioManager.Instance.AudioLibrary.UI, AudioManager.Instance.SFXAudioSource);
        }
        else
        {
            AudioManager.Instance.PlayClipByName("UI_Backpack_Close", AudioManager.Instance.AudioLibrary.UI, AudioManager.Instance.SFXAudioSource);
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

            GameObject newItem = Instantiate(InventoryItemPrefab, InventoryGrid.transform);

            InventoryUIItem itemController = newItem.GetComponent<InventoryUIItem>();
            itemController.LobbyInventoryController = this;
            itemController.ItemType = item.ItemType;


            itemController.LoadValues();

        }
    }

    public void NewItemRecieved(ItemType type, int newAmount)
    {
        LoadUserInventory();
        if (ActiveUIItem.ItemType == type) { 
            AmountText.text = newAmount.ToString();
            ActiveUIItem.UpdateAmountValue(newAmount);
        }
    }

    private void UpdateInventoryAmount(int newAmount)
    {
        foreach (Transform child in InventoryGrid.transform)
        {
            InventoryUIItem itemController = child.GetComponent<InventoryUIItem>();
            if (itemController.ItemType == ActiveUIItem.ItemType)
            {
                itemController.UpdateAmountText(newAmount);
                break;
            }
        }


        AmountText.text = newAmount.ToString();
    }

    public void ChangeDetailsInventory(ItemType itemType)
    {

        if (!ItemDetailsPanel.activeSelf) ItemDetailsPanel.SetActive(true);

        if (IsIngameInventory)
        {
            if (ActiveUIItem.ItemType != itemType)
            {
                ItemLibraryManager.Instance.InGameItems[ActiveUIItem.ItemType].UnselectItem();
            }

            ActiveUIItem.ResetCoolDownUI();
            ActiveUIItem.gameObject.SetActive(true);
            ActiveUIItem.ItemType = itemType;
            ActiveUIItem.LoadValues();
            ItemLibraryManager.Instance.InGameItems[ActiveUIItem.ItemType].InitializeItem();
        }


        UIItem ItemClicked = ItemLibraryManager.Instance.UIItems[itemType];
        TitleText.text = ItemClicked.Title;
        Icon.sprite = ItemClicked.Icon;
        AmountText.text = SaveLoadManager.Instance.Progress.Items.First(x => x.ItemType == itemType).Amount.ToString();
        SubtitleText.text = ItemClicked.Subtitle;
        DescriptionText.text = ItemClicked.Description;


    }
}
