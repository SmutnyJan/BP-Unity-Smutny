using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class MicrophoneUIController : MonoBehaviour
{
    public TMP_Dropdown MicrophoneDropdown;
    public string SelectedMicrophone;


    void Start()
    {
        PopulateMicrophoneDropdown();
    }

    void PopulateMicrophoneDropdown()
    {
        MicrophoneDropdown.ClearOptions();
        var micOptions = Microphone.devices.ToList();

        if (micOptions.Count > 0)
        {
            MicrophoneDropdown.AddOptions(micOptions);

            if (SaveLoadManager.Instance.Settings.Microphone != "")
            {
                var activeMic = SaveLoadManager.Instance.Settings.Microphone;

                int activeIndex = MicrophoneDropdown.options.FindIndex(option => option.text == activeMic);

                MicrophoneDropdown.value = activeIndex;
            }
            else
            {
                SelectedMicrophone = micOptions[0];
            }
        }
        else
        {
            Debug.LogWarning("Žádný mikrofon nebyl nalezen!");
        }

        MicrophoneDropdown.onValueChanged.AddListener(delegate { ChangeMicrophone(); });
    }

    void ChangeMicrophone()
    {
        SelectedMicrophone = MicrophoneDropdown.options[MicrophoneDropdown.value].text;
    }





}
