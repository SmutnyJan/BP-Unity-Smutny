using System.Linq;
using UnityEngine;

public class BootsItem : ScriptableObject, IUsableItem
{

    private PlayerPlatformerMovementController PlayerPlatformerMovementController { get; set; }


    public void InitializeItem()
    {
        PlayerPlatformerMovementController = GameObject.FindGameObjectsWithTag("Player").First().GetComponent<PlayerPlatformerMovementController>();
    }
    public void UseItem()
    {
        PlayerPlatformerMovementController.MovementSpeed *= 2;
    }

    public void UnselectItem()
    {
        throw new System.NotImplementedException();
    }

}
