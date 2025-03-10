using UnityEngine;

public class CheckpointController : MonoBehaviour
{


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            SaveLoadManager.Instance.Progress.LevelConfig.SpawnPoint = this.transform.position;

            SaveLoadManager.Instance.Save(SaveLoadManager.SaveType.Progress);
        }
    }
}
