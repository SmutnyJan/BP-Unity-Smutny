using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public class PencilItem : ScriptableObject, IUsableItem
{
    private GameObject Player { get; set; }

    private GameObject ItemPrefab { get; set; }

    public PencilItem()
    {
    }

    public void UseItem()
    {
        Player = GameObject.FindGameObjectsWithTag("Player").First();
        ItemPrefab = Resources.Load<GameObject>("InGameItems/PencilInGameItem");

        Instantiate(ItemPrefab, Player.transform.position + new Vector3(2, 0, 0), Player.transform.localRotation);
    }

}
