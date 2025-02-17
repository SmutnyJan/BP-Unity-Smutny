using UnityEngine;

public class PlayerLobbyMovementController : MonoBehaviour
{
    public int MovementSpeed;
    public LobbyInventoryController LobbyInventoryController;
    private PlayerInputSystem _inputSystem;
    private Rigidbody2D _rigidbody;


    private void Awake()
    {
        _inputSystem = new PlayerInputSystem();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _inputSystem.PlayerLobby.Enable();
    }

    private void OnDisplayInventory()
    {
        LobbyInventoryController.ToggleInventory();
    }
    void Start()
    {

    }


    private void FixedUpdate()
    {
        Vector2 moveDir = _inputSystem.PlayerLobby.Movement.ReadValue<Vector2>();
        _rigidbody.linearVelocity = moveDir * MovementSpeed;
    }

    private void OnDisable()
    {
        _inputSystem?.PlayerLobby.Disable();
    }
}
