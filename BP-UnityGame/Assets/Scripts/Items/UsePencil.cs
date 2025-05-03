using System.Collections;
using UnityEngine;

public class UsePencil : MonoBehaviour
{
    public GameObject Player;

    private Rigidbody2D _rigidBody;

    void Start()
    {
        int offset = Player.GetComponent<SpriteRenderer>().flipX ? 1 : -1;
        _rigidBody = GetComponent<Rigidbody2D>();
        _rigidBody.AddForce(new Vector2(1 * offset, 0) * 10, ForceMode2D.Impulse);
        _rigidBody.AddTorque(-1, ForceMode2D.Impulse);
        StartCoroutine(DestroyAfterTime());

    }


    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(25);

        Destroy(gameObject);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
