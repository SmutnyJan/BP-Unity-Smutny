using System.Collections;
using System.Linq;
using UnityEngine;

public class PickItemController : MonoBehaviour
{
    public ItemType ItemType;

    private Collider2D _itemCollider;
    private float _pickupDelay = 1.5f;

    void Start()
    {
        _itemCollider = GetComponent<Collider2D>();

        StartCoroutine(EnablePickup());
    }

    IEnumerator EnablePickup()
    {
        if(SeasonsManager.Instance.CurrentSeason != SeasonsManager.Season.Autumn)
        {
            yield return new WaitForSeconds(_pickupDelay);
        }
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
            AudioManager.Instance.PlayClipByName("PickUp", AudioManager.Instance.AudioLibrary.Player, AudioManager.Instance.SFXAudioSource);

            int newAmount = ++SaveLoadManager.Instance.Progress.Items.First(x => x.ItemType == ItemType).Amount;

            collision.gameObject.GetComponent<PlayerPlatformerMovementController>().LobbyInventoryController.NewItemRecieved(ItemType, newAmount);
            Destroy(gameObject);
        }
    }

}
