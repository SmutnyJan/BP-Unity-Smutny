using System.Collections;
using UnityEngine;

public class UseEnderPearl : MonoBehaviour
{
    public GameObject Player;
    private Rigidbody2D _rigidBody;

    void Start()
    {
        int offset = Player.GetComponent<PlayerPlatformerMovementController>().PlayerRig.transform.localScale.x < 0 ? -1 : 1;
        _rigidBody = GetComponent<Rigidbody2D>();
        _rigidBody.AddForce(new Vector2(1 * offset, 1) * 10, ForceMode2D.Impulse);
        StartCoroutine(DestroyAfterTime());

    }

    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(25);

        Destroy(gameObject);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player.transform.position = this.transform.position + new Vector3(0, 1.5f, 0);
        Destroy(gameObject);
    }
}
