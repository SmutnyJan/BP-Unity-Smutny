using UnityEngine;

public class HourglassItem : ScriptableObject, IUsableItem
{
    private PlayerPlatformerMovementController _playerPlatformerMovementController { get; set; }

    public void InitializeItem()
    {
        _playerPlatformerMovementController = GameObject.FindWithTag("Player").GetComponent<PlayerPlatformerMovementController>();
        FullScreenShaderManager.Instance.SwitchToShader(FullScreenShaderManager.FullScreenShader.Magic);
        _playerPlatformerMovementController.StartPositionTracking();
        SeasonsManager.Instance.OnSeasonChangeStarted += OnSeasonChanged;
    }

    private void OnSeasonChanged(SeasonsManager.Season season)
    {
        FullScreenShaderManager.Instance.SwitchToShader(FullScreenShaderManager.FullScreenShader.Magic);
    }

    public void UseItem()
    {
        _playerPlatformerMovementController.gameObject.transform.position = _playerPlatformerMovementController.ResetPositionTracking();
    }

    public void UnselectItem()
    {
        _playerPlatformerMovementController.StopPositionTracking();
        FullScreenShaderManager.Instance.SwitchToShader(FullScreenShaderManager.FullScreenShader.None);
        SeasonsManager.Instance.OnSeasonChangeStarted -= OnSeasonChanged;
    }
}
