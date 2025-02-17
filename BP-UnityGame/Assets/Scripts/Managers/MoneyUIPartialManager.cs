using TMPro;
using UnityEngine;

public class MoneyUIPartialManager : MonoBehaviour
{
    public TextMeshProUGUI MoneyText;
    public GameObject GameInputSystem;

    void Start()
    {
        MoneyText.text = SaveLoadManager.Instance.Progress.Money.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
