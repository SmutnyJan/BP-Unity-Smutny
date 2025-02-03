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
        if(SceneLoaderManager.Instance.CurrentScene == SceneLoaderManager.ActiveScene.LobbyMenza && SaveLoadManager.Instance.Progress.GameState == SaveLoadManager.GameState.Beggining)
        {
            ShowMessages(new string[] { "Ještì jsem se nepøedstavil, jmenuji se -jmeno- a budu Tvým prùvodcem.",
            "Poøádnì ses prospal, ale teï je naèase odhodit splín stranou a pustit se do práce. Nejdøíve se stav na budovì G. Èervená šipka ti ukáže cestu."});
        }
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
