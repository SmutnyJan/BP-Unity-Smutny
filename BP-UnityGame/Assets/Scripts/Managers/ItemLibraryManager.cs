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
                Title = "Tu�ka",
                Subtitle = "Oby�ejn�, ale efektivn�",
                Description = "Pom�h� se zbavit obt�n�ch nep��tel nebo projektil�",
                UnitPrice = 10,
                Icon = Resources.Load<Sprite>("Sprites/pencilUI")}
            },
            { ItemType.Pearl, new UIItem(){
                Title = "Endov� perla",
                Subtitle = "Jak se sem dostala?",
                Description = "Ho� ji a objev� se na m�st�, kde dopadne!",
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
