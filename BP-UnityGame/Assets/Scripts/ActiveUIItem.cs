using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActiveUIItem : MonoBehaviour
{
    public ItemType ItemType;

    public TextMeshProUGUI AmountText;
    public Image CooldownImage;

    private Sprite _UISprite;
    private Coroutine _cooldownCoroutine;

    void Start()
    {

    }

    void Update()
    {

    }

    public void StartCooldown(float duration)
    {
        if (duration < 0)
        {
            return;
        }

        if (_cooldownCoroutine != null)
        {
            StopCoroutine(_cooldownCoroutine);
        }

        _cooldownCoroutine = StartCoroutine(CooldownRoutine(duration));
    }

    public void ResetCoolDownUI()
    {
        if (_cooldownCoroutine != null)
        {
            StopCoroutine(_cooldownCoroutine);
            _cooldownCoroutine = null;
        }

        CooldownImage.fillAmount = 0f;
    }

    private IEnumerator CooldownRoutine(float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            CooldownImage.fillAmount = 1f - (elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        CooldownImage.fillAmount = 0f;
        _cooldownCoroutine = null;
    }

    public void LoadValues()
    {
        _UISprite = ItemLibraryManager.Instance.UIItems[ItemType].Icon;

        this.GetComponent<Image>().sprite = _UISprite;

        AmountText.text = SaveLoadManager.Instance.Progress.Items.First(x => x.ItemType == ItemType).Amount.ToString();

    }

    public void UpdateAmountValue(int amount)
    {
        AmountText.text = amount.ToString();
    }
}
