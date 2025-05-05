using UnityEngine;

public class EnderPearlItem : ScriptableObject, IUsableItem
{
    private PlayerPlatformerMovementController _player { get; set; }
    private GameObject _itemPrefab { get; set; }

    public void InitializeItem()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<PlayerPlatformerMovementController>();
        FullScreenShaderManager.Instance.SwitchToShader(FullScreenShaderManager.FullScreenShader.Pearl);
        SeasonsManager.Instance.OnSeasonChangeStarted += OnSeasonChanged;
        _itemPrefab = Resources.Load<GameObject>("InGameItems/EnderPearlInGameItem");
    }

    private void OnSeasonChanged(SeasonsManager.Season season)
    {
        FullScreenShaderManager.Instance.SwitchToShader(FullScreenShaderManager.FullScreenShader.Pearl);
    }

    public void UseItem()
    {
        UseEnderPearl UseEnderPearl = _itemPrefab.GetComponent<UseEnderPearl>();
        UseEnderPearl.Player = _player.gameObject;
        int offset = _player.PlayerRig.transform.localScale.x < 0 ? -1 : 1;
        Instantiate(_itemPrefab, _player.PlayerRig.transform.position + new Vector3(1.5f * offset, 2.0f, 0), _player.transform.localRotation);
    }

    public void UnselectItem()
    {
        FullScreenShaderManager.Instance.SwitchToShader(FullScreenShaderManager.FullScreenShader.None);
        SeasonsManager.Instance.OnSeasonChangeStarted -= OnSeasonChanged;
    }
}
