using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerMovementController : MonoBehaviour
{
    public const int MOVEMENT_SPEED = 10;
    public const int MOVEMENT_SPEED_AFFECTED = 25; //rychlost pøi použití ítemu "Boots"

    public const int JUMP_FORCE = 25;
    public const int JUMP_FORCE_AFFECTED = 50; //rychlost pøi použití ítemu "JumpCoil"

    [HideInInspector]
    public int MovementSpeed = MOVEMENT_SPEED;
    [HideInInspector]
    public int JumpForce = JUMP_FORCE;
    public Camera Camera;
    public GameObject Background;
    public PlatformCollisionController PlatformCollisionController;
    public LobbyInventoryController LobbyInventoryController;
    public GameObject TimewarpPoint;

    private PlayerInputSystem _inputSystem;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private float _cameraSize;
    private const float CAMERA_SIZE_ZOOMED = 10;
    private Vector3 _backgroundScale;
    private Vector3 _backgroundScaleZoomed = new(12.1f, 12.1f, 0);



    private void Awake()
    {
        _inputSystem = new PlayerInputSystem();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _cameraSize = Camera.orthographicSize;
        _backgroundScale = Background.transform.localScale;
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

    public void DeactivateBinoculars()
    {
        Camera.orthographicSize = _cameraSize;
        Background.transform.localScale = _backgroundScale;
    }

    public void ActivateBinoculars()
    {
        Camera.orthographicSize = CAMERA_SIZE_ZOOMED;
        Background.transform.localScale = _backgroundScaleZoomed;

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

        if (!PlatformCollisionController.IsTouchingMovingPlatform)
        {
            _rigidbody.linearVelocity = new Vector2(moveDir * MovementSpeed, _rigidbody.linearVelocity.y);
        }

    }

    /*private void Update() SuperHot
    {

        if (this._rigidbody.linearVelocity == Vector2.zero)
        {
            Time.timeScale = 0.1f;
        }
        else
        {
            Time.timeScale = 1;

        }
    }*/


    #region TimeWarping
    private Queue<Vector3> Positions;

    public void StartPositionTracking()
    {
        Positions = new Queue<Vector3>();

        TimewarpPoint.transform.SetParent(null);


        StartCoroutine("Track");
    }

    public Vector3 ResetPositionTracking()
    {
        Vector3 returnPoint = Positions.Dequeue();
        Positions.Clear();

        return returnPoint;
    }

    public void StopPositionTracking()
    {
        StopCoroutine("Track");

        Positions.Clear();

        TimewarpPoint.transform.SetParent(this.transform);
        TimewarpPoint.transform.localPosition = new Vector3(0, 0, 0);
    }



    private IEnumerator Track()
    {


        while (true)
        {
            Positions.Enqueue(this.transform.position);

            if (Positions.Count > 500) // 5 vteøin zpìt, mezera každých 0.1 sekund
            {
                Positions.Dequeue();
            }

            TimewarpPoint.transform.position = Positions.Peek();

            yield return new WaitForSeconds(0.01f);

        }
    }

    #endregion


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Oneway Moving Platform" &&
            collision.otherCollider.gameObject.tag == "Player" &&
            !PlatformCollisionController.IsOnPlatform)
        {
            PlatformCollisionController.IsTouchingMovingPlatform = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Oneway Moving Platform" && collision.otherCollider.gameObject.tag == "Player")
        {
            PlatformCollisionController.IsTouchingMovingPlatform = false;
        }
    }

    private void OnDisable()
    {
        _inputSystem?.PlayerPlatformer.Disable();
    }
}
