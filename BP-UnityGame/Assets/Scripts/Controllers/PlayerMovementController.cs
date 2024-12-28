using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    public int MovementSpeed;
    public int JumpForce;
    public PlatformCollisionController PlatformCollisionController;

    private PlayerInputSystem _inputSystem;
    private Rigidbody2D _rigidbody;


    private void Awake()
    {
        _inputSystem = new PlayerInputSystem();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {

        _inputSystem.Player.Enable();
    }

    void Start()
    {

    }

    private void OnJump()
    {
        if (PlatformCollisionController.IsGrounded)
        {
            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, JumpForce);            
        }
    }

    private void OnDown()
    {
        if(PlatformCollisionController.IsOnPlatform)
        {
            PlatformsManager.Instance.FlipPlatform(PlatformCollisionController.TouchingPlatform);
        }


    }

    private void FixedUpdate()
    {
        float moveDir = _inputSystem.Player.Horizontal.ReadValue<float>();
         _rigidbody.linearVelocity = new Vector2(moveDir * MovementSpeed, _rigidbody.linearVelocity.y);
    }

    private void OnDisable()
    {
        _inputSystem?.Player.Disable();
    }


}
