using UnityEngine;

public class PlatformCollisionController : MonoBehaviour
{
    public bool IsGrounded = false;
    public bool IsOnPlatform = false;
    public GameObject TouchingPlatform = null;
    public Animator Animator;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IsGrounded = true;
        Animator.SetBool("IsGrounded", true);
        Debug.Log("True IsGrounded");

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
        Animator.SetBool("IsGrounded", false);
        Debug.Log("false IsGrounded");

        if (collision.gameObject.name == "Oneway Moving Platform" && this.transform.parent.gameObject.activeInHierarchy)
        {

            this.transform.parent.SetParent(null, true);
        }
    }


}

