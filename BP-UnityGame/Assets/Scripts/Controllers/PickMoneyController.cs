using System.Collections;
using TMPro;
using UnityEngine;

public class PickMoneyController : MonoBehaviour
{
    public int MoneyValue = 1;

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
            SaveLoadManager.Instance.Progress.Money += MoneyValue;
            int money = SaveLoadManager.Instance.Progress.Money;

            TextMeshProUGUI moneyText = GameObject.Find("Money Canvas").GetComponent<MoneyUIPartialManager>().MoneyText;

            moneyText.text = money.ToString();

            Destroy(gameObject);
        }
    }
}
