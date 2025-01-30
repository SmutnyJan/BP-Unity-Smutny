using UnityEngine;

public class CameraFollowPlayerLobbyController : MonoBehaviour
{
    public Transform player; // Odkaz na hr��e
    public float minX, maxX; // Limity pro kameru na ose X

    private float fixedY; // Pevn� v��ka kamery

    void Start()
    {
        fixedY = transform.position.y; // Ulo�en� pevn� Y pozice
    }

    void LateUpdate()
    {
        // Z�sk�n� X pozice hr��e
        float targetX = player.position.x;

        // Omez�me X tak, aby bylo mezi minX a maxX
        targetX = Mathf.Clamp(targetX, minX, maxX);

        // Nastaven� nov� pozice kamery
        transform.position = new Vector3(targetX, fixedY, transform.position.z);
    }
}
