using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class OpenChestController : MonoBehaviour
{
    public List<GameObject> ChestContent;

    private LobbyInventoryController _lobbyInventoryController;
    private float _spawnForce = 7.5f;
    private float _spawnDelay = 0.2f;
    private Animator _animator;
    private bool _isOpened = false;

    void Start()
    {
        if (!Regex.Match(this.name, @"^Chest \((\d+)\)$").Success)
        {
            Debug.LogWarning("Chest name " + this.name + " incorrect format!");
        }

            _animator = GetComponent<Animator>();
        _lobbyInventoryController = GameObject.FindGameObjectWithTag("Inventory").GetComponent<LobbyInventoryController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_isOpened && collision.gameObject.CompareTag("Player"))
        {
            _isOpened = true;
            //_animator.SetTrigger("Open");
            GetComponent<SpriteRenderer>().color = (Color)(new Color32(255, 0, 0, 255));

            SaveLoadManager.Instance.Progress.LevelConfig.ChestsOpenedIndexes.Add(
                int.Parse(Regex.Match(this.name, @"\((\d+)\)").Groups[1].Value)
            );

            StartCoroutine(SpawnContent());
        }
    }

    public void SetStateToOpen()
    {
        _isOpened = true;
        //animator.Play("Opened", 0, 1f); pøeskoèí pøehrávání animace
        GetComponent<SpriteRenderer>().color = (Color)(new Color32(255, 0, 0, 255));
    }

    IEnumerator SpawnContent()
    {
        foreach (var item in ChestContent)
        {
            {
                GameObject spawnedItem = Instantiate(item, transform.position, Quaternion.identity);
                Rigidbody2D rb = spawnedItem.GetComponent<Rigidbody2D>();
                PickItemController pickItemController = spawnedItem.GetComponentInParent<PickItemController>();
                pickItemController.LobbyInventoryController = _lobbyInventoryController;
                float randomForceX = Random.Range(-2f, 2f);
                rb.AddForce(new Vector2(randomForceX, _spawnForce), ForceMode2D.Impulse);
                yield return new WaitForSeconds(_spawnDelay);
            }
        }
        _isOpened = true;
    }
}
