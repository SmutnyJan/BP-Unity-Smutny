using UnityEngine;

public class BootsItem : ScriptableObject, IUsableItem
{
    private PlayerPlatformerMovementController _playerPlatformerMovementController { get; set; }

    public void InitializeItem()
    {
        _playerPlatformerMovementController = GameObject.FindWithTag("Player").GetComponent<PlayerPlatformerMovementController>();
    }

    public void UseItem()
    {
        _playerPlatformerMovementController.PlayerEffectsController.AddEffect(PlayerEffectsController.PlayerEffect.Speed);
    }

    public void UnselectItem()
    {
    }
}
