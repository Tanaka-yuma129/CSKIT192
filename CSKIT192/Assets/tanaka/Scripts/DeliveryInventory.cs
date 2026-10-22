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