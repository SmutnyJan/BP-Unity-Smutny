using System.Collections;
using UnityEngine;

public class PlatformCracker : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private MaterialPropertyBlock _propertyBlock;
    private BoxCollider2D _boxCollider;
    private SeasonMaterialController _seasonMaterialController;
    private CrackState _crackState;
    private float _resetTime;
    private float _resetTimeDefault = 5;
    private float _resetTimeSummer = 10;
    private AudioSource _audioSource;
    public enum CrackState
    {
        None,
        One,
        Two,
        Three
    }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _propertyBlock = new MaterialPropertyBlock();
        _seasonMaterialController = GetComponent<SeasonMaterialController>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.outputAudioMixerGroup = AudioManager.Instance.SFXAudioSource.outputAudioMixerGroup;
    }

    private void Start()
    {
        SetCrackLevel(1f, 1f);
        _crackState = CrackState.None;
        _resetTime = _resetTimeDefault;
        SeasonsManager.Instance.OnSeasonChangeStarted += OnSeasonChangeStarted;
    }

    private void OnSeasonChangeStarted(SeasonsManager.Season season)
    {
        if (season == SeasonsManager.Season.Summer)
        {
            _resetTime = _resetTimeSummer;
        }
        else
        {
            _resetTime = _resetTimeDefault;
        }
    }

    private void SetCrackLevel(float edgeMin, float edgeMax)
    {
        _spriteRenderer.GetPropertyBlock(_propertyBlock);

        _propertyBlock.SetFloat("_CrackEdgeMin", edgeMin);
        _propertyBlock.SetFloat("_CrackEdgeMax", edgeMax);

        _spriteRenderer.SetPropertyBlock(_propertyBlock);
    }

    public void Crack()
    {
        switch (_crackState)
        {
            case CrackState.None:
                _crackState = CrackState.One;
                SetCrackLevel(0.2f, 0);
                break;
            case CrackState.One:
                _crackState = CrackState.Two;
                SetCrackLevel(0.5f, 0.1f);
                break;
            case CrackState.Two:
                _crackState = CrackState.Three;
                SetCrackLevel(0.7f, 0.1f);
                break;
            case CrackState.Three:
                _crackState = CrackState.Three;
                StartCoroutine(HandleBreak());
                break;
        }
    }

    private IEnumerator HandleBreak()
    {
        _spriteRenderer.enabled = false;
        _boxCollider.enabled = false;

        int random = Random.Range(1, 3);
        AudioManager.Instance.PlayClipByName("Platform_Break_" + random, AudioManager.Instance.AudioLibrary.Player, _audioSource);

        yield return new WaitForSeconds(_resetTime);
        _seasonMaterialController.SwitchToSeason(SeasonsManager.Instance.CurrentSeason);
        _spriteRenderer.enabled = true;
        _boxCollider.enabled = true;
        _crackState = CrackState.None;
        SetCrackLevel(1f, 1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "GroundCheck" || SeasonsManager.Instance.CurrentSeason == SeasonsManager.Season.Winter) //odstranìní duplicitního volání
        {
            return;
        }

        Crack();
    }

    private void OnDestroy()
    {
        SeasonsManager.Instance.OnSeasonChangeStarted -= OnSeasonChangeStarted;
    }
}
