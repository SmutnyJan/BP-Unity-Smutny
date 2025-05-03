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
        MicrophoneDropdown.onValueChanged.AddListener(delegate { ChangeMicrophone(); });

    }

    void PopulateMicrophoneDropdown()
    {
        MicrophoneDropdown.ClearOptions();
        System.Collections.Generic.List<string> micOptions = Microphone.devices.ToList();

        if (micOptions.Count > 0)
        {
            MicrophoneDropdown.AddOptions(micOptions);

            if (SaveLoadManager.Instance.Settings.Microphone != "")
            {
                string activeMic = SaveLoadManager.Instance.Settings.Microphone;
                SelectedMicrophone = activeMic;

                int activeIndex = MicrophoneDropdown.options.FindIndex(option => option.text == activeMic);

                MicrophoneDropdown.value = activeIndex;
            }
            else
            {
                SelectedMicrophone = micOptions[0];
            }
        }
    }

    void ChangeMicrophone()
    {
        SelectedMicrophone = MicrophoneDropdown.options[MicrophoneDropdown.value].text;
    }

}
