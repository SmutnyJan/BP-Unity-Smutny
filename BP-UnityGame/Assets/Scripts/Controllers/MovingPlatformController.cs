using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    public GameObject TargetDestination;
    public float speed = 2f;

    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private Vector3 _currentTarget;


    void Start()
    {
        _startPosition = this.transform.position;
        _targetPosition = TargetDestination.transform.position;
        _currentTarget = _targetPosition;

    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _currentTarget, speed * Time.deltaTime);

        if (transform.position == _currentTarget)
        {
            _currentTarget = (_currentTarget == _targetPosition) ? _startPosition : _targetPosition;
        }
    }



}
