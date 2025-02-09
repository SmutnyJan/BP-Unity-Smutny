using UnityEngine;
using UnityEngine.UI;

public class ShopUIItem : MonoBehaviour
{
    public ItemType ItemType;
    public MenzaShopController ShopController;
    public bool IsInventoryItem;

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
    }

    private void OnItemClick()
    {
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
