using UnityEngine;

public class ArrowNavigationController : MonoBehaviour
{
    public Transform Target;

    private bool _IsActive = false;

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
        if (_IsActive)
        {
            Vector2 direction = Target.position - transform.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
