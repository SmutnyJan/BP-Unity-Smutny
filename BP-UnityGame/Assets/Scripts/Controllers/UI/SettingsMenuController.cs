using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuController : MonoBehaviour
{
    public Slider SFXVolumeSlider;
    public Slider MusicVolumeSlider;

    void Start()
    {
        SFXVolumeSlider.value = SaveLoadManager.Instance.Settings.SFXVolume;
        MusicVolumeSlider.value = SaveLoadManager.Instance.Settings.MusicVolume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
