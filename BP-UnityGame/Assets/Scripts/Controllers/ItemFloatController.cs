using UnityEngine;

public class ItemFloatController : MonoBehaviour
{
    private float _amplitude = 0.1f;
    private float _frequency = 10f;
    private Vector3 _startLocalPos;

    void Start()
    {
        _startLocalPos = transform.localPosition;
    }

    void Update()
    {
        float y = Mathf.Sin(Time.time * _frequency) * _amplitude;
        transform.localPosition = _startLocalPos + new Vector3(0, y, 0);
    }
}
