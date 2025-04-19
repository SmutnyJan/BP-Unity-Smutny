using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectsController : MonoBehaviour
{
    public PlayerPlatformerMovementController Player;
    public List<PlayerEffect> ActiveEffects = new();
    public int XMoveReverseCoeficient = 1;

    private Coroutine _resetJumpForceCoroutine;
    private Coroutine _resetSpeedCoroutine;
    private Coroutine _resetZoomOutCoroutine;
    private Coroutine _reverseCameraCoroutine;
    private Coroutine _resetZoomInCoroutine;
    private Coroutine _resetItemsUsageCoroutine;


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
        ControllReverse,
        ZoomIn,
        DisableItems
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _backgroundScale = Player.Background.transform.localScale;
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
                    Player.JumpForce += _JUMP_SPEED_BONUS;
                }

                _resetJumpForceCoroutine = StartCoroutine(ResetJumpForceAfterDelay(ProcessUptimeChange(ItemLibraryManager.Instance.UIItems[ItemType.JumpCoil].UpTime)));

                break;
            case PlayerEffect.Speed:
                if (_resetSpeedCoroutine != null)
                {
                    StopCoroutine(_resetSpeedCoroutine);
                }
                if (!ActiveEffects.Contains(effect))
                {
                    Player.MovementSpeed += _MOVEMENT_SPEED_BONUS;
                }
                _resetSpeedCoroutine = StartCoroutine(ResetSpeedAfterDelay(ProcessUptimeChange(ItemLibraryManager.Instance.UIItems[ItemType.Boots].UpTime)));
                break;

            case PlayerEffect.ZoomOut:
                if (_resetZoomOutCoroutine != null)
                {
                    StopCoroutine(_resetZoomOutCoroutine);
                }
                if (!ActiveEffects.Contains(effect))
                {
                    Player.Camera.orthographicSize += _CAMERA_ZOOM_OUT_BONUS;
                    Player.Background.transform.localScale = _backgroundScaleZoomed;
                }

                _resetZoomOutCoroutine = StartCoroutine(ResetBinocularsAfterDelay(ProcessUptimeChange(ItemLibraryManager.Instance.UIItems[ItemType.Binoculars].UpTime)));
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

                _reverseCameraCoroutine = StartCoroutine(RevertControllsAfterDelay(ProcessCanTimeChange()));
                break;

            case PlayerEffect.ZoomIn:
                if (_resetZoomInCoroutine != null)
                {
                    StopCoroutine(_resetZoomInCoroutine);
                }
                if (!ActiveEffects.Contains(effect))
                {
                    Player.Camera.orthographicSize -= _CAMERA_ZOOM_OUT_BONUS;
                }

                _resetZoomInCoroutine = StartCoroutine(ResetCameraZoomInAfterDelay(ProcessCanTimeChange()));
                break;

            case PlayerEffect.DisableItems:
                if (_resetItemsUsageCoroutine != null)
                {
                    StopCoroutine(_resetItemsUsageCoroutine);
                }
                if (!ActiveEffects.Contains(effect))
                {
                    Player.LobbyInventoryController.ActiveUIItem.ToggleCross(true);
                }

                _resetItemsUsageCoroutine = StartCoroutine(ResetItemsUsageAfterDelay(ProcessCanTimeChange()));
                break;
        }

        if (!ActiveEffects.Contains(effect))
        {
            ActiveEffects.Add(effect);
        }
    }

    private int ProcessUptimeChange(int uptime)
    {
        switch (SeasonsManager.Instance.CurrentSeason)
        {
            case SeasonsManager.Season.Winter:
                uptime /= 2;
                break;
            case SeasonsManager.Season.Spring:
                uptime *= 2;
                break;
        }

        return uptime;
    }

    private int ProcessCanTimeChange()
    {
        switch (SeasonsManager.Instance.CurrentSeason)
        {
            case SeasonsManager.Season.Autumn:
                return _CAN_EFFECT_UPTIME * 2;
            case SeasonsManager.Season.Spring:
                return _CAN_EFFECT_UPTIME / 2;
            default:
                return _CAN_EFFECT_UPTIME;
        }
    }

    private IEnumerator ResetJumpForceAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Player.JumpForce -= _JUMP_SPEED_BONUS;
        _resetJumpForceCoroutine = null;
        ActiveEffects.Remove(PlayerEffect.Jump);
    }

    private IEnumerator ResetSpeedAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Player.MovementSpeed -= _MOVEMENT_SPEED_BONUS;
        _resetSpeedCoroutine = null;
        ActiveEffects.Remove(PlayerEffect.Speed);
    }

    private IEnumerator ResetBinocularsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Player.Camera.orthographicSize -= _CAMERA_ZOOM_OUT_BONUS;
        Player.Background.transform.localScale = _backgroundScale;
        ActiveEffects.Remove(PlayerEffect.ZoomOut);

    }

    private IEnumerator RevertControllsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        XMoveReverseCoeficient = 1;
        ActiveEffects.Remove(PlayerEffect.ControllReverse);

    }

    private IEnumerator ResetCameraZoomInAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Player.Camera.orthographicSize += _CAMERA_ZOOM_OUT_BONUS;
        ActiveEffects.Remove(PlayerEffect.ZoomIn);

    }

    private IEnumerator ResetItemsUsageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Player.LobbyInventoryController.ActiveUIItem.ToggleCross(false);
        ActiveEffects.Remove(PlayerEffect.DisableItems);

    }
}
