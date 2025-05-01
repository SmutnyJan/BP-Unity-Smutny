using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using static SeasonsManager;
using static UnityEngine.Rendering.DebugUI;

public class SeasonMaterialController : MonoBehaviour, ISeasonChange
{
    public PhysicsMaterial2D platformMaterial;
    private Renderer _renderer;
    private MaterialPropertyBlock _propertyBlock;
    private BoxCollider2D _boxCollider;
    


    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _propertyBlock = new MaterialPropertyBlock();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        SetBlend(0f);
    }


    public void AnimateToSeason(Season season)
    {
        if (season == Season.Winter)
        {
            _boxCollider.sharedMaterial = platformMaterial;
        }
        else
        {
            _boxCollider.sharedMaterial = null;
        }

        float from = _propertyBlock.GetFloat("_Blend");
        float to = SeasonTransitionPoint(season);

        StartCoroutine(TransitionBlend(from, to));
    }

    public void SwitchToSeason(Season season)
    {
        SetBlend(SeasonTransitionPoint(season));
    }


    private float SeasonTransitionPoint(Season season)
    {
        switch (season)
        {
            case Season.Spring:
                return 1f;
            case Season.Summer:
                return 0.25f;
            case Season.Autumn:
                return 0.5f;
            case Season.Winter:
                return 0.8f;
            default:
                return 0f;
        }
    }

    private void SetBlend(float value)
    {
        _renderer.GetPropertyBlock(_propertyBlock);
        _propertyBlock.SetFloat("_Blend", value);
        _renderer.SetPropertyBlock(_propertyBlock);
    }

    private IEnumerator TransitionBlend(float from, float to)
    {
        float duration = 2.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            float value = Mathf.Lerp(from, to, t);
            SetBlend(value);

            elapsed += Time.deltaTime;
            yield return null;
        }

        if (to == 1f)
        {
            to = 0f; // pokud jsme pøešli ze zimy na jaro, musíme se vrátit v blendu zpìt na 0
        }
        SetBlend(to);


    }
}
