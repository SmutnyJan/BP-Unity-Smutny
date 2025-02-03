using UnityEngine;

public class LobbyColliderController : MonoBehaviour
{
    [HideInInspector]
    public SceneLoaderManager.ActiveScene LoadToScene;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SceneLoaderManager.Instance.LoadScene(LoadToScene);
    }
}
