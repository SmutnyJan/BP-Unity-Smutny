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

        // P�ihl�en� k ud�lostem
        inputActions.Demo.Hi.performed += ctx => OnHi();
        inputActions.Demo.HowAreYou.performed += ctx => OnHowAreYou();
    }

    private void OnEnable()
    {
        inputActions.Enable(); // Aktivace akc�
    }

    private void OnDisable()
    {
        inputActions.Disable(); // Deaktivace akc�
    }

    private void Start()
    {
        // Nastaven� text� vazeb
        SetBindingsText();

        // P�id�n� poslucha�� tla��tek
        hiButton.onClick.AddListener(() => StartRebinding(inputActions.Demo.Hi, hiText));
        howAreYouButton.onClick.AddListener(() => StartRebinding(inputActions.Demo.HowAreYou, howAreYouText));
    }

    private void SetBindingsText()
    {
        // Nastaven� text� pro akci "Hi"
        var hiAction = inputActions.Demo.Hi;
        if (hiAction != null)
        {
            hiText.text = GetSimplifiedBindings(hiAction);
        }

        // Nastaven� text� pro akci "HowAreYou"
        var howAreYouAction = inputActions.Demo.HowAreYou;
        if (howAreYouAction != null)
        {
            howAreYouText.text = GetSimplifiedBindings(howAreYouAction);
        }
    }

    private string GetSimplifiedBindings(InputAction action)
    {
        // Vrac� text reprezentuj�c� prvn� aktivn� binding
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

        action.Disable(); // Do�asn� zak�eme akci

        action.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse") // Nap�. vylou��me my�
            .OnComplete(operation =>
            {
                // P�id�n� nov� vazby
                text.text = InputControlPath.ToHumanReadableString(
                    operation.selectedControl.path,
                    InputControlPath.HumanReadableStringOptions.OmitDevice
                );

                operation.Dispose();
                action.Enable(); // Znovu povol�me akci
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
