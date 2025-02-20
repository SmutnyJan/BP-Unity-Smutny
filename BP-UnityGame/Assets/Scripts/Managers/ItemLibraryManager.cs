using System.Collections.Generic;
using UnityEngine;

public class ItemLibraryManager : MonoBehaviour
{
    public Dictionary<ItemType, UIItem> UIItems;
    public Dictionary<ItemType, IUsableItem> InGameItems;


    public static ItemLibraryManager Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    void Start()
    {
        LoadUIItems();
        LoadIngameItems();
    }

    void Update()
    {

    }

    private void LoadUIItems()
    {
        UIItems = new Dictionary<ItemType, UIItem>
        {
            { ItemType.Pencil, new UIItem(){
                Title = "Tužka",
                Subtitle = "Obyèejná, ale efektivní",
                Description = "Pomáhá se zbavit obtížných nepøátel nebo projektilù",
                UnitPrice = 10,
                Icon = Resources.Load<Sprite>("Sprites/pencilUI")}
            },
            { ItemType.Pearl, new UIItem(){
                Title = "Endová perla",
                Subtitle = "Jak se sem dostala?",
                Description = "Hoï ji a objevíš se na místì, kde dopadne!",
                UnitPrice = 25,
                Icon = Resources.Load<Sprite>("Sprites/pearlUI")}
            }

        };
    }

    private void LoadIngameItems()
    {
        InGameItems = new Dictionary<ItemType, IUsableItem>
        {
            { ItemType.Pencil, ScriptableObject.CreateInstance<PencilItem>() },
            { ItemType.Pearl, ScriptableObject.CreateInstance<EnderPearlItem>() },
        };
    }
}

public enum ItemType
{
    Pencil,
    Pearl
}

public class UIItem
{
    public string Title;
    public string Subtitle;
    public string Description;
    public int UnitPrice;
    public Sprite Icon;
}
