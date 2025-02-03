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
            ShowMessages(new string[] { "Je�t� jsem se nep�edstavil, jmenuji se -jmeno- a budu Tv�m pr�vodcem.",
            "Po��dn� ses prospal, ale te� je na�ase odhodit spl�n stranou a pustit se do pr�ce. Nejd��ve se stav na budov� G. �erven� �ipka ti uk�e cestu."});
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
