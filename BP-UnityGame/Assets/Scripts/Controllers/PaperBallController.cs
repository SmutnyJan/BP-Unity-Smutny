using UnityEngine;

public class PaperBallController : MonoBehaviour
{
    public int RecursiveLives;
    private bool hasCollided = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasCollided) return;
        hasCollided = true;

        if (RecursiveLives <= 0)
        {
            Destroy(gameObject);
            return;
        }

        GameObject obj1 = Instantiate(gameObject, transform.position + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
        PaperBallController paperball1 = obj1.GetComponent<PaperBallController>();
        paperball1.RecursiveLives = RecursiveLives - 1;
        obj1.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 1) * 10, ForceMode2D.Impulse);

        GameObject obj2 = Instantiate(gameObject, transform.position + new Vector3(-0.5f, 0.5f, 0), Quaternion.identity);
        PaperBallController paperball2 = obj2.GetComponent<PaperBallController>();
        paperball2.RecursiveLives = RecursiveLives - 1;
        obj2.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 1) * 10, ForceMode2D.Impulse);

        Destroy(gameObject);
    }
}
