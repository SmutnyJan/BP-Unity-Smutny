using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    private bool _isActive = false;
    private SpriteRenderer _spriteRenderer;
    public static CheckpointController LastCheckpoint;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetFlagActive(bool active)
    {
        if (active)
        {
            _spriteRenderer.color = (Color)(new Color32(0, 255, 0, 255));
            _isActive = true;
            return;
        }
        _spriteRenderer.color = (Color)(new Color32(255, 255, 255, 255));
        _isActive = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !_isActive)
        {
            if (LastCheckpoint)
            {
                LastCheckpoint.SetFlagActive(false);
            }
            else
            {
                LastCheckpoint = this;
            }
            this.SetFlagActive(true);
            LastCheckpoint = this;
            SaveLoadManager.Instance.Progress.LevelConfig.SpawnPoint = this.transform.position;
            SaveLoadManager.Instance.Save(SaveLoadManager.SaveType.Progress);
        }
    }
}
