using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using static SceneLoaderManager;

public class TestUIController : MonoBehaviour
{
    public TextMeshProUGUI DateText;
    public PlayableDirector director;

    void Start()
    {
        DateText.text = "Datum: " + DateTime.Now.ToString("dd. MM. yyyy");
        TimelineAsset timelineAsset = (TimelineAsset)director.playableAsset;
        director.SetGenericBinding(timelineAsset.GetOutputTracks().Where(x => x is AudioTrack).First(), AudioManager.Instance.SFXAudioSource);
        AudioManager.Instance.PlayClipByName("Clock Ticking", AudioManager.Instance.AudioLibrary.Music, AudioManager.Instance.MusicAudioSource, AudioManager.PlayType.Play);
    }

    private void OnEnable()
    {
        director.stopped += OnTimelineEnd;
    }

    public void PlayCheckTest()
    {
        director.Play();
    }

    void OnTimelineEnd(PlayableDirector pd)
    {
        AudioManager.Instance.MusicAudioSource.Stop();
        SceneLoaderManager.Instance.LoadScene(ActiveScene.PostTest);
    }

    private void OnDisable()
    {
        director.stopped -= OnTimelineEnd;
    }


}
