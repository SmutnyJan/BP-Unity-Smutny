using UnityEngine;

public class CameraFollowPlayerLobbyController : MonoBehaviour
{
    public Transform player; // Odkaz na hráèe
    public float minX, maxX; // Limity pro kameru na ose X

    private float fixedY; // Pevná výška kamery

    void Start()
    {
        fixedY = transform.position.y; // Uložení pevné Y pozice
    }

    void LateUpdate()
    {
        // Získání X pozice hráèe
        float targetX = player.position.x;

        // Omezíme X tak, aby bylo mezi minX a maxX
        targetX = Mathf.Clamp(targetX, minX, maxX);

        // Nastavení nové pozice kamery
        transform.position = new Vector3(targetX, fixedY, transform.position.z);
    }
}
