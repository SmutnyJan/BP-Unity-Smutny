using UnityEngine;

public class PencilItem : ScriptableObject, IUsableItem
{
    private PlayerPlatformerMovementController _player { get; set; }
    private GameObject _itemPrefab { get; set; }

    public void InitializeItem()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<PlayerPlatformerMovementController>();
    }

    public void UseItem()
    {
        _itemPrefab = Resources.Load<GameObject>("InGameItems/PencilInGameItem");
        UsePencil usePencil = _itemPrefab.GetComponent<UsePencil>();
        usePencil.Player = _player.gameObject;
        int offset = _player.PlayerRig.transform.localScale.x < 0 ? -1 : 1;
        Instantiate(_itemPrefab, _player.PlayerRig.transform.position + new Vector3(1.5f * offset, 2.0f, 0), _player.transform.localRotation);
    }

    public void UnselectItem()
    {
    }
}
