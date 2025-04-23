using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CheckpointController : MonoBehaviour
{
    private bool _isActive = false;
    public static CheckpointController LastCheckpoint;
    public Light2D LampLight;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetCheckpointActive(bool active)
    {
        if (active)
        {
            LampLight.color = (Color)(new Color32(0, 255, 0, 255));
            _isActive = true;
            return;
        }
        LampLight.color = (Color)(new Color32(255, 255, 255, 255));
        _isActive = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !_isActive)
        {
            if (LastCheckpoint)
            {
                LastCheckpoint.SetCheckpointActive(false);
            }
            else
            {
                LastCheckpoint = this;
            }
            this.SetCheckpointActive(true);
            LastCheckpoint = this;
            SaveLoadManager.Instance.Progress.LevelConfig.SpawnPoint = this.transform.position;
            SaveLoadManager.Instance.Save(SaveLoadManager.SaveType.Progress);
        }
    }
}
