using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerEffectsController : MonoBehaviour
{
    public PlayerPlatformerMovementController player;
    private List<PlayerEffect> _activeEffects = new List<PlayerEffect>();


    private Coroutine _resetJumpForceCoroutine;
    private Coroutine _resetSpeedCoroutine;
    private Coroutine _resetZoomCoroutine;


    private const int _JUMP_SPEED_BONUS = 25;
    private const int _MOVEMENT_SPEED_BONUS = 15;
    private const int _CAMERA_ZOOM_OUT_BONUS = 2;

    private Vector3 _backgroundScale;
    private Vector3 _backgroundScaleZoomed = new(12.1f, 12.1f, 0);


    public enum PlayerEffect
    {
        Speed,
        ZoomOut,
        Jump
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
                if(_resetJumpForceCoroutine != null)
                {
                    StopCoroutine(_resetJumpForceCoroutine);
                }
                if (!_activeEffects.Contains(effect))
                {
                    player.JumpForce += _JUMP_SPEED_BONUS;
                }

                _resetJumpForceCoroutine = StartCoroutine(ResetJumpForceAfterDelay(ItemLibraryManager.Instance.UIItems[ItemType.JumpCoil].UpTime));

                break;
            case PlayerEffect.Speed:
                if(_resetSpeedCoroutine != null)
                {
                    StopCoroutine(_resetSpeedCoroutine);
                }
                if (!_activeEffects.Contains(effect))
                {
                    player.MovementSpeed += _MOVEMENT_SPEED_BONUS;
                }
                _resetSpeedCoroutine = StartCoroutine(ResetSpeedAfterDelay(ItemLibraryManager.Instance.UIItems[ItemType.Boots].UpTime));
                break;

            case PlayerEffect.ZoomOut:
                if(_resetZoomCoroutine != null)
                {
                    StopCoroutine(_resetZoomCoroutine);
                }
                if(!_activeEffects.Contains(effect))
                {
                    player.Camera.orthographicSize += _CAMERA_ZOOM_OUT_BONUS;
                    player.Background.transform.localScale = _backgroundScaleZoomed;
                }

                _resetZoomCoroutine = StartCoroutine(ResetBinocularsAfterDelay(ItemLibraryManager.Instance.UIItems[ItemType.Binoculars].UpTime));
                break;
        }

        if (!_activeEffects.Contains(effect))
        {
            _activeEffects.Add(effect);
        }
    }

    private IEnumerator ResetJumpForceAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        player.JumpForce -= _JUMP_SPEED_BONUS;
        _resetJumpForceCoroutine = null;
        _activeEffects.Remove(PlayerEffect.Jump);
    }

    private IEnumerator ResetSpeedAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        player.MovementSpeed -= _MOVEMENT_SPEED_BONUS;
        _resetSpeedCoroutine = null;
        _activeEffects.Remove(PlayerEffect.Speed);
    }

    private IEnumerator ResetBinocularsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        player.Camera.orthographicSize -= _CAMERA_ZOOM_OUT_BONUS;
        player.Background.transform.localScale = _backgroundScale;
        _activeEffects.Remove(PlayerEffect.ZoomOut);

    }
}
