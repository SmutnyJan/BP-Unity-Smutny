using System.Collections;
using UnityEngine;

public class BlueCanController : MonoBehaviour
{
    private bool _firstHit = false;
    void Start()
    {
        StartCoroutine(DestroyAfterTimeCoroutine(Random.Range(3, 7)));

    }

    void Update()
    {

    }

    private IEnumerator DestroyAfterTimeCoroutine(float delay)
    {

        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_firstHit)
        {
            _firstHit = true;
            return;
        }
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<PlayerEffectsController>().AddEffect(PlayerEffectsController.PlayerEffect.DisableItems);

        }
        Destroy(gameObject);
    }
}
