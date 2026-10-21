using UnityEngine;
public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public PlayerFlight playerFlight;
    public float smoothSpeed = 5f;
    public Vector3 offset = new Vector3(0f, 0f, -10f);
    private Vector3 startPosition;
    void Start()
    {
        startPosition = transform.position;
    }
    void LateUpdate()
    {
        // ”z’Bˆõ‚ª”­ŽË‚³‚ê‚½Œã‚¾‚¯’Ç‚¢‚©‚¯‚é
        if (playerFlight == null || !playerFlight.IsLaunched)
            return;
        if (target == null)
            return;
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
        );
    }
    public void ResetCamera()
    {
        transform.position = startPosition;
    }
}