using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuController : MonoBehaviour
{
    public Slider SFXVolumeSlider;
    public Slider MusicVolumeSlider;
    public Toggle IsFullScreenToggle;
    public Toggle IsFPSShowToggle;
    public Toggle VSyncToggle;
    public Button SaveButton;
    public Button BackButton;
    public MicrophoneUIController MicrophoneUIController;


    void Start()
    {
        SFXVolumeSlider.value = SaveLoadManager.Instance.Settings.SFXVolume;
        MusicVolumeSlider.value = SaveLoadManager.Instance.Settings.MusicVolume;
        IsFullScreenToggle.isOn = SaveLoadManager.Instance.Settings.IsFullScreen;
        IsFPSShowToggle.isOn = SaveLoadManager.Instance.Settings.IsShowingFPS;
        VSyncToggle.isOn = SaveLoadManager.Instance.Settings.VSync;


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBackButtonPressed()
    {
        AudioManager.Instance.PlayClipByName("UI_Button_Click_1", AudioManager.Instance.AudioLibrary.UI, AudioManager.Instance.SFXAudioSource);

        SaveLoadManager.Instance.InvokeLoadEvent(SaveLoadManager.SaveType.Settings);

        SceneLoaderManager.Instance.LoadScene(SceneLoaderManager.ActiveScene.MainMenu);

    }

    public void OnSaveButtonPressed()
    {
        AudioManager.Instance.PlayClipByName("UI_Button_Click_1", AudioManager.Instance.AudioLibrary.UI, AudioManager.Instance.SFXAudioSource);
        SaveLoadManager.Instance.Settings.SFXVolume = SFXVolumeSlider.value;
        SaveLoadManager.Instance.Settings.MusicVolume = MusicVolumeSlider.value;
        SaveLoadManager.Instance.Settings.IsFullScreen = IsFullScreenToggle.isOn;
        SaveLoadManager.Instance.Settings.VSync = VSyncToggle.isOn;
        SaveLoadManager.Instance.Settings.IsShowingFPS = IsFPSShowToggle.isOn;
        SaveLoadManager.Instance.Settings.Microphone = MicrophoneUIController.SelectedMicrophone;

        MicrophoneManager.Instance.ChangeActiveMicrophone(MicrophoneUIController.SelectedMicrophone);

        SaveLoadManager.Instance.Save(SaveLoadManager.SaveType.Settings);
    }
    public void OnSFXVolumeSliderChanged()
    {
        AudioManager.Instance.SFXVolume = SFXVolumeSlider.value;

    }

    public void OnMusicVolumeSliderChanged()
    {
        AudioManager.Instance.MusicVolume = MusicVolumeSlider.value;
    }

    public void OnToggleFullScreenClick()
    {
        Screen.fullScreen = IsFullScreenToggle.isOn;
    }

    public void OnToggleFPSShowClick()
    {
        FPSDisplayController.Instance.ToggleShow(IsFPSShowToggle.isOn);
    }

    public void OnToggleVSyncClick()
    {
        QualitySettings.vSyncCount = VSyncToggle.isOn ? 1 : 0;
    }

}
