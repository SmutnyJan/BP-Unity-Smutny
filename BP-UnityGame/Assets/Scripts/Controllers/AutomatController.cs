using System.Collections;
using UnityEngine;

public class AutomatController : MonoBehaviour
{
    public GameObject Can;
    private float _shootForce = 10f;
    private float _interval = 7.5f;
    private int _directionMultiplier;
    private void Start()
    {
        _directionMultiplier = GetComponent<SpriteRenderer>().flipX ? 1 : -1;
        StartCoroutine(SpawnCanRoutine());
    }

    private IEnumerator SpawnCanRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_interval);

            GameObject newCan = Instantiate(Can, transform.TransformPoint(new Vector3(0, -2.91f, 0)), Quaternion.identity);

            Rigidbody2D rb = newCan.GetComponent<Rigidbody2D>();

            rb.AddForce(_directionMultiplier * transform.right * _shootForce, ForceMode2D.Impulse);
        }
    }


}
