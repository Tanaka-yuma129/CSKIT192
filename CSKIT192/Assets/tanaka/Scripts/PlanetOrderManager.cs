using UnityEngine;
using TMPro;
public class PlanetOrderManager : MonoBehaviour
{
    [Header("注文表示")]
    public TMP_Text mercuryOrderText;
    public TMP_Text marsOrderText;
    public TMP_Text saturnOrderText;
    private string[] itemNames =
    {
       "トマト", "みかん", "キャベツ"
   };
    private class OrderData
    {
        public string itemName;
        public int requiredCount;
    }
    private OrderData mercuryOrder;
    private OrderData marsOrder;
    private OrderData saturnOrder;
    void Start()
    {
        RandomizeAllOrders();
    }
    // 3つすべての惑星の注文をランダムに変更
    public void RandomizeAllOrders()
    {
        mercuryOrder = CreateRandomOrder();
        marsOrder = CreateRandomOrder();
        saturnOrder = CreateRandomOrder();
        UpdateOrderDisplay();
        Debug.Log("全惑星の配達注文を更新しました！");
    }
    private OrderData CreateRandomOrder()
    {
        OrderData order = new OrderData();
        int itemIndex = Random.Range(0, itemNames.Length);
        order.itemName = itemNames[itemIndex];
        // 必要個数は1～3個
        order.requiredCount = Random.Range(1, 4);
        return order;
    }
    private void UpdateOrderDisplay()
    {
        if (mercuryOrderText != null)
        {
            mercuryOrderText.text =
                "水星の注文\n" +
                "欲しい野菜：" + mercuryOrder.itemName + "\n" +
                "必要な数：" + mercuryOrder.requiredCount + "個";
        }
        if (marsOrderText != null)
        {
            marsOrderText.text =
                "火星の注文\n" +
                "欲しい野菜：" + marsOrder.itemName + "\n" +
                "必要な数：" + marsOrder.requiredCount + "個";
        }
        if (saturnOrderText != null)
        {
            saturnOrderText.text =
                "土星の注文\n" +
                "欲しい野菜：" + saturnOrder.itemName + "\n" +
                "必要な数：" + saturnOrder.requiredCount + "個";
        }
    }
    // 惑星の注文内容を取得する
    public bool TryGetOrder(
       Transform planet,
       out string itemName,
       out int requiredCount)
    {
        itemName = "";
        requiredCount = 0;
        if (planet == null) return false;
        OrderData order = null;
        string planetName = planet.name;
        if (planetName.Contains("水星") ||
            planetName.Contains("Mercury"))
        {
            order = mercuryOrder;
        }
        else if (planetName.Contains("火星") ||
                 planetName.Contains("Mars"))
        {
            order = marsOrder;
        }
        else if (planetName.Contains("土星") ||
                 planetName.Contains("Saturn"))
        {
            order = saturnOrder;
        }
        if (order == null) return false;
        itemName = order.itemName;
        requiredCount = order.requiredCount;
        return true;
    }
}
