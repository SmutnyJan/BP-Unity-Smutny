using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyInventoryController : MonoBehaviour
{
    public GameObject InventoryGrid;
    public GameObject InventoryItemPrefab;
    public GameObject InventoryWrapper;

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

    public void ToggleInventory()
    {
        InventoryWrapper.SetActive(!InventoryWrapper.activeSelf);
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

    public void ChangeDetailsInventory(ItemType itemType)
    {
        if (!ItemDetailsPanel.activeSelf) ItemDetailsPanel.SetActive(true);


        UIItem ItemClicked = ItemLibraryManager.Instance.UIItems[itemType];
        TitleText.text = ItemClicked.Title;
        Icon.sprite = ItemClicked.Icon;
        AmountText.text = SaveLoadManager.Instance.Progress.Items.First(x => x.ItemType == itemType).Amount.ToString();
        SubtitleText.text = ItemClicked.Subtitle;
        DescriptionText.text = ItemClicked.Description;
    }
}
