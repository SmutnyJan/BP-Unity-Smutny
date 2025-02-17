using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActiveUIItem : MonoBehaviour
{
    public ItemType ItemType;
    public TextMeshProUGUI AmountText;

    private Sprite _UISprite;



    void Start()
    {

    }

    void Update()
    {

    }

    public void LoadValues()
    {
        _UISprite = ItemLibraryManager.Instance.UIItems[ItemType].Icon;

        this.GetComponent<Image>().sprite = _UISprite;

        AmountText.text = SaveLoadManager.Instance.Progress.Items.First(x => x.ItemType == ItemType).Amount.ToString();

    }
}
