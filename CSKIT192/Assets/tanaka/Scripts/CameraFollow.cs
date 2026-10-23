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
        // 配達員が発射された後だけ追いかける
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
        if (target == null)
            return;
        // 主人公の位置をカメラの中心にする
        transform.position = new Vector3(
            target.position.x + offset.x,
            target.position.y + offset.y,
            offset.z
        );
        // カメラの開始位置も更新する
        startPosition = transform.position;
    }
}