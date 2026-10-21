using UnityEngine;
using TMPro;
public class PlanetArrowIndicator : MonoBehaviour
{
    public Transform targetPlanet;
    public Camera targetCamera;
    public float screenMargin = 60f;
    private RectTransform rectTransform;
    private TMP_Text arrowText;
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        arrowText = GetComponent<TMP_Text>();
    }
    void LateUpdate()
    {
        if (targetPlanet == null || targetCamera == null)
        {
            if (arrowText != null)
                arrowText.enabled = false;
            return;
        }
        Vector3 screenPos =
            targetCamera.WorldToScreenPoint(targetPlanet.position);
        Vector2 direction = new Vector2(
            screenPos.x - Screen.width / 2f,
            screenPos.y - Screen.height / 2f
        );
        if (screenPos.z < 0f)
            direction = -direction;
        bool isOnScreen =
            screenPos.z > 0f &&
            screenPos.x >= 0f &&
            screenPos.x <= Screen.width &&
            screenPos.y >= 0f &&
            screenPos.y <= Screen.height;
        // オブジェクト自体は無効にせず、文字だけ表示・非表示にする
        if (arrowText != null)
            arrowText.enabled = !isOnScreen;
        if (isOnScreen)
            return;
        if (direction.sqrMagnitude < 0.001f)
            direction = Vector2.up;
        direction.Normalize();
        float halfWidth = Mathf.Max(
            1f, Screen.width / 2f - screenMargin
        );
        float halfHeight = Mathf.Max(
            1f, Screen.height / 2f - screenMargin
        );
        float scale = Mathf.Min(
            halfWidth / Mathf.Max(Mathf.Abs(direction.x), 0.001f),
            halfHeight / Mathf.Max(Mathf.Abs(direction.y), 0.001f)
        );
        Vector2 position = new Vector2(
            Screen.width / 2f + direction.x * scale,
            Screen.height / 2f + direction.y * scale
        );
        rectTransform.position = position;
        float angle = Mathf.Atan2(
            direction.y, direction.x
        ) * Mathf.Rad2Deg;
        rectTransform.rotation =
            Quaternion.Euler(0f, 0f, angle);
    }
}