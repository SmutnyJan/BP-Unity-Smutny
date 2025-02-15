using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class IntroController : MonoBehaviour
{
    public PlayableDirector director;
    private PlayerInputSystem _inputSystem;
    public SceneLoaderManager.ActiveScene LoadAfterScene;


    private void Awake()
    {
        _inputSystem = new PlayerInputSystem();
    }

    void Start()
    {
        TimelineAsset timelineAsset = (TimelineAsset)director.playableAsset;
        director.SetGenericBinding(timelineAsset.GetOutputTracks().Where(x => x is AudioTrack).First(), AudioManager.Instance.MusicAudioSource);


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
        SceneLoaderManager.Instance.LoadScene(LoadAfterScene);
    }

    private void OnDisable()
    {
        _inputSystem.Cutscene.Disable();
        director.stopped -= OnTimelineEnd;
    }
}
