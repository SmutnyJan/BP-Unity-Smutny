using TMPro;
using UnityEngine;

public class GameTitleAnimationController : MonoBehaviour
{
    private float _pulseSpeed = 5f;
    private float _pulseAmount = 0.1f;

    private Vector3 _originalScale;

    void Start()
    {
        _originalScale = transform.localScale;
    }

    void Update()
    {
        float scale = 1f + Mathf.Sin(Time.time * _pulseSpeed) * _pulseAmount;
        transform.localScale = _originalScale * scale;
    }
}
