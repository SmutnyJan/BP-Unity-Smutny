using UnityEngine;

public class PlatformCollisionController : MonoBehaviour
{
    public bool IsGrounded = false;
    public bool IsOnPlatform = false;
    public GameObject TouchingPlatform = null;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IsGrounded = true;

        if (collision.gameObject.tag == "Platform")
        {
            IsOnPlatform = true;
            TouchingPlatform = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        IsGrounded = false;
        IsOnPlatform = false;
        TouchingPlatform = null;
    }
}

