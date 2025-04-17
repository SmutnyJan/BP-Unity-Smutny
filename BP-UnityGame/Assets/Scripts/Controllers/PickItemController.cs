using UnityEngine;
using System.Collections;
using System.Linq;

public class PickItemController : MonoBehaviour
{
    public ItemType ItemType;
    public LobbyInventoryController LobbyInventoryController;

    private Collider2D _itemCollider;
    private float _pickupDelay = 3f;

    void Start()
    {
        _itemCollider = GetComponent<Collider2D>();

        StartCoroutine(EnablePickup());
    }

    IEnumerator EnablePickup()
    {
        yield return new WaitForSeconds(_pickupDelay);
        _itemCollider.excludeLayers = 0;

        StartCoroutine(DestroyAfterTime());

    }

    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(25);

        Destroy(gameObject);

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            int newAmount = ++SaveLoadManager.Instance.Progress.Items.First(x => x.ItemType == ItemType).Amount;
            LobbyInventoryController.NewItemRecieved(ItemType, newAmount);

            Destroy(gameObject);
        }
    }

}
