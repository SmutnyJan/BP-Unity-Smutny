using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnerBoxAreaController : MonoBehaviour
{
    public List<GameObject> Objects;
    public float SpawnInterval = 10f;

    private BoxCollider2D _boxCollider;
    private Coroutine _spawnCoroutine;

    void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        SeasonsManager.Instance.OnSeasonChangeStarted += OnSeasonChangeStarted;
    }

    private void OnSeasonChangeStarted(SeasonsManager.Season season)
    {
        if (season == SeasonsManager.Season.Summer)
        {
            _spawnCoroutine = StartCoroutine(SpawnRoutine());
        }
        else
        {
            StopCoroutine(_spawnCoroutine);
        }
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            Vector2 spawnPos = GetRandomPointInBox();
            Instantiate(Objects[Random.Range(0, Objects.Count)], spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(SpawnInterval);
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


}