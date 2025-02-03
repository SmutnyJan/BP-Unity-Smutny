using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TipsController : MonoBehaviour
{
    [HideInInspector]
    public static TipsController Instance;
    public GameObject MessagesPanel;
    public TextMeshProUGUI CloseContinueButtonText;
    public TextMeshProUGUI TipsText;

    private Stack<string> messageBuffer;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnCloseContinueButtonClicked()
    {
        if (messageBuffer.Any())
        {
            TipsText.text = messageBuffer.Pop();
            if (!messageBuffer.Any())
            {
                CloseContinueButtonText.text = "X";
            }
        }
        else
        {
            MessagesPanel.SetActive(false);
        }



    }

    public void ShowMessages(string[] messages)
    {
        MessagesPanel.SetActive(true);
        messageBuffer = new Stack<string>(messages.Reverse());
        TipsText.text = messageBuffer.Pop();
        if(messageBuffer.Any())
        {
            CloseContinueButtonText.text = ">";
        }
        else
        {
            CloseContinueButtonText.text = "X";
        }

    }


}
