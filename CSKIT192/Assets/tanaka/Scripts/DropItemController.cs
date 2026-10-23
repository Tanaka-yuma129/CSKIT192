using UnityEngine;
using TMPro;
public class DropItemController : MonoBehaviour
{
    public PlayerFlight playerFlight;
    public DeliveryInventory inventory;
    public GameObject tomatoPrefab;
    public GameObject orangePrefab;
    public GameObject cabbagePrefab;
    public TMP_Text selectedItemText;
    public float dropDistance = 1.5f;
    public float dropSpeed = 3f;
    // 0 = トマト、1 = みかん、2 = キャベツ
    private int selectedIndex = 0;
    void Start()
    {
        UpdateSelectedItemText();
    }
    void Update()
    {
        // 配達員が飛行中だけ操作できる
        if (playerFlight == null || !playerFlight.IsLaunched)
            return;
        // Qキーで落とす野菜を切り替える
        if (Input.GetKeyDown(KeyCode.Q))
        {
            selectedIndex = (selectedIndex + 1) % 3;
            UpdateSelectedItemText();
        }
        // Spaceキーで選択中の野菜を落とす
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DropSelectedItem();
        }
    }
    void UpdateSelectedItemText()
    {
        if (selectedItemText == null)
            return;
        string itemName = GetSelectedItemName();
        selectedItemText.text = "落とす野菜：" + itemName + "（Qで変更）";
    }
    string GetSelectedItemName()
    {
        switch (selectedIndex)
        {
            case 0: return "トマト";
            case 1: return "みかん";
            default: return "キャベツ";
        }
    }
    void DropSelectedItem()
    {
        if (inventory == null)
            return;
        GameObject prefab;
        bool removed;
        switch (selectedIndex)
        {
            case 0:
                prefab = tomatoPrefab;
                removed = inventory.TryRemoveTomatoForDrop();
                break;
            case 1:
                prefab = orangePrefab;
                removed = inventory.TryRemoveOrangeForDrop();
                break;
            default:
                prefab = cabbagePrefab;
                removed = inventory.TryRemoveCabbageForDrop();
                break;
        }
        if (!removed)
        {
            Debug.Log(GetSelectedItemName() + "を持っていません！");
            return;
        }
        if (prefab == null)
        {
            Debug.LogError(GetSelectedItemName() + "のPrefabが設定されていません！");
            return;
        }
        Vector3 spawnPosition =
            transform.position + Vector3.down * dropDistance;
        GameObject dropped = Instantiate(
            prefab,
            spawnPosition,
            Quaternion.identity
        );
        Rigidbody2D rb = dropped.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 1f;
            rb.linearVelocity = Vector2.down * dropSpeed;
        }
    }
}