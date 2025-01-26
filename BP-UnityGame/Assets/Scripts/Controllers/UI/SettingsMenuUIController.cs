using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuController : MonoBehaviour
{
    public Slider SFXVolumeSlider;
    public Slider MusicVolumeSlider;
    public Button SaveButton;
    public Button BackButton;


    void Start()
    {
        SFXVolumeSlider.value = SaveLoadManager.Instance.Settings.SFXVolume;
        MusicVolumeSlider.value = SaveLoadManager.Instance.Settings.MusicVolume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBackButtonPressed()
    {
        AudioManager.Instance.PlayClipByName("UI_Button_Click_1", AudioManager.Instance.AudioLibrary.UI, AudioManager.Instance.SFXAudioSource);
        SceneLoaderManager.Instance.LoadScene(SceneLoaderManager.ActiveScene.MainMenu);
    }

    public void OnSaveButtonPressed()
    {
        SaveLoadManager.Instance.Save(SaveLoadManager.SaveType.Settings);
    }
    public void OnSFXVolumeSliderChanged()
    {
        AudioManager.Instance.MusicVolume = AudioManager.Instance.AudioMixer.ConvertToDecibelValue(MusicVolumeSlider.value);
    }

    public void OnMusicVolumeSliderChanged()
    {

        AudioManager.Instance.SFXVolume = AudioManager.Instance.AudioMixer.ConvertToDecibelValue(SFXVolumeSlider.value);
    }

}
