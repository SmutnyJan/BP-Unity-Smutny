using UnityEngine;

public class BinocularsItem : ScriptableObject, IUsableItem
{
    private PlayerPlatformerMovementController _playerPlatformerMovementController { get; set; }

    public void InitializeItem()
    {
        _playerPlatformerMovementController = GameObject.FindWithTag("Player").GetComponent<PlayerPlatformerMovementController>();
    }

    public void UseItem()
    {
        _playerPlatformerMovementController.PlayerEffectsController.AddEffect(PlayerEffectsController.PlayerEffect.ZoomOut);
    }

    public void UnselectItem()
    {
    }
}
