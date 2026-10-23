using UnityEngine;
using TMPro;

public class ChipManager : MonoBehaviour
{
    public static ChipManager Instance { get; private set; }

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI chipText;

    private int currentChip = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateUI();
    }

    public void AddChips(int amount)
    {
        currentChip += amount;

        if (currentChip < 0)
        {
            currentChip = 0;
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (chipText != null)
        {
            chipText.text = "Chips: " + currentChip.ToString();
        }
    }
}
