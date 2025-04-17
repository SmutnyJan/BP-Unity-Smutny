using System;
using System.Linq;
using UnityEngine;
using TMPro;

public class MicrophoneManager : MonoBehaviour
{
    public static MicrophoneManager Instance { get; private set; }
    public float threshold = 0.25f;
    public event Action OnLoudNoiseDetected;


    private AudioClip _microphoneClip;
    private string _selectedMic;
    private const int _sampleWindow = 128;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    void Start()
    {
        if(SaveLoadManager.Instance.Settings.Microphone == "")
        {

            if(!Microphone.devices.Any())
            {
                return;
            }
            _selectedMic = Microphone.devices[0];
            StartMicrophone();
            SaveLoadManager.Instance.Settings.Microphone = _selectedMic;
            SaveLoadManager.Instance.Save(SaveLoadManager.SaveType.Settings);
        }
        else
        {
            _selectedMic = Microphone.devices.FirstOrDefault(x => x == SaveLoadManager.Instance.Settings.Microphone);
            if(_selectedMic == null)
            {
                return;
            }
            StartMicrophone();
        }
    }

    void StartMicrophone()
    {
        if (Microphone.IsRecording(_selectedMic))
        {
            Microphone.End(_selectedMic);
        }

        _microphoneClip = Microphone.Start(_selectedMic, true, 1, 44100);
        Debug.Log($"Spuštìno nahrávání z mikrofonu: {_selectedMic}");
    }

    void Update()
    {
        if (_microphoneClip != null)
        {
            float volume = GetMicrophoneLevel();
            if (volume > threshold)
            {
                Debug.Log("Detekován hlasitý zvuk!");
                OnLoudNoiseDetected?.Invoke();
            }
        }
    }

    float GetMicrophoneLevel()
    {
        float[] data = new float[_sampleWindow];
        int micPosition = Microphone.GetPosition(_selectedMic);
        if (micPosition < _sampleWindow) return 0;

        _microphoneClip.GetData(data, micPosition - _sampleWindow);
        return data.Max(Mathf.Abs);
    }

    public void ChangeActiveMicrophone(string microphoneName)
    {
        _selectedMic = microphoneName;
        StartMicrophone();
    }



}
