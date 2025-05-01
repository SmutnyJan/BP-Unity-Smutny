using System.Collections;
using UnityEngine;

public class RedCanController : MonoBehaviour
{
    private bool _firstHit = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(DestroyAfterTimeCoroutine(Random.Range(3, 7)));

    }

    // Update is called once per frame
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
            int random = Random.Range(1, 3);
            AudioManager.Instance.PlayClipByName("Hit_" + random, AudioManager.Instance.AudioLibrary.Player, AudioManager.Instance.SFXAudioSource);
            collision.gameObject.GetComponent<PlayerPlatformerMovementController>().PlayerEffectsController.AddEffect(PlayerEffectsController.PlayerEffect.ControllReverse);
        }
        Destroy(gameObject);
    }
}
