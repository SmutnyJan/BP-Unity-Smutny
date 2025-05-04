using UnityEngine;

public class LevelExitCollisionController : MonoBehaviour
{
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            LevelFlowManager.Instance.EndLevel();
        }
    }
}
