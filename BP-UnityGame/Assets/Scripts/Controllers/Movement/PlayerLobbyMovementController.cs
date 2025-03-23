using UnityEngine;

public class PlayerLobbyMovementController : MonoBehaviour
{
    public int MovementSpeed;
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
        _inputSystem.PlayerLobby.Enable();
    }

    private void OnDisplayInventory()
    {
        if (LobbyInventoryController != null)
        {
            LobbyInventoryController.ToggleInventory();
        }
    }
    void Start()
    {

    }


    private void FixedUpdate()
    {
        Vector2 moveDir = _inputSystem.PlayerLobby.Movement.ReadValue<Vector2>();


        if (!_spriteRenderer.flipX && moveDir.x > 0)
        {
            _spriteRenderer.flipX = true;
        }
        else if (_spriteRenderer.flipX && moveDir.x < 0)
        {
            _spriteRenderer.flipX = false;
        }


        _rigidbody.linearVelocity = moveDir * MovementSpeed;
    }

    private void OnDisable()
    {
        _inputSystem?.PlayerLobby.Disable();
    }




}
