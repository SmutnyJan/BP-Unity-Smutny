using UnityEngine;

public class CameraFollowPlayerLobbyController : MonoBehaviour
{
    public Transform player;

    private float fixedY;

    void Start()
    {
        fixedY = transform.position.y;
    }

    void LateUpdate()
    {
        transform.position = new Vector3(player.position.x, fixedY, transform.position.z);
    }
}
