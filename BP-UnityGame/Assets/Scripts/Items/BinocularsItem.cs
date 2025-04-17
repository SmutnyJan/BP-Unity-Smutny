using System.Collections;
using System.Linq;
using UnityEngine;

public class BinocularsItem : ScriptableObject, IUsableItem
{
    private PlayerPlatformerMovementController PlayerPlatformerMovementController { get; set; }

    public void InitializeItem()
    {
        PlayerPlatformerMovementController = GameObject.FindGameObjectsWithTag("Player").First().GetComponent<PlayerPlatformerMovementController>();
    }

    public void UseItem()
    {
        PlayerPlatformerMovementController.PlayerEffectsController.AddEffect(PlayerEffectsController.PlayerEffect.ZoomOut);

    }


    public void UnselectItem()
    {
    }
}
