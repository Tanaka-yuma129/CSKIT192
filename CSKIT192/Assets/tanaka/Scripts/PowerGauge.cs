using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PowerGauge : MonoBehaviour
{
    public Image powerBarFill;
    public TMP_Text powerText;
    public float minPower = 1f;
    public float maxPower = 60f;
    public float changeSpeed = 45f;
    private float currentPower = 1f;
    private float direction = 1f;
    private bool isRunning = false;
    private float maxWidth;
    void Start()
    {
        if (powerBarFill != null)
        {
            maxWidth = powerBarFill.rectTransform.sizeDelta.x;
        }
        UpdateDisplay();
    }
    void Update()
    {
        if (!isRunning) return;
        currentPower += direction * changeSpeed * Time.deltaTime;
        if (currentPower >= maxPower)
        {
            currentPower = maxPower;
            direction = -1f;
        }
        else if (currentPower <= minPower)
        {
            currentPower = minPower;
            direction = 1f;
        }
        UpdateDisplay();
    }
    public void ResetGauge()
    {
        currentPower = minPower;
        direction = 1f;
        isRunning = true;
        UpdateDisplay();
    }
    public float LockPower()
    {
        isRunning = false;
        UpdateDisplay();
        return currentPower;
    }
    public void StopGauge()
    {
        isRunning = false;
    }
    void UpdateDisplay()
    {
        if (powerBarFill != null)
        {
            float ratio = Mathf.InverseLerp(
                minPower, maxPower, currentPower
            );
            Vector2 size = powerBarFill.rectTransform.sizeDelta;
            size.x = maxWidth * ratio;
            powerBarFill.rectTransform.sizeDelta = size;
        }
        if (powerText != null)
        {
            powerText.text =
                Mathf.RoundToInt(currentPower).ToString();
        }
    }
}