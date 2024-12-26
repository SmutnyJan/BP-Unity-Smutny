using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    public int MovementSpeed;

    private PlayerInputSystem _inputSystem;
    private Rigidbody2D _rigidbody;


    private void Awake()
    {
        _inputSystem = new PlayerInputSystem();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {

        _inputSystem.Enable();
    }

    void Start()
    {

    }

    private void FixedUpdate()
    {
        float moveDir = _inputSystem.Player.Horizontal.ReadValue<float>();

        if (moveDir != 0)
        {
            _rigidbody.linearVelocity = new Vector2(moveDir * MovementSpeed, 0);
        }


    }

    private void OnDisable()
    {
        _inputSystem?.Disable();
    }
}
