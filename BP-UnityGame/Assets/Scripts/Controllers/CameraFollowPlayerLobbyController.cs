using UnityEngine;

public class CameraFollowPlayerLobbyController : MonoBehaviour
{
    public Transform player;
    public float minX, maxX;
    public float minY, maxY;

    void Start()
    {
    }

    void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(player.position.x, minX, maxX), Mathf.Clamp(player.position.y, minY, maxY), transform.position.z);
    }
}
