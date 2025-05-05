using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerPlatformerMovementController : MonoBehaviour
{

    [HideInInspector]
    public int MovementSpeed = 10;
    [HideInInspector]
    public int JumpForce = 25;
    public Camera Camera;
    public GameObject Background;
    public PlatformCollisionController PlatformCollisionController;
    public LobbyInventoryController LobbyInventoryController;
    public GameObject TimewarpPoint;
    public BuildingMenuController BuildingMenuController;
    public PlayerEffectsController PlayerEffectsController;
    public Canvas SeasonInfoCanvas;
    public Animator Animator;
    public GameObject PlayerRig;


    private PlayerInputSystem _inputSystem;
    private Rigidbody2D _rigidbody;
    private bool _hasSecondJump = true;
    private bool _controllRevertLock = false;
    private Vector3 _scale;


    private void Awake()
    {
        _inputSystem = new PlayerInputSystem();
        _rigidbody = GetComponent<Rigidbody2D>();
        _scale = PlayerRig.transform.localScale;

    }

    private void OnEnable()
    {

        _inputSystem.PlayerPlatformer.Enable();
    }

    private void OnDisplayInventory()
    {
        LobbyInventoryController.ToggleInventory();
    }

    private void OnDisplayMenu()
    {
        BuildingMenuController.ToggleMenu();
    }

    private void OnDisplaySeasonInfo()
    {
        SeasonInfoCanvas.enabled = !SeasonInfoCanvas.enabled;
    }

    private void OnJump()
    {
        if (!_controllRevertLock && PlayerEffectsController.ActiveEffects.Contains(PlayerEffectsController.PlayerEffect.ControllReverse))
        {
            _controllRevertLock = true;
            OnDown();
            return;
        }
        _controllRevertLock = false;

        bool isSummer = SeasonsManager.Instance.CurrentSeason == SeasonsManager.Season.Summer;

        if (PlatformCollisionController.IsGrounded)
        {
            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, JumpForce);

            if (isSummer)
            {
                _hasSecondJump = true;
            }
        }
        else if (isSummer && _hasSecondJump)
        {
            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, JumpForce);
            _hasSecondJump = false;
        }
    }

    private void OnDown()
    {
        if (!_controllRevertLock && PlayerEffectsController.ActiveEffects.Contains(PlayerEffectsController.PlayerEffect.ControllReverse))
        {
            _controllRevertLock = true;
            OnJump();
            return;
        }
        _controllRevertLock = false;
        if (PlatformCollisionController.IsOnPlatform)
        {
            PlatformsManager.Instance.FlipPlatform(PlatformCollisionController.TouchingPlatform);
        }
    }

    private void OnUseItem()
    {
        if (PlayerEffectsController.ActiveEffects.Contains(PlayerEffectsController.PlayerEffect.DisableItems))
        {
            AudioManager.Instance.PlayClipByName("Item_Error", AudioManager.Instance.AudioLibrary.Player, AudioManager.Instance.SFXAudioSource);
            return;
        }
        LobbyInventoryController.UseItem();
    }

    private void FixedUpdate()
    {
        float moveDir = _inputSystem.PlayerPlatformer.Horizontal.ReadValue<float>() * PlayerEffectsController.XMoveReverseCoeficient;

        if (!Animator.GetBool("IsRunning") && moveDir != 0)
        {
            Animator.SetBool("IsRunning", true);
        }
        else if (Animator.GetBool("IsRunning") && moveDir == 0)
        {
            Animator.SetBool("IsRunning", false);
        }


        if (PlayerRig.transform.localScale.x == -_scale.x && moveDir > 0)
        {
            PlayerRig.transform.localScale = new Vector3(_scale.x, _scale.y, _scale.z);
        }
        else if (PlayerRig.transform.localScale.x == _scale.x && moveDir < 0)
        {
            PlayerRig.transform.localScale = new Vector3(-_scale.x, _scale.y, _scale.z);
        }


        Debug.Log(_rigidbody.linearVelocityY);
        Animator.SetFloat("VerticalVelocity", _rigidbody.linearVelocityY);

        if (moveDir != 0 || SeasonsManager.Instance.CurrentSeason != SeasonsManager.Season.Winter) //klouzání
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
        TimewarpPoint.SetActive(true);


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
        TimewarpPoint.SetActive(false);
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



    private void OnDisable()
    {
        _inputSystem?.PlayerPlatformer.Disable();
    }
}
