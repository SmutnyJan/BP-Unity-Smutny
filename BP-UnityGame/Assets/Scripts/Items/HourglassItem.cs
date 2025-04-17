using System;
using System.Linq;
using UnityEngine;

public class HourglassItem : ScriptableObject, IUsableItem
{
    private PlayerPlatformerMovementController PlayerPlatformerMovementController { get; set; }

    public void InitializeItem()
    {
        PlayerPlatformerMovementController = GameObject.FindGameObjectsWithTag("Player").First().GetComponent<PlayerPlatformerMovementController>();
        FullScreenShaderManager.Instance.SwitchToShader(FullScreenShaderManager.FullScreenShader.Magic);

        PlayerPlatformerMovementController.StartPositionTracking();

        SeasonsManager.Instance.OnSeasonChangeStarted += OnSeasonChanged;


    }

    private void OnSeasonChanged(SeasonsManager.Season season)
    {
        FullScreenShaderManager.Instance.SwitchToShader(FullScreenShaderManager.FullScreenShader.Magic);
    }

    public void UseItem()
    {
        PlayerPlatformerMovementController.gameObject.transform.position = PlayerPlatformerMovementController.ResetPositionTracking();
    }

    public void UnselectItem()
    {
        PlayerPlatformerMovementController.StopPositionTracking();

        FullScreenShaderManager.Instance.SwitchToShader(FullScreenShaderManager.FullScreenShader.None);
        SeasonsManager.Instance.OnSeasonChangeStarted -= OnSeasonChanged;
    }


}
