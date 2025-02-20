using UnityEngine;

public class UsePencil : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _rigidBody.AddForce(new Vector2(1,1) * 10, ForceMode2D.Impulse);
        _rigidBody.AddTorque(-1, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
