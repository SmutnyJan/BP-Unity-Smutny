using System.Collections;
using System.Linq;
using UnityEngine;

public class JumpCoilItem : ScriptableObject, IUsableItem
{
    private PlayerPlatformerMovementController PlayerPlatformerMovementController { get; set; }
    private Coroutine _resetJumpForceCoroutine;

    public void InitializeItem()
    {
        PlayerPlatformerMovementController = GameObject.FindGameObjectsWithTag("Player").First().GetComponent<PlayerPlatformerMovementController>();
    }

    public void UseItem()
    {
        if (_resetJumpForceCoroutine != null)
        {
            PlayerPlatformerMovementController.StopCoroutine(_resetJumpForceCoroutine);
        }

        PlayerPlatformerMovementController.JumpForce = PlayerPlatformerMovementController.JUMP_FORCE_AFFECTED;
        _resetJumpForceCoroutine = PlayerPlatformerMovementController.StartCoroutine(ResetJunpForceAfterDelay(ItemLibraryManager.Instance.UIItems[ItemType.JumpCoil].UpTime));
    }

    private IEnumerator ResetJunpForceAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        PlayerPlatformerMovementController.JumpForce = PlayerPlatformerMovementController.JUMP_FORCE;
        _resetJumpForceCoroutine = null;
    }

    public void UnselectItem()
    {
    }
}
