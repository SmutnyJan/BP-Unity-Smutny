using UnityEngine;
using System.Collections;

public class PickItemController : MonoBehaviour
{
    private Collider2D itemCollider;
    private float _pickupDelay = 3f;

    void Start()
    {
        itemCollider = GetComponent<Collider2D>();
        if (itemCollider != null)
        {
            StartCoroutine(EnablePickup());
        }
    }

    IEnumerator EnablePickup()
    {
        yield return new WaitForSeconds(_pickupDelay);
        itemCollider.excludeLayers = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    //TODO:
    // Resources.Load<Sprite>("Sprites/speedUI")}
    // naèítání itemù podle enumu
}
