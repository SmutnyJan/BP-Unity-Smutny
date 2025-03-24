using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonsSwitcher : MonoBehaviour
{
    public List<Material> MaterialsToChange;

    private Season _currentSeason;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentSeason = Season.Spring;

        float initialBlendValue = SeasonTransitionPoint(_currentSeason);

        foreach (var material in MaterialsToChange)
        {
            material.SetFloat("_Blend", initialBlendValue);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public enum Season
    {
        Spring,
        Summer,
        Autumn,
        Winter
    }

    private void SwitchToSeason(Season season)
    {
        float switchTo = SeasonTransitionPoint(season);


        foreach (Material material in MaterialsToChange)
        {
            StartCoroutine(TransitionMaterialFromTo(material, SeasonTransitionPoint(_currentSeason), SeasonTransitionPoint(season)));
        }

        _currentSeason = season;
    }

    private float SeasonTransitionPoint(Season season)
    {
        switch (season)
        {
            case Season.Spring:
                return 0;

            case Season.Summer:
                return 0.35f;

            case Season.Autumn:
                return 0.6f;

            case Season.Winter:
                return 1f;
            default:
                return 0;
        }
    }

    private IEnumerator TransitionMaterialFromTo(Material material, float from, float to)
    {
        float duration = 1.0f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            float blendValue = Mathf.Lerp(from, to, t);
            material.SetFloat("_Blend", blendValue);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        material.SetFloat("_Blend", to);
    }



    public void SwitchToSpring()
    {
        SwitchToSeason(Season.Spring);
    }

    public void SwitchToSummer()
    {
        SwitchToSeason(Season.Summer);
    }

    public void SwitchToAutumn()
    {
        SwitchToSeason(Season.Autumn);

    }

    public void SwitchToWinter()
    {
        SwitchToSeason(Season.Winter);

    }
}
