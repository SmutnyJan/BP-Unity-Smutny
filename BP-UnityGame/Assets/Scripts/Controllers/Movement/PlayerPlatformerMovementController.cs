using UnityEngine;

public class PlayerPlatformerMovementController : MonoBehaviour
{
    public int MovementSpeed;
    public int JumpForce;
    public PlatformCollisionController PlatformCollisionController;
    public LobbyInventoryController LobbyInventoryController;

    private PlayerInputSystem _inputSystem;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;


    private void Awake()
    {
        _inputSystem = new PlayerInputSystem();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {

        _inputSystem.PlayerPlatformer.Enable();
    }

    void Start()
    {

    }

    private void OnDisplayInventory()
    {
        LobbyInventoryController.ToggleInventory();
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
        if (PlatformCollisionController.IsOnPlatform)
        {
            PlatformsManager.Instance.FlipPlatform(PlatformCollisionController.TouchingPlatform);
        }
    }

    private void OnUseItem()
    {
        LobbyInventoryController.UseItem();
    }

    private void FixedUpdate()
    {
        float moveDir = _inputSystem.PlayerPlatformer.Horizontal.ReadValue<float>();
        if (!_spriteRenderer.flipX && moveDir > 0)
        {
            _spriteRenderer.flipX = true;
        }
        else if (_spriteRenderer.flipX && moveDir < 0)
        {
            _spriteRenderer.flipX = false;
        }

        _rigidbody.linearVelocity = new Vector2(moveDir * MovementSpeed, _rigidbody.linearVelocity.y);

    }

    private void OnDisable()
    {
        _inputSystem?.PlayerPlatformer.Disable();
    }
}
