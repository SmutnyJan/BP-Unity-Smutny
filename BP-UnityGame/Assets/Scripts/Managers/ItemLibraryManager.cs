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
                Title = "Kreslíøùv pomocník",
                Subtitle = "Nepøátelé nejsou omalovánky, ale i tak je vybarvíš",
                Description = "Hráè mùže tužku hodit jako projektil. Pokud zasáhne nepøítele, zpùsobí mu poškození. Pokud trefí nepøátelský projektil, obì støely se vzájemnì znièí. Ideální pro obranu proti letícím útokùm a zároveò efektivní zpùsob, jak udržet vzdálenost od nepøátel.",
                UnitPrice = 10,
                UpTime = -1,
                Icon = Resources.Load<Sprite>("Sprites/pencilUI")}
            },
            { ItemType.Pearl, new UIItem(){
                Title = "Dimenzionální perla",
                Subtitle = "Nejrychlejší zpùsob, jak zmìnit adresu",
                Description = "Po hodu se hráè okamžitì teleportuje na místo, kam perla dopadne. Umožòuje rychlý únik z nebezpeèí nebo pøekonání pøekážek, ale špatnì zvolený cíl mùže znamenat teleport pøímo do pasti nebo mezi nepøátele.",
                UnitPrice = 25,
                UpTime = -1,
                Icon = Resources.Load<Sprite>("Sprites/pearlUI")}
            },
            { ItemType.Hourglass, new UIItem(){
                Title = "Písek èasu",
                Subtitle = "Tak trochu podvádìní, ale koho to zajímá?",
                Description = "Po aktivaci se hráè vrátí pøesnì na místo, kde byl pøed 5 vteøinami. Ideální pro opravu špatných rozhodnutí, útìk z nebezpeèné situace nebo druhý pokus na špatnì provedený skok. Èas se sice vrátí, ale okolí zùstává beze zmìny, takže si dávej pozor, kde resetuješ svou cestu.",
                UnitPrice = 50,
                UpTime = -1,
                Icon = Resources.Load<Sprite>("Sprites/hourglassUI")}
            },
            { ItemType.Boots, new UIItem(){
                Title = "Bleskoboty",
                Subtitle = "Èas na maraton? Ne, jen utíkáš o život",
                Description = "Po použití se hráè na 10 vteøin pohybuje výraznì rychleji. Zrychlený pohyb usnadòuje vyhýbání se nepøátelským útokùm, rychlé pøesuny po mapì nebo efektivnìjší uniky pøed nebezpeèím. Pozor však na ovládání – vìtší rychlost znamená i horší manévrování v úzkých prostorách.",
                UnitPrice = 15,
                UpTime = 10,
                Icon = Resources.Load<Sprite>("Sprites/speedUI")}
            },
            { ItemType.JumpCoil, new UIItem(){
                Title = "Skoková cívka",
                Subtitle = "Kdo potøebuje žebøíky?",
                Description = "Po použití se hráè na omezenou dobu mùže odrážet s vìtší silou, což mu umožní vyskoèit výš než obvykle. Ideální pro dosažení tìžko dostupných míst nebo pøekonání vysokých pøekážek. Efekt trvá po 10 vteøin a poté se skoky vrátí do normálu.",
                UnitPrice = 15,
                UpTime = 10,
                Icon = Resources.Load<Sprite>("Sprites/coilUI")}
            },
            { ItemType.Binoculars, new UIItem(){
                Title = "Orlí oèi",
                Subtitle = "Najednou vidíš, co nemáš vidìt",
                Description = "Po použití se kamera na omezenou dobu oddálí, což hráèi poskytne širší pohled na okolní prostøedí. Umožòuje lépe plánovat pohyb, odhalit skryté cesty nebo sledovat nepøátele z bezpeèné vzdálenosti. Efekt trvá po dobu 20 vteøin, poté se pohled vrátí do normálního stavu.",
                UnitPrice = 30,
                UpTime = 20,
                Icon = Resources.Load<Sprite>("Sprites/binocularsUI")}
            },

        };
    }

    private void LoadIngameItems()
    {
        InGameItems = new Dictionary<ItemType, IUsableItem>
        {
            { ItemType.Pencil, ScriptableObject.CreateInstance<PencilItem>() },
            { ItemType.Pearl, ScriptableObject.CreateInstance<EnderPearlItem>() },
            { ItemType.Hourglass, ScriptableObject.CreateInstance<HourglassItem>() },
            { ItemType.Boots, ScriptableObject.CreateInstance<BootsItem>() },
            { ItemType.JumpCoil, ScriptableObject.CreateInstance<JumpCoilItem>() },
            { ItemType.Binoculars, ScriptableObject.CreateInstance<BinocularsItem>() }
        };
    }
}

public enum ItemType
{
    Pencil,
    Pearl,
    Hourglass,
    Boots,
    JumpCoil,
    Binoculars
}

public class UIItem
{
    public string Title;
    public string Subtitle;
    public string Description;
    public int UnitPrice;
    public int UpTime; //itemy se zápornou hodnotou (-1) nemají žádný èas trvání
    public Sprite Icon;
}
