using System.Collections;
using UnityEngine;

public class RedCanController : MonoBehaviour
{
    private bool _firstHit = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(DestroyAfterTimeCoroutine(5));

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
        if(!_firstHit)
        {
            _firstHit = true;
            return;
        }
        if(collision.gameObject.name == "Player")
        {
            collision.gameObject.transform.position = SaveLoadManager.Instance.Progress.LevelConfig.SpawnPoint + new Vector3(0, 5, 0);
        }
        Destroy(gameObject);
    }
}
