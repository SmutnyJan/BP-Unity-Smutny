using System.Linq;
using UnityEngine;

public class EnderPearlItem : ScriptableObject, IUsableItem
{
    private GameObject Player { get; set; }

    private GameObject ItemPrefab { get; set; }

    public EnderPearlItem()
    {
    }

    public void UseItem()
    {
        Player = GameObject.FindGameObjectsWithTag("Player").First();
        ItemPrefab = Resources.Load<GameObject>("InGameItems/EnderPearlInGameItem");
        UseEnderPearl UseEnderPearl = ItemPrefab.GetComponent<UseEnderPearl>();
        UseEnderPearl.Player = Player;

        Instantiate(ItemPrefab, Player.transform.position + new Vector3(2, 0, 0), Player.transform.localRotation);
    }

}
