using System.Linq;
using UnityEngine;

public class ShowDialogInLevelController : MonoBehaviour
{
    public bool HasBeenTriggered = false;

    [TextArea]
    public string[] Messages;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
