using UnityEngine;

public class PlatformCollisionController : MonoBehaviour
{
    public bool IsGrounded = false;
    public bool IsOnPlatform = false;
    public bool IsTouchingMovingPlatform = false;
    public GameObject TouchingPlatform = null;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IsGrounded = true;

        if (collision.gameObject.tag == "Platform")
        {
            IsOnPlatform = true;
            TouchingPlatform = collision.gameObject;
        }

        if (collision.gameObject.name == "Oneway Moving Platform")
        {

            this.transform.parent.SetParent(collision.gameObject.transform, true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        IsGrounded = false;
        IsOnPlatform = false;
        TouchingPlatform = null;

        if (collision.gameObject.name == "Oneway Moving Platform")
        {
            this.transform.parent.SetParent(null, true);
        }
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Groundcheck"))
        {

            collision.transform.parent = this.transform;


        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Groundcheck"))
        {
            collision.transform.parent = null;
        }
    }*/

}

