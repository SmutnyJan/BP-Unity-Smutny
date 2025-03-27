using UnityEngine;

public class CameraFollowPlayerLobbyController : MonoBehaviour
{
    public Transform player;

    public Transform boundaryBottomLeft;
    public Transform boundaryTopRight;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        float camHalfHeight = cam.orthographicSize;
        float camHalfWidth = camHalfHeight * cam.aspect;

        float minX = boundaryBottomLeft.position.x + camHalfWidth;
        float maxX = boundaryTopRight.position.x - camHalfWidth;
        float minY = boundaryBottomLeft.position.y + camHalfHeight;
        float maxY = boundaryTopRight.position.y - camHalfHeight;

        float clampedX = Mathf.Clamp(player.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(player.position.y, minY, maxY);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
