using System.Collections.Generic;

using UnityEngine;

using TMPro;

public class DropItemController : MonoBehaviour

{

    public PlayerFlight playerFlight;

    public DeliveryInventory inventory;

    public DeliveryScoreCalculator scoreCalculator;

    public GameObject tomatoPrefab;

    public GameObject orangePrefab;

    public GameObject cabbagePrefab;

    public TMP_Text selectedItemText;

    public float dropDistance = 1.5f;

    public float dropSpeed = 3f;

    // 0 = トマト、1 = みかん、2 = キャベツ

    private int selectedIndex = 0;

    private static int savedSelectedIndex = 0;

    // 今回の飛行中に落とした野菜を記録

    private List<DeliveryItem> droppedItems =

        new List<DeliveryItem>();

    void Start()

    {

        selectedIndex = savedSelectedIndex;

        UpdateSelectedItemText();

    }

    void Update()

    {

        if (playerFlight == null) return;

        if (playerFlight.IsFlying &&

            Input.GetKeyDown(KeyCode.Q))

        {

            selectedIndex = (selectedIndex + 1) % 3;

            savedSelectedIndex = selectedIndex;

            UpdateSelectedItemText();

        }

        if (playerFlight.IsFlying &&

            Input.GetKeyDown(KeyCode.Space))

        {

            DropSelectedItem();

        }

    }

    void UpdateSelectedItemText()

    {

        if (selectedItemText == null) return;

        selectedItemText.text =

            "落とす野菜：" + GetSelectedItemName() +

            "（Qで変更）";

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

        {

            Debug.LogError("Inventoryが設定されていません");

            return;

        }

        // 先にPrefabを確認する

        GameObject prefab;

        switch (selectedIndex)

        {

            case 0: prefab = tomatoPrefab; break;

            case 1: prefab = orangePrefab; break;

            default: prefab = cabbagePrefab; break;

        }

        if (prefab == null)

        {

            Debug.LogError(

                GetSelectedItemName() +

                "のPrefabが設定されていません！");

            return;

        }

        // インベントリから野菜を1個減らす

        bool removed;

        switch (selectedIndex)

        {

            case 0:

                removed = inventory.TryRemoveTomatoForDrop();

                break;

            case 1:

                removed = inventory.TryRemoveOrangeForDrop();

                break;

            default:

                removed = inventory.TryRemoveCabbageForDrop();

                break;

        }

        if (!removed)

        {

            Debug.Log(GetSelectedItemName() + "を持っていません！");

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

        DeliveryItem deliveryItem =

            dropped.GetComponent<DeliveryItem>();

        if (deliveryItem == null)

        {

            Debug.LogError(

                "野菜のPrefabにDeliveryItemがありません！");

            inventory.ReturnItem(GetSelectedItemName());

            Destroy(dropped);

            return;

        }

        // 野菜に必要な情報を渡す

        deliveryItem.itemName = GetSelectedItemName();

        deliveryItem.scoreCalculator = scoreCalculator;

        deliveryItem.inventory = inventory;

        // 今回落とした野菜として記録する

        droppedItems.Add(deliveryItem);

    }

    // 着地時に呼び出す：

    // 惑星に届かず残っている野菜だけをインベントリへ戻す

    // 配達結果に応じて、残っている野菜を処理する
    public void FinishDelivery(bool success)
    {
        foreach (DeliveryItem item in droppedItems)
        {
            // 惑星に届いて消えた野菜は処理しない
            if (item == null) continue;
            if (!success && inventory != null)
            {
                // 失敗時だけ、届かなかった野菜を返す
                inventory.ReturnItem(item.itemName);
            }
            // 成功時は消費、失敗時は返却後に消す
            Destroy(item.gameObject);
        }
        // 次の配達に前回の記録を持ち越さない
        droppedItems.Clear();
    }

}
