using System.Collections;
using System.Linq;
using UnityEngine;

public class BootsItem : ScriptableObject, IUsableItem
{
    private PlayerPlatformerMovementController PlayerPlatformerMovementController { get; set; }
    private Coroutine _resetSpeedCoroutine;

    public void InitializeItem()
    {
        PlayerPlatformerMovementController = GameObject.FindGameObjectsWithTag("Player").First().GetComponent<PlayerPlatformerMovementController>();
    }

    public void UseItem()
    {
        if (_resetSpeedCoroutine != null)
        {
            PlayerPlatformerMovementController.StopCoroutine(_resetSpeedCoroutine);
        }

        PlayerPlatformerMovementController.MovementSpeed = PlayerPlatformerMovementController.MOVEMENT_SPEED_AFFECTED;
        _resetSpeedCoroutine = PlayerPlatformerMovementController.StartCoroutine(ResetSpeedAfterDelay(ItemLibraryManager.Instance.UIItems[ItemType.Boots].UpTime));
    }

    private IEnumerator ResetSpeedAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        PlayerPlatformerMovementController.MovementSpeed = PlayerPlatformerMovementController.MOVEMENT_SPEED;
        _resetSpeedCoroutine = null;
    }

    public void UnselectItem()
    {
    }
}
