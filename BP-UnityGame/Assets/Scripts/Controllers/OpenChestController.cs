using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

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
            SetStateToOpen();
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
        List<GameObject> itemsToSpawn = new List<GameObject>(ChestContent);

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
            {
                GameObject spawnedItem = Instantiate(item, transform.position, Quaternion.identity);
                Rigidbody2D rb = spawnedItem.GetComponent<Rigidbody2D>();

                PickItemController pickItemController = spawnedItem.GetComponentInParent<PickItemController>();
                if(pickItemController) //peníze PickItemController nemají
                {
                    pickItemController.LobbyInventoryController = _lobbyInventoryController;
                }


                float randomForceX = Random.Range(-2f, 2f);
                rb.AddForce(new Vector2(randomForceX, _spawnForce), ForceMode2D.Impulse);
                yield return new WaitForSeconds(_spawnDelay);
            }
        }
    }

    private List<GameObject> GetRandomSubset(List<GameObject> source, int count)
    {
        List<GameObject> temp = new List<GameObject>(source);
        List<GameObject> result = new List<GameObject>();
        for (int i = 0; i < count && temp.Count > 0; i++)
        {
            int index = Random.Range(0, temp.Count);
            result.Add(temp[index]);
            temp.RemoveAt(index);
        }
        return result;
    }
}
