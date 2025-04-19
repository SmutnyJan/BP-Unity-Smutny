using System.Collections;
using UnityEngine;

public class PaperBallSpawnerBoxAreaController : MonoBehaviour
{
    public GameObject PaperBall;
    private float _defaultSpawnInterval = 5;
    private float _spawnInterval;

    private int _defaultRecursiveLives = 2;
    private int _recursiveLives;

    private BoxCollider2D _boxCollider;

    void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        SeasonsManager.Instance.OnSeasonChangeStarted += OnSeasonChangeStarted;
        _spawnInterval = _defaultSpawnInterval;
        _recursiveLives = _defaultRecursiveLives;
        StartCoroutine(SpawnRoutine());
    }

    private void OnSeasonChangeStarted(SeasonsManager.Season season)
    {
        _spawnInterval = _defaultSpawnInterval;
        _recursiveLives = _defaultRecursiveLives;
        switch (season)
        {
            case SeasonsManager.Season.Summer:
                _spawnInterval *= 2;
                break;
            case SeasonsManager.Season.Winter:
                _recursiveLives *= 2;
                break;
        }
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            Vector2 spawnPos = GetRandomPointInBox();
            Instantiate(PaperBall, spawnPos, Quaternion.identity).GetComponent<PaperBallController>().RecursiveLives = _recursiveLives;
            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    Vector2 GetRandomPointInBox()
    {
        Vector2 size = _boxCollider.size;
        Vector2 center = (Vector2)transform.position + _boxCollider.offset;

        float x = Random.Range(center.x - size.x / 2f, center.x + size.x / 2f);
        float y = Random.Range(center.y - size.y / 2f, center.y + size.y / 2f);

        return new Vector2(x, y);
    }

    private void OnDestroy()
    {
        SeasonsManager.Instance.OnSeasonChangeStarted -= OnSeasonChangeStarted;
    }
}
