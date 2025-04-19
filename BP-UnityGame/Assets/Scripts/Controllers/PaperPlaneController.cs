using UnityEngine;

public class PaperPlaneController : MonoBehaviour
{
    public GameObject TargetDestination;

    private Vector2 _startPosition;
    private Vector2 _targetPosition;
    private Vector2 _currentTarget;

    private float _floatAmplitude = 0.5f;
    private float _floatFrequency = 2f;
    private float _speed = 2f;
    public float _maxRotationAngle = 15f;

    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _startPosition = transform.position;
        _targetPosition = TargetDestination.transform.position;
        _currentTarget = _targetPosition;

        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector2 basePosition = Vector2.MoveTowards(transform.position, _currentTarget, _speed * Time.deltaTime);

        float yOffset = Mathf.Sin(Time.time * _floatFrequency) * _floatAmplitude;


        // Pokud je sprite zrcadlený (flipX), otoè smìr rotace
        float directionMultiplier = _spriteRenderer.flipX ? -1f : 1f;

        //využíváme derivace sinus (cosinus) pro výpoèet sklonu
        float rotationZ = Mathf.Cos(Time.time * _floatFrequency) * _maxRotationAngle * directionMultiplier;
        transform.rotation = Quaternion.Euler(0, 0, rotationZ);

        // Nastavení výsledné pozice
        transform.position = new Vector2(basePosition.x, _startPosition.y + yOffset);

        // Pøepnutí smìru
        if (Vector2.Distance(transform.position, _currentTarget) < 0.1f)
        {
            _currentTarget = (_currentTarget == _targetPosition) ? _startPosition : _targetPosition;
            _spriteRenderer.flipX = !_spriteRenderer.flipX;
        }
    }
}
