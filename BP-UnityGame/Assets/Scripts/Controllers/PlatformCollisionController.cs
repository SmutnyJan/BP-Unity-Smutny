using UnityEngine;

public class PlatformCollisionController : MonoBehaviour
{
    public bool IsGrounded = false;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        IsGrounded = true;
        Debug.Log("True");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IsGrounded = false;
        Debug.Log("False");
    }
}

