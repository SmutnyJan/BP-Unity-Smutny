using System.Linq;
using UnityEngine;

public class PencilItem : ScriptableObject, IUsableItem
{
    private GameObject Player { get; set; }

    private GameObject ItemPrefab { get; set; }

    public PencilItem()
    {
    }

    public void InitializeItem()
    {
        Player = GameObject.FindGameObjectsWithTag("Player").First();
    }

    public void UseItem()
    {
        ItemPrefab = Resources.Load<GameObject>("InGameItems/PencilInGameItem");

        UsePencil UsePencil = ItemPrefab.GetComponent<UsePencil>();
        UsePencil.Player = Player;

        int offset = Player.GetComponent<SpriteRenderer>().flipX ? 1 : -1;

        Instantiate(ItemPrefab, Player.transform.position + new Vector3(2 * offset, 0, 0), Player.transform.localRotation);
    }

    public void UnselectItem()
    {
    }

}
