using UnityEngine;

public class PencilItem : ScriptableObject, IUsableItem
{
    private GameObject _player { get; set; }
    private GameObject _itemPrefab { get; set; }

    public void InitializeItem()
    {
        _player = GameObject.FindWithTag("Player");
    }

    public void UseItem()
    {
        _itemPrefab = Resources.Load<GameObject>("InGameItems/PencilInGameItem");
        UsePencil usePencil = _itemPrefab.GetComponent<UsePencil>();
        usePencil.Player = _player;
        int offset = _player.GetComponent<SpriteRenderer>().flipX ? 1 : -1;
        Instantiate(_itemPrefab, _player.transform.position + new Vector3(2 * offset, 0, 0), _player.transform.localRotation);
    }

    public void UnselectItem()
    {
    }
}
