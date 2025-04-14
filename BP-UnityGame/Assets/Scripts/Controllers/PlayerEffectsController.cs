using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectsController : MonoBehaviour
{
    public PlayerPlatformerMovementController player;
    public List<PlayerEffect> ActiveEffects = new();
    public int XMoveReverseCoeficient = 1;

    private Coroutine _resetJumpForceCoroutine;
    private Coroutine _resetSpeedCoroutine;
    private Coroutine _resetZoomCoroutine;
    private Coroutine _reverseCameraCoroutine;


    private const int _JUMP_SPEED_BONUS = 25;
    private const int _MOVEMENT_SPEED_BONUS = 15;
    private const int _CAMERA_ZOOM_OUT_BONUS = 2;
    private const int _CAN_EFFECT_UPTIME = 5;


    private Vector3 _backgroundScale;
    private Vector3 _backgroundScaleZoomed = new(12.1f, 12.1f, 0);


    public enum PlayerEffect
    {
        Speed,
        ZoomOut,
        Jump,
        ControllReverse
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _backgroundScale = player.Background.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddEffect(PlayerEffect effect)
    {
        switch (effect)
        {
            case PlayerEffect.Jump:
                if (_resetJumpForceCoroutine != null)
                {
                    StopCoroutine(_resetJumpForceCoroutine);
                }
                if (!ActiveEffects.Contains(effect))
                {
                    player.JumpForce += _JUMP_SPEED_BONUS;
                }

                _resetJumpForceCoroutine = StartCoroutine(ResetJumpForceAfterDelay(ItemLibraryManager.Instance.UIItems[ItemType.JumpCoil].UpTime));

                break;
            case PlayerEffect.Speed:
                if (_resetSpeedCoroutine != null)
                {
                    StopCoroutine(_resetSpeedCoroutine);
                }
                if (!ActiveEffects.Contains(effect))
                {
                    player.MovementSpeed += _MOVEMENT_SPEED_BONUS;
                }
                _resetSpeedCoroutine = StartCoroutine(ResetSpeedAfterDelay(ItemLibraryManager.Instance.UIItems[ItemType.Boots].UpTime));
                break;

            case PlayerEffect.ZoomOut:
                if (_resetZoomCoroutine != null)
                {
                    StopCoroutine(_resetZoomCoroutine);
                }
                if (!ActiveEffects.Contains(effect))
                {
                    player.Camera.orthographicSize += _CAMERA_ZOOM_OUT_BONUS;
                    player.Background.transform.localScale = _backgroundScaleZoomed;
                }

                _resetZoomCoroutine = StartCoroutine(ResetBinocularsAfterDelay(ItemLibraryManager.Instance.UIItems[ItemType.Binoculars].UpTime));
                break;

            case PlayerEffect.ControllReverse:
                if (_reverseCameraCoroutine != null)
                {
                    StopCoroutine(_reverseCameraCoroutine);
                }
                if (!ActiveEffects.Contains(effect))
                {
                    XMoveReverseCoeficient = -1;
                }

                _reverseCameraCoroutine = StartCoroutine(RevertControllsAfterDelay(_CAN_EFFECT_UPTIME));
                break;
        }

        if (!ActiveEffects.Contains(effect))
        {
            ActiveEffects.Add(effect);
        }
    }

    private IEnumerator ResetJumpForceAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        player.JumpForce -= _JUMP_SPEED_BONUS;
        _resetJumpForceCoroutine = null;
        ActiveEffects.Remove(PlayerEffect.Jump);
    }

    private IEnumerator ResetSpeedAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        player.MovementSpeed -= _MOVEMENT_SPEED_BONUS;
        _resetSpeedCoroutine = null;
        ActiveEffects.Remove(PlayerEffect.Speed);
    }

    private IEnumerator ResetBinocularsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        player.Camera.orthographicSize -= _CAMERA_ZOOM_OUT_BONUS;
        player.Background.transform.localScale = _backgroundScale;
        ActiveEffects.Remove(PlayerEffect.ZoomOut);

    }

    private IEnumerator RevertControllsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        XMoveReverseCoeficient = 1;
        ActiveEffects.Remove(PlayerEffect.ControllReverse);

    }
}
