using UnityEngine;

public class CameraFollowPlayerLobbyController : MonoBehaviour
{
    public Transform Player;

    public Transform BoundaryBottomLeft;
    public Transform BoundaryTopRight;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        float camHalfHeight = cam.orthographicSize;
        float camHalfWidth = camHalfHeight * cam.aspect;

        float minX = BoundaryBottomLeft.position.x + camHalfWidth;
        float maxX = BoundaryTopRight.position.x - camHalfWidth;
        float minY = BoundaryBottomLeft.position.y + camHalfHeight;
        float maxY = BoundaryTopRight.position.y - camHalfHeight;

        float clampedX = Mathf.Clamp(Player.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(Player.position.y, minY, maxY);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
