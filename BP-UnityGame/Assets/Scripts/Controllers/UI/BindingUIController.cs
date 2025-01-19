using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BindingUIController : MonoBehaviour
{
    public TextMeshProUGUI hiText;
    public TextMeshProUGUI howAreYouText;

    public Button hiButton;
    public Button howAreYouButton;

    private DemoInputActions inputActions;

    private void Awake()
    {
        inputActions = new DemoInputActions();

        // Pøihlášení k událostem
        inputActions.Demo.Hi.performed += ctx => OnHi();
        inputActions.Demo.HowAreYou.performed += ctx => OnHowAreYou();
    }

    private void OnEnable()
    {
        inputActions.Enable(); // Aktivace akcí
    }

    private void OnDisable()
    {
        inputActions.Disable(); // Deaktivace akcí
    }

    private void Start()
    {
        // Nastavení textù vazeb
        SetBindingsText();

        // Pøidání posluchaèù tlaèítek
        hiButton.onClick.AddListener(() => StartRebinding(inputActions.Demo.Hi, hiText));
        howAreYouButton.onClick.AddListener(() => StartRebinding(inputActions.Demo.HowAreYou, howAreYouText));
    }

    private void SetBindingsText()
    {
        // Nastavení textù pro akci "Hi"
        var hiAction = inputActions.Demo.Hi;
        if (hiAction != null)
        {
            hiText.text = GetSimplifiedBindings(hiAction);
        }

        // Nastavení textù pro akci "HowAreYou"
        var howAreYouAction = inputActions.Demo.HowAreYou;
        if (howAreYouAction != null)
        {
            howAreYouText.text = GetSimplifiedBindings(howAreYouAction);
        }
    }

    private string GetSimplifiedBindings(InputAction action)
    {
        // Vrací text reprezentující první aktivní binding
        foreach (var binding in action.bindings)
        {
            return InputControlPath.ToHumanReadableString(binding.path, InputControlPath.HumanReadableStringOptions.OmitDevice);
        }
        return "";
    }

    private void StartRebinding(InputAction action, TextMeshProUGUI text)
    {
        text.text = "Press key...";

        for (int i = 0; i < action.bindings.Count; i++)
        {
            action.ApplyBindingOverride(i, string.Empty);
        }

        action.Disable(); // Doèasnì zakážeme akci

        action.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse") // Napø. vylouèíme myš
            .OnComplete(operation =>
            {
                // Pøidání nové vazby
                text.text = InputControlPath.ToHumanReadableString(
                    operation.selectedControl.path,
                    InputControlPath.HumanReadableStringOptions.OmitDevice
                );

                operation.Dispose();
                action.Enable(); // Znovu povolíme akci
            })
            .Start();
    }



    public void OnHi()
    {
        Debug.Log("Hi!");
    }

    public void OnHowAreYou()
    {
        Debug.Log("How are you?");
    }
}
