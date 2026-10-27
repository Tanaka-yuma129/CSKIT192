using UnityEngine;
public class CameraMove : MonoBehaviour
{
    public float moveSpeed = 10f;
    public SelectionMenu selectionMenu;
    public Transform backgroundRoot;
    private Camera cam;
    void Start()
    {
        cam = GetComponent<Camera>();
    }
    void Update()
    {
        if (selectionMenu == null ||
            !selectionMenu.IsFlightScreen)
        {
            return;
        }
        if (backgroundRoot == null || cam == null)
            return;
        float move = 0f;
        if (Input.GetKey(KeyCode.A))
            move = -1f;
        if (Input.GetKey(KeyCode.D))
            move = 1f;
        // ”wŒi‘S‘Ì‚Ì¶‰E’[‚ğæ“¾
        SpriteRenderer[] sprites =
            backgroundRoot.GetComponentsInChildren<SpriteRenderer>(true);
        if (sprites.Length == 0)
            return;
        Bounds bounds = sprites[0].bounds;
        foreach (SpriteRenderer sprite in sprites)
        {
            bounds.Encapsulate(sprite.bounds);
        }
        // ƒJƒƒ‰‚Ì‰¡•‚ğl—¶‚·‚é
        float halfWidth = cam.orthographicSize * cam.aspect;
        float minX = bounds.min.x + halfWidth;
        float maxX = bounds.max.x - halfWidth;
        if (minX > maxX)
            return;
        // ˆÚ“®æ‚ğ”wŒi‚Ì”ÍˆÍ“à‚É§ŒÀ
        float nextX = transform.position.x
            + move * moveSpeed * Time.deltaTime;
        nextX = Mathf.Clamp(nextX, minX, maxX);
        transform.position = new Vector3(
            nextX,
            transform.position.y,
            transform.position.z
        );
    }
}