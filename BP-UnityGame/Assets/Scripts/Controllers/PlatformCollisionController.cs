using UnityEngine;

public class PlatformCollisionController : MonoBehaviour
{
    public bool IsGrounded = false;
    public bool IsOnPlatform = false;
    public GameObject TouchingPlatform = null;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        IsGrounded = true;
        
        if(collision.tag == "Platform")
        {
            IsOnPlatform = true;
            TouchingPlatform = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IsGrounded = false;
        IsOnPlatform = false;
        TouchingPlatform = null;
    }
}

