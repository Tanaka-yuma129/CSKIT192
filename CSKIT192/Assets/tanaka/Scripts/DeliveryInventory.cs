using UnityEngine;
using TMPro;
public class DeliveryInventory : MonoBehaviour
{
    public int maxItems = 7;
    public TMP_Text inventoryText;
    public TMP_Text messageText;
    private int tomatoCount = 0;
    private int orangeCount = 0;
    private int cabbageCount = 0;
    private int TotalCount
    {
        get { return tomatoCount + orangeCount + cabbageCount; }
    }
    void Start()
    {
        UpdateDisplay();
        if (messageText != null)
            messageText.text = "";
    }
    public void AddTomato()
    {
        AddItem("トマト");
    }
    public void AddOrange()
    {
        AddItem("みかん");
    }
    public void AddCabbage()
    {
        AddItem("キャベツ");
    }
    public void RemoveTomato()
    {
        RemoveItem("トマト");
    }
    public void RemoveOrange()
    {
        RemoveItem("みかん");
    }
    public void RemoveCabbage()
    {
        RemoveItem("キャベツ");
    }
    // スロー機能のために野菜を1個消費する
    public bool TryConsumeOneForSlow()
    {
        if (tomatoCount > 0)
        {
            tomatoCount--;
            if (messageText != null)
                messageText.text = "トマトを1個食べた！";
        }
        else if (orangeCount > 0)
        {
            orangeCount--;
            if (messageText != null)
                messageText.text = "みかんを1個食べた！";
        }
        else if (cabbageCount > 0)
        {
            cabbageCount--;
            if (messageText != null)
                messageText.text = "キャベツを1個食べた！";
        }
        else
        {
            if (messageText != null)
                messageText.text = "食べられる配達物がありません！";
            return false;
        }
        UpdateDisplay();
        return true;
    }
    private void RemoveItem(string itemName)
    {
        switch (itemName)
        {
            case "トマト":
                if (tomatoCount > 0)
                    tomatoCount--;
                else
                {
                    if (messageText != null)
                        messageText.text = "トマトは0個です！";
                    return;
                }
                break;
            case "みかん":
                if (orangeCount > 0)
                    orangeCount--;
                else
                {
                    if (messageText != null)
                        messageText.text = "みかんは0個です！";
                    return;
                }
                break;
            case "キャベツ":
                if (cabbageCount > 0)
                    cabbageCount--;
                else
                {
                    if (messageText != null)
                        messageText.text = "キャベツは0個です！";
                    return;
                }
                break;
        }
        if (messageText != null)
            messageText.text = itemName + "を1個減らした！";
        UpdateDisplay();
    }
    private void AddItem(string itemName)
    {
        if (TotalCount >= maxItems)
        {
            if (messageText != null)
                messageText.text = "インベントリがいっぱい！";
            return;
        }
        switch (itemName)
        {
            case "トマト":
                tomatoCount++;
                break;
            case "みかん":
                orangeCount++;
                break;
            case "キャベツ":
                cabbageCount++;
                break;
        }
        if (messageText != null)
            messageText.text = itemName + "を1個入れた！";
        UpdateDisplay();
    }
    private void UpdateDisplay()
    {
        if (inventoryText != null)
        {
            inventoryText.text =
                "配達員の持ち物\n" +
                "トマト：" + tomatoCount + "個\n" +
                "みかん：" + orangeCount + "個\n" +
                "キャベツ：" + cabbageCount + "個\n" +
                "合計：" + TotalCount + " / " + maxItems;
        }
    }
    public bool TryRemoveTomatoForDrop()
    {
        return TryRemoveForDrop("トマト");
    }
    public bool TryRemoveOrangeForDrop()
    {
        return TryRemoveForDrop("みかん");
    }
    public bool TryRemoveCabbageForDrop()
    {
        return TryRemoveForDrop("キャベツ");
    }
    private bool TryRemoveForDrop(string itemName)
    {
        switch (itemName)
        {
            case "トマト":
                if (tomatoCount <= 0) return false;
                tomatoCount--;
                break;
            case "みかん":
                if (orangeCount <= 0) return false;
                orangeCount--;
                break;
            case "キャベツ":
                if (cabbageCount <= 0) return false;
                cabbageCount--;
                break;
            default:
                return false;
        }
        if (messageText != null)
            messageText.text = itemName + "を落とした！";
        UpdateDisplay();
        return true;
    }
    // 配達失敗時に野菜をインベントリへ戻す
    public void ReturnItem(string itemName)
    {
        switch (itemName)
        {
            case "トマト":
                tomatoCount++;
                break;
            case "みかん":
                orangeCount++;
                break;
            case "キャベツ":
                cabbageCount++;
                break;
            default:
                Debug.LogWarning("戻す野菜の名前が不明です: " + itemName);
                return;
        }
        UpdateDisplay();
        if (messageText != null)
            messageText.text = itemName + "がインベントリに戻った！";
    }
    // 指定した野菜の所持数を調べる
    public int GetItemCount(string itemName)
    {
        switch (itemName)
        {
            case "トマト":
                return tomatoCount;
            case "みかん":
                return orangeCount;
            case "キャベツ":
                return cabbageCount;
            default:
                return 0;
        }
    }
    // 配達成功時に必要な個数を減らす
    public bool TryRemoveItems(string itemName, int count)
    {
        if (count <= 0) return false;
        if (GetItemCount(itemName) < count)
            return false;
        for (int i = 0; i < count; i++)
        {
            bool removed = TryRemoveForDrop(itemName);
            if (!removed)
                return false;
        }
        UpdateDisplay();
        return true;
    }
}