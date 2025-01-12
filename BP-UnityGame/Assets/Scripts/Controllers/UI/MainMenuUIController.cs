using System.Collections;
using UnityEngine;

public class MainMenuUIController : MonoBehaviour
{
    void Start()
    {
    }

    public void OnNewGameButtonPressed()
    {
        StartCoroutine(LoadWithAnimationCoroutine(SceneLoaderManager.Scene.Intro));
    }

    private IEnumerator LoadWithAnimationCoroutine(SceneLoaderManager.Scene scene)
    {
        SceneLoaderManager.Instance.SceneTransitionAnimator.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        SceneLoaderManager.Instance.LoadScene(scene);
    }

    public void OnLoadGameButtonPressed()
    {

    }

    public void OnSettingsButtonPressed()
    {

    }

    public void OnQuitGameButtonPressed()
    {
        #if UNITY_STANDALONE
                Application.Quit();
        #endif
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
