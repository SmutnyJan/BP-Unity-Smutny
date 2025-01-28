using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TestUIController : MonoBehaviour
{
    public TextMeshProUGUI DateText;
    public PlayableDirector director;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DateText.text = "Datum: " + DateTime.Now.ToString("dd. MM. yyyy");
        /*var timelineAsset = (TimelineAsset)director.playableAsset;
        director.SetGenericBinding(timelineAsset.GetOutputTracks().Where(x => x is AudioTrack).First(), AudioManager.Instance.SFXAudioSource);*/
    }

    public void PlayCheckTest()
    {
        director.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
