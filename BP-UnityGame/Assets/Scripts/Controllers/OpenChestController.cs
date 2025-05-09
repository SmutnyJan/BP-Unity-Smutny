using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class OpenChestController : MonoBehaviour
{
    public List<GameObject> ChestContent;
    private float _spawnForce = 2f;
    private float _spawnDelay = 0.2f;
    private Animator _animator;
    private bool _isOpened = false;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider;

    void Start()
    {
#if UNITY_EDITOR
        if (!Regex.Match(this.name, @"^Chest \((\d+)\)$").Success)
        {
            Debug.LogWarning("Chest name " + this.name + " incorrect format!");
        }
#endif
        SeasonsManager.Instance.OnSeasonChangeStarted += OnSeasonChangeStarted;
        _animator = GetComponent<Animator>();

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();

    }

    private void OnSeasonChangeStarted(SeasonsManager.Season season)
    {
        if (season == SeasonsManager.Season.Summer)
        {
            _spriteRenderer.enabled = false;
            _boxCollider.enabled = false;
        }
        else if (season == SeasonsManager.Season.Autumn)
        {
            _spriteRenderer.enabled = true;
            _boxCollider.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_isOpened && collision.gameObject.CompareTag("Player"))
        {
            _animator.SetTrigger("Open");
            _isOpened = true;

            SaveLoadManager.Instance.Progress.LevelConfig.ChestsOpenedIndexes.Add(
                int.Parse(Regex.Match(this.name, @"\((\d+)\)").Groups[1].Value)
            );

            StartCoroutine(SpawnContent());
        }
    }

    public void SetStateToOpen()
    {
        _isOpened = true;
        _animator.Play("Chest_Open", 0, 1f);
    }

    IEnumerator SpawnContent()
    {
        List<GameObject> itemsToSpawn = new(ChestContent);

        switch (SeasonsManager.Instance.CurrentSeason)
        {
            case SeasonsManager.Season.Winter:
                itemsToSpawn.AddRange(GetRandomSubset(ChestContent, ChestContent.Count / 2));
                break;

            case SeasonsManager.Season.Spring:
                itemsToSpawn = GetRandomSubset(ChestContent, ChestContent.Count / 2);
                break;
        }


        foreach (GameObject item in itemsToSpawn)
        {
            GameObject spawnedItem = Instantiate(item, transform.position, Quaternion.identity);
            Rigidbody2D rb = spawnedItem.GetComponent<Rigidbody2D>();

            float randomForceX = Random.Range(-1f, 1f);
            rb.AddForce(new Vector2(randomForceX, _spawnForce), ForceMode2D.Impulse);
            yield return new WaitForSeconds(_spawnDelay);
        }


    }

    private List<GameObject> GetRandomSubset(List<GameObject> source, int count)
    {
        List<GameObject> temp = new(source);
        List<GameObject> result = new();
        for (int i = 0; i < count && temp.Count > 0; i++)
        {
            int index = Random.Range(0, temp.Count);
            result.Add(temp[index]);
            temp.RemoveAt(index);
        }
        return result;
    }
}
