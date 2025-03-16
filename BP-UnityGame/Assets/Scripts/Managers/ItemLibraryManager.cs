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
                Title = "Kresl���v pomocn�k",
                Subtitle = "Nep��tel� nejsou omalov�nky, ale i tak je vybarv�",
                Description = "Hr�� m��e tu�ku hodit jako projektil. Pokud zas�hne nep��tele, zp�sob� mu po�kozen�. Pokud tref� nep��telsk� projektil, ob� st�ely se vz�jemn� zni��. Ide�ln� pro obranu proti let�c�m �tok�m a z�rove� efektivn� zp�sob, jak udr�et vzd�lenost od nep��tel.",
                UnitPrice = 10,
                UpTime = -1,
                Icon = Resources.Load<Sprite>("Sprites/pencilUI")}
            },
            { ItemType.Pearl, new UIItem(){
                Title = "Dimenzion�ln� perla",
                Subtitle = "Nejrychlej�� zp�sob, jak zm�nit adresu",
                Description = "Po hodu se hr�� okam�it� teleportuje na m�sto, kam perla dopadne. Umo��uje rychl� �nik z nebezpe�� nebo p�ekon�n� p�ek�ek, ale �patn� zvolen� c�l m��e znamenat teleport p��mo do pasti nebo mezi nep��tele.",
                UnitPrice = 25,
                UpTime = -1,
                Icon = Resources.Load<Sprite>("Sprites/pearlUI")}
            },
            { ItemType.Hourglass, new UIItem(){
                Title = "P�sek �asu",
                Subtitle = "Tak trochu podv�d�n�, ale koho to zaj�m�?",
                Description = "Po aktivaci se hr�� vr�t� p�esn� na m�sto, kde byl p�ed 5 vte�inami. Ide�ln� pro opravu �patn�ch rozhodnut�, �t�k z nebezpe�n� situace nebo druh� pokus na �patn� proveden� skok. �as se sice vr�t�, ale okol� z�st�v� beze zm�ny, tak�e si d�vej pozor, kde resetuje� svou cestu.",
                UnitPrice = 50,
                UpTime = -1,
                Icon = Resources.Load<Sprite>("Sprites/hourglassUI")}
            },
            { ItemType.Boots, new UIItem(){
                Title = "Bleskoboty",
                Subtitle = "�as na maraton? Ne, jen ut�k� o �ivot",
                Description = "Po pou�it� se hr�� na 10 vte�in pohybuje v�razn� rychleji. Zrychlen� pohyb usnad�uje vyh�b�n� se nep��telsk�m �tok�m, rychl� p�esuny po map� nebo efektivn�j�� uniky p�ed nebezpe��m. Pozor v�ak na ovl�d�n� � v�t�� rychlost znamen� i hor�� man�vrov�n� v �zk�ch prostor�ch.",
                UnitPrice = 15,
                UpTime = 10,
                Icon = Resources.Load<Sprite>("Sprites/speedUI")}
            },
            { ItemType.JumpCoil, new UIItem(){
                Title = "Skokov� c�vka",
                Subtitle = "Kdo pot�ebuje �eb��ky?",
                Description = "Po pou�it� se hr�� na omezenou dobu m��e odr�et s v�t�� silou, co� mu umo�n� vysko�it v�� ne� obvykle. Ide�ln� pro dosa�en� t�ko dostupn�ch m�st nebo p�ekon�n� vysok�ch p�ek�ek. Efekt trv� po 10 vte�in a pot� se skoky vr�t� do norm�lu.",
                UnitPrice = 15,
                UpTime = 10,
                Icon = Resources.Load<Sprite>("Sprites/coilUI")}
            },
            { ItemType.Binoculars, new UIItem(){
                Title = "Orl� o�i",
                Subtitle = "Najednou vid�, co nem� vid�t",
                Description = "Po pou�it� se kamera na omezenou dobu odd�l�, co� hr��i poskytne �ir�� pohled na okoln� prost�ed�. Umo��uje l�pe pl�novat pohyb, odhalit skryt� cesty nebo sledovat nep��tele z bezpe�n� vzd�lenosti. Efekt trv� po dobu 20 vte�in, pot� se pohled vr�t� do norm�ln�ho stavu.",
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
    public int UpTime; //itemy se z�pornou hodnotou (-1) nemaj� ��dn� �as trv�n�
    public Sprite Icon;
}
