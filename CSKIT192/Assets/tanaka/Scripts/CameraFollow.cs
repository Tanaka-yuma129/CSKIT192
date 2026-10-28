using UnityEngine;
public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public PlayerFlight playerFlight;
    public float smoothSpeed = 5f;
    public Vector3 offset = new Vector3(0f, 0f, -10f);
    [Header("îwåiÇÃà⁄ìÆêßå¿")]
    public Transform backgroundRoot;
    private Vector3 startPosition;
    private Camera cam;
    [Header("É}ÉEÉXÉzÉCÅ[ÉãÇ≈ÉYÅ[ÉÄ")]
    public float zoomSpeed = 2f;
    public float minZoom = 3f;
    public float maxZoom = 15f;
    void Update()
    {
        float scroll = Input.mouseScrollDelta.y;
        if (scroll == 0f)
            return;
        Camera cam = GetComponent<Camera>();
        if (cam == null)
           return;
        cam.orthographicSize -= scroll * zoomSpeed;
        cam.orthographicSize = Mathf.Clamp(
           cam.orthographicSize,
            minZoom,
            maxZoom
        );

    }

    void Start()
    {
        startPosition = transform.position;
        cam = GetComponent<Camera>();
    }
    void LateUpdate()
    {
        if (playerFlight == null || !playerFlight.IsLaunched)
            return;
        if (target == null || cam == null || backgroundRoot == null)
            return;
        Vector3 targetPosition = target.position + offset;
        // îwåiëSëÃÇÃç∂âEí[ÇéÊìæ
        SpriteRenderer[] sprites =
            backgroundRoot.GetComponentsInChildren<SpriteRenderer>(true);
        if (sprites.Length == 0)
            return;
        Bounds bounds = sprites[0].bounds;
        foreach (SpriteRenderer sprite in sprites)
        {
            bounds.Encapsulate(sprite.bounds);
        }
        // ÉJÉÅÉâÇÃâ°ïùÇçló∂ÇµÇƒÅAîwåiÇÃäOÇâfÇ≥Ç»Ç¢
        float halfWidth = cam.orthographicSize * cam.aspect;
        float minX = bounds.min.x + halfWidth;
        float maxX = bounds.max.x - halfWidth;
        if (minX > maxX)
            targetPosition.x = bounds.center.x;
        else
            targetPosition.x = Mathf.Clamp(
                targetPosition.x, minX, maxX
            );
        // ècï˚å¸ÇÕç°Ç‹Ç≈í ÇËí«è]
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
        );
        // í«è]íÜÇ‡ç∂âEÇÃîÕàÕÇ©ÇÁèoÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
        Vector3 pos = transform.position;
        if (minX > maxX)
            pos.x = bounds.center.x;
        else
            pos.x = Mathf.Clamp(pos.x, minX, maxX);
        transform.position = pos;
    }
    public void ResetCamera()
    {
        if (target == null)
            return;
        transform.position = new Vector3(
            target.position.x + offset.x,
            target.position.y + offset.y,
            offset.z
        );
        startPosition = transform.position;
    }
}