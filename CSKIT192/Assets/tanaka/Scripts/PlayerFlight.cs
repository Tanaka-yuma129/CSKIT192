using UnityEngine;
using TMPro;
public class PlayerFlight : MonoBehaviour
{
    public float speed = 20f;
    public PowerGauge powerGauge;
    public float rotateSpeed = 60f;
    public float gravity = 3f;
    public LineRenderer aimArrow;

    public float arrowLength = 2f;

    public float arrowHeadLength = 0.4f;

    public float arrowHeadAngle = 25f;

    public float angleChangeSpeed = 0.5f;

    public TMP_Text successText;
    public GameObject flyButton;
    public GameObject powerGaugePanel;
    public GameObject packagePrefab;
    public SelectionMenu selectionMenu;

    private bool packageDropped = false;

    private Rigidbody2D rb;
    private bool launched = false;
    private bool landed = false;
    private bool angleLocked = false;
    private bool canAim = false;

    public bool IsLaunched => launched || landed;
    public bool IsFlying => launched;
    private float angle = 45f;
    private float angleDirection = 1f;
    private Vector3 startPosition;
    private Quaternion checkpointRotation;
    private GameObject currentPlanet;
    public TMP_Text scoreText;
    public int scorePerLanding = 100;
    private int score = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        checkpointRotation = transform.rotation;
        canAim = false;
        rb.gravityScale = 0f;
        rb.linearDamping = 0f;
        rb.angularVelocity = 0f;
        rb.linearVelocity = Vector2.zero;
        if (successText != null)
            successText.gameObject.SetActive(false);
        if (flyButton != null)
            flyButton.SetActive(false);
        if (powerGaugePanel != null)
            powerGaugePanel.SetActive(false);
        if (aimArrow != null)
        {
            aimArrow.positionCount = 4;
            aimArrow.enabled = false;
        }

    }
    void Update()
    {
        UpdateAimArrow();
        if (!canAim) return;
        if (!angleLocked)
        {
            angle += angleDirection * rotateSpeed * Time.deltaTime;
            if (angle >= 155f)
            {
                angle = 155f;
                angleDirection = -1f;

            }
            else if (angle <= 30f)
            {
                angle = 30f;
                angleDirection = 1f;

            }

            transform.rotation =
                Quaternion.Euler(0f, 0f, angle);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 飛行中なら配達物を落とす
            if (launched)
            {
                return;
            }
            if (!angleLocked)
            {
                // 角度を固定
                angleLocked = true;
                // パワーゲージをリセットして表示
                if (powerGauge != null)
                    powerGauge.ResetGauge();
                if (powerGaugePanel != null)
                    powerGaugePanel.SetActive(true);
                return;
            }
            // 発射
            launched = true;
            if (powerGaugePanel != null)
                powerGaugePanel.SetActive(false);
            landed = false;
            if (successText != null)
                successText.gameObject.SetActive(false);
            float radians = angle * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(
                Mathf.Cos(radians),
                Mathf.Sin(radians)
            );
            rb.gravityScale = gravity;
            float power = powerGauge != null
                ? powerGauge.LockPower()
                : speed;
            rb.linearVelocity = direction * power * 1.5f;
            if (selectionMenu != null)
                selectionMenu.HideReturnButton();
        }
    }
    void UpdateAimArrow()
    {
        if (aimArrow == null) return;
        if (!canAim || launched )
        {
            aimArrow.enabled = false;
            return;
        }
        aimArrow.enabled = true;
        Vector3 start = transform.position;
        float radians = angle * Mathf.Deg2Rad;
        Vector2 direction = new Vector2(
            Mathf.Cos(radians),
            Mathf.Sin(radians)
        );
        Vector3 tip = start + (Vector3)(direction * arrowLength);
        float leftAngle = (angle + 180f - arrowHeadAngle)
            * Mathf.Deg2Rad;
        float rightAngle = (angle + 180f + arrowHeadAngle)
            * Mathf.Deg2Rad;
        Vector3 left = tip + new Vector3(
            Mathf.Cos(leftAngle),
            Mathf.Sin(leftAngle),
            0f
        ) * arrowHeadLength;
        Vector3 right = tip + new Vector3(
            Mathf.Cos(rightAngle),
            Mathf.Sin(rightAngle),
            0f
        ) * arrowHeadLength;
        aimArrow.positionCount = 4;
        aimArrow.SetPosition(0, start);
        aimArrow.SetPosition(1, tip);
        aimArrow.SetPosition(2, left);
        aimArrow.SetPosition(3, tip);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!launched) return;
        Debug.Log("衝突相手：" + collision.gameObject.name
   + " / Tag：" + collision.gameObject.tag);
        // 惑星以外との衝突は無視
        if (!collision.gameObject.CompareTag("Earth") &&
            !collision.gameObject.CompareTag("Planet"))
        {
            return;
        }
        // 直前に着地した惑星に当たったらリセット
        if (collision.gameObject == currentPlanet)
        {
            ReturnToLaunch();
            return;
        }
    
        // 新しい惑星への着地成功
        currentPlanet = collision.gameObject;
        launched = false;
        landed = true;
        angleLocked = false;
        // 今回着地した場所を次のリセット地点にする
        startPosition = transform.position;
        checkpointRotation = transform.rotation;
        // 配達員を停止
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.gravityScale = 0f;
        if (powerGauge != null)
            powerGauge.ResetGauge();
        if (successText != null)
            successText.gameObject.SetActive(true);
        if (flyButton != null)
            flyButton.SetActive(true);
        // 地球に着地したときだけ画面を戻す
        // 地球に着地したときだけ画面を戻す
        if (collision.gameObject.CompareTag("Earth"))
        {

            Debug.Log("地球に着地！地球画面に戻します");
            launched = false;
            landed = true;
            angleLocked = false;
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.gravityScale = 0f;
            if (powerGauge != null)
                powerGauge.ResetGauge();
            if (successText != null)
                successText.gameObject.SetActive(false);
            if (flyButton != null)
                flyButton.SetActive(false);
            if (selectionMenu != null)
                selectionMenu.ReturnToEarthScreen();
            return;
        }
  
    }

    void LateUpdate()

    {

        if (launched && transform.position.y < -10f)

        {

            ReturnToLaunch();
            // カメラも初期位置に戻す
            CameraFollow cameraFollow =
                Camera.main != null
                    ? Camera.main.GetComponent<CameraFollow>()
                    : null;
            if (cameraFollow != null)

            {
                cameraFollow.ResetCamera();
            }
        }
    }

    public void ReturnToLaunch()
    {
        launched = false;
        packageDropped = false;
        landed = false;
        angleLocked = false;
        transform.position = startPosition;
        transform.rotation = checkpointRotation;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.gravityScale = 0f;
        angle = 45f;
        angleDirection = 1f;
        if (powerGauge != null)
            powerGauge.ResetGauge();
        if (successText != null)
            successText.gameObject.SetActive(false);
        if (flyButton != null)
            flyButton.SetActive(false);
    }
    public void SetCanAim(bool value)
    {
        canAim = value;
        Debug.Log("角度操作：" + canAim);
    }

    public void DropPackage()
    {
        if (!launched || packageDropped)
            return;
        if (packagePrefab == null)
        {
            Debug.LogWarning("Package Prefabが設定されていません！");
            return;
        }
        Vector3 dropPosition = transform.position + Vector3.down * 0.5f;
        GameObject package = Instantiate(
            packagePrefab,
            dropPosition,
            Quaternion.identity
        );
        Rigidbody2D packageRb = package.GetComponent<Rigidbody2D>();
        if (packageRb != null)
        {
            packageRb.gravityScale = 3f;
            packageRb.linearVelocity = Vector2.zero;
        }
        Collider2D packageCollider = package.GetComponent<Collider2D>();
        Collider2D playerCollider = GetComponent<Collider2D>();
        if (packageCollider != null && playerCollider != null)
        {
            Physics2D.IgnoreCollision(packageCollider, playerCollider);
        }
        packageDropped = true;
    }
    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "スコア：" + score + "点";
        }

    }

}