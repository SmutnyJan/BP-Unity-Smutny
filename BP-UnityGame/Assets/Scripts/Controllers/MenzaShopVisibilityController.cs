using UnityEngine;

public class MenzaShopVisibilityController : MonoBehaviour
{
    public GameObject MenzaShopCanvas;
    void Start()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        MenzaShopCanvas.SetActive(true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        MenzaShopCanvas.SetActive(false);
        SaveLoadManager.Instance.Save(SaveLoadManager.SaveType.Progress);

    }
}
