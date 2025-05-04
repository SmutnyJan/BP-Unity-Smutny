using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    public GameObject TargetDestination;
    private float _defaultSpeed = 2;
    private float _speed;
    private float _higherSpeed;
    private float _lowerSpeed;

    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private Vector3 _currentTarget;


    void Start()
    {
        _startPosition = this.transform.position;
        _targetPosition = TargetDestination.transform.position;
        _currentTarget = _targetPosition;

        _speed = _defaultSpeed;
        _higherSpeed = _speed * _defaultSpeed;
        _lowerSpeed = _speed / _defaultSpeed;

        _speed = _higherSpeed; //rovnou nastavíme _higherSpeed, protože zaèínáme na jaøe

        SeasonsManager.Instance.OnSeasonChangeStarted += OnSeasonChangeStarted;
    }

    private void OnSeasonChangeStarted(SeasonsManager.Season season)
    {
        switch (season)
        {
            case SeasonsManager.Season.Spring:
                _speed = _higherSpeed;
                break;
            case SeasonsManager.Season.Winter:
                _speed = _lowerSpeed;
                break;
            default:
                _speed = _defaultSpeed;
                break;
        }

    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _currentTarget, _speed * Time.deltaTime);

        if (transform.position == _currentTarget)
        {
            _currentTarget = (_currentTarget == _targetPosition) ? _startPosition : _targetPosition;
        }
    }

    private void OnDestroy()
    {
        SeasonsManager.Instance.OnSeasonChangeStarted -= OnSeasonChangeStarted;
    }
}
