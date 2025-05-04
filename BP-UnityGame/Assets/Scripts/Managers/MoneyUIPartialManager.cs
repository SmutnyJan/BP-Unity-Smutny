using TMPro;
using UnityEngine;

public class MoneyUIPartialManager : MonoBehaviour
{
    public TextMeshProUGUI MoneyText;

    void Start()
    {
        MoneyText.text = SaveLoadManager.Instance.Progress.Money.ToString();
    }
}
