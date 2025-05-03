using System.Linq;
using UnityEngine;

public class ShowDialogInLevelController : MonoBehaviour
{
    public bool HasBeenTriggered = false;

    [TextArea]
    public string[] Messages;

    void Start()
    {

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Messages.Any() && collision.gameObject.tag == "Player" && !HasBeenTriggered)
        {
            TipsController.Instance.ShowMessages(Messages);
            HasBeenTriggered = true;
        }
    }
}
