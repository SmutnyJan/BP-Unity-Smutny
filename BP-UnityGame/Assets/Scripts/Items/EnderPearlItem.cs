using UnityEngine;

public class EnderPearlItem : ScriptableObject, IUsableItem
{
    private GameObject _player { get; set; }
    private GameObject _itemPrefab { get; set; }

    public EnderPearlItem()
    {

    }

    public void InitializeItem()
    {
        _player = GameObject.FindWithTag("Player");
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
        UseEnderPearl.Player = _player;
        int offset = _player.GetComponent<SpriteRenderer>().flipX ? 1 : -1;
        Instantiate(_itemPrefab, _player.transform.position + new Vector3(2 * offset, 0, 0), _player.transform.localRotation);
    }

    public void UnselectItem()
    {
        FullScreenShaderManager.Instance.SwitchToShader(FullScreenShaderManager.FullScreenShader.None);
        SeasonsManager.Instance.OnSeasonChangeStarted -= OnSeasonChanged;
    }
}
