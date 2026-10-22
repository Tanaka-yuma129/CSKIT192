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
}