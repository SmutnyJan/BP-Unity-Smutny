using UnityEngine;
using System.Collections;

public class OpenChestController : MonoBehaviour
{
    public GameObject Content;
    public int Amount;
    private float _spawnForce = 5f;
    private float _spawnDelay = 0.2f;

    private Animator animator;
    private bool isOpened = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isOpened && collision.gameObject.CompareTag("Player"))
        {
            isOpened = true;
            //animator.SetTrigger("Open");
            StartCoroutine(SpawnContent());
        }
    }

    IEnumerator SpawnContent()
    {
        for (int i = 0; i < Amount; i++)
        {
            GameObject spawnedItem = Instantiate(Content, transform.position, Quaternion.identity);
            Rigidbody2D rb = spawnedItem.GetComponent<Rigidbody2D>();
            float randomForceX = Random.Range(-2f, 2f);
            rb.AddForce(new Vector2(randomForceX, _spawnForce), ForceMode2D.Impulse);
            yield return new WaitForSeconds(_spawnDelay);
        }
    }
}
