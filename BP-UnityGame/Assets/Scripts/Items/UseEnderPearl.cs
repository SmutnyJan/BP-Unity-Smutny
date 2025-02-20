using System.Collections;
using UnityEngine;

public class UseEnderPearl : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    public GameObject Player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _rigidBody.AddForce(new Vector2(1, 1) * 10, ForceMode2D.Impulse);
        StartCoroutine(DestroyAfterTime());

    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(25);

        Destroy(gameObject);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player.transform.position = this.transform.position;
        Destroy(gameObject);
    }
}
