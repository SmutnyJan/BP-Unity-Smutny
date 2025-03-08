using System.Linq;
using UnityEngine;

public class HourglassItem : ScriptableObject, IUsableItem
{
    private PlayerPlatformerMovementController PlayerPlatformerMovementController { get; set; }

    public void InitializeItem()
    {
        PlayerPlatformerMovementController = GameObject.FindGameObjectsWithTag("Player").First().GetComponent<PlayerPlatformerMovementController>();

        PlayerPlatformerMovementController.StartPositionTracking();

    }

    public void UseItem()
    {
        PlayerPlatformerMovementController.gameObject.transform.position = PlayerPlatformerMovementController.ResetPositionTracking();
    }

    public void UnselectItem()
    {
        PlayerPlatformerMovementController.StopPositionTracking();
    }


}
