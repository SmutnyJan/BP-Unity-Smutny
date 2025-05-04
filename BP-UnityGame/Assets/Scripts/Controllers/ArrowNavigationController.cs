using UnityEngine;

public class ArrowNavigationController : MonoBehaviour
{
    public Transform Target;

    private bool _IsActive = false;
    private Vector3 _lastTargetPosition;
    private Vector3 _lastSelfPosition;

    void Start()
    {

    }

    public void StartNavigating(Transform target)
    {
        _IsActive = true;
        Target = target;
    }

    public void StopNavigating()
    {
        _IsActive = false;
    }


    void Update()
    {
        if (!_IsActive) return;

        if (_lastTargetPosition != Target.position || _lastSelfPosition != transform.position)
        {
            Vector2 direction = Target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            _lastTargetPosition = Target.position;
            _lastSelfPosition = transform.position;
        }
    }
}
