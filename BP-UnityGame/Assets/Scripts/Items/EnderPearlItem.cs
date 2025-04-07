using System;
using System.Linq;
using UnityEngine;

public class EnderPearlItem : ScriptableObject, IUsableItem
{
    private GameObject Player { get; set; }

    private GameObject ItemPrefab { get; set; }

    public EnderPearlItem()
    {

    }

    public void InitializeItem()
    {
        Player = GameObject.FindGameObjectsWithTag("Player").First();
        FullScreenShaderManager.Instance.SwitchToShader(FullScreenShaderManager.FullScreenShader.Pearl);
        SeasonsManager.Instance.OnSeasonChangeStarted += OnSeasonChanged;

    }

    private void OnSeasonChanged(SeasonsManager.Season season)
    {

        FullScreenShaderManager.Instance.SwitchToShader(FullScreenShaderManager.FullScreenShader.Pearl);
    }

    public void UseItem()
    {
        ItemPrefab = Resources.Load<GameObject>("InGameItems/EnderPearlInGameItem");
        UseEnderPearl UseEnderPearl = ItemPrefab.GetComponent<UseEnderPearl>();
        UseEnderPearl.Player = Player;

        int offset = Player.GetComponent<SpriteRenderer>().flipX ? 1 : -1;

        Instantiate(ItemPrefab, Player.transform.position + new Vector3(2 * offset, 0, 0), Player.transform.localRotation);
    }

    public void UnselectItem()
    {
        FullScreenShaderManager.Instance.SwitchToShader(FullScreenShaderManager.FullScreenShader.None);
        SeasonsManager.Instance.OnSeasonChangeStarted -= OnSeasonChanged;


    }

}
