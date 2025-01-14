using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using static SceneLoaderManager;

public class IntroController : MonoBehaviour
{
    public PlayableDirector director;
    private GameInputSystem _inputSystem;

    private void Awake()
    {
        _inputSystem = new GameInputSystem();
    }

    private void OnEnable()
    {
        _inputSystem.Cutscene.Enable();
        director.stopped += OnTimelineEnd;

    }

    public void OnSkip()
    {
        director.Stop();

    }
   

    void OnTimelineEnd(PlayableDirector pd)
    {
        SceneLoaderManager.Instance.LoadScene(ActiveScene.Lobby);
    }

    private void OnDisable()
    {
        _inputSystem.Cutscene.Disable();
        director.stopped -= OnTimelineEnd;
    }
}
