using System.Collections;
using System.Linq;
using UnityEngine;

public class BinocularsItem : ScriptableObject, IUsableItem
{
    private PlayerPlatformerMovementController PlayerPlatformerMovementController { get; set; }
    private Coroutine _resetBinocularsCoroutine;

    public void InitializeItem()
    {
        PlayerPlatformerMovementController = GameObject.FindGameObjectsWithTag("Player").First().GetComponent<PlayerPlatformerMovementController>();
    }

    public void UseItem()
    {
        if (_resetBinocularsCoroutine != null)
        {
            PlayerPlatformerMovementController.StopCoroutine(_resetBinocularsCoroutine);
        }

        PlayerPlatformerMovementController.ActivateBinoculars();
        _resetBinocularsCoroutine = PlayerPlatformerMovementController.StartCoroutine(ResetBinocularsAfterDelay(ItemLibraryManager.Instance.UIItems[ItemType.Binoculars].UpTime));
    }

    private IEnumerator ResetBinocularsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        PlayerPlatformerMovementController.DeactivateBinoculars();
        _resetBinocularsCoroutine = null;
    }

    public void UnselectItem()
    {
    }
}
