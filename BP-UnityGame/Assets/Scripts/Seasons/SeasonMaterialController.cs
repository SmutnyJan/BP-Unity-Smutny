using System.Collections;
using UnityEngine;
using static SeasonsManager;

public class SeasonMaterialController : MonoBehaviour, ISeasonChange
{
    private Renderer _renderer;
    private MaterialPropertyBlock _propertyBlock;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _propertyBlock = new MaterialPropertyBlock();
    }

    private void Start()
    {
        SetBlend(0f);
    }


    public void SwitchToSeason(Season season)
    {
        float from = _propertyBlock.GetFloat("_Blend");
        float to = SeasonTransitionPoint(season);

        StartCoroutine(TransitionBlend(from, to));
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
