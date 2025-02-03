using UnityEngine;

public class LobbyExitColliderController : MonoBehaviour
{
    public SceneLoaderManager.ActiveScene LoadToScene;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        SceneLoaderManager.Instance.LoadScene(LoadToScene);
    }
}
