using UnityEngine;
using TMPro;
public class DeliveryScoreCalculator : MonoBehaviour
{
    [Header("地球")]
    public Transform earth;
    [Header("合計スコア表示")]
    public TMP_Text scoreText;
    public int totalScore = 0;
    [Header("惑星の注文管理")]
    public PlanetOrderManager orderManager;

    // 着地時にインベントリと注文を確認する
    public bool TryScoreLanding(
        Transform planet,
        PlanetOrderManager orderManager,
        DeliveryInventory inventory,
        out string message)
    {
        message = "配達失敗！";
        if (earth == null || planet == null ||
            orderManager == null || inventory == null)
        {
            message = "設定エラー：参照を確認してください";
            Debug.LogError(message);
            return false;
        }
        if (!orderManager.TryGetOrder(
            planet, out string itemName, out int requiredCount))
        {
            message = "注文を確認できません";
            Debug.LogError(message);
            return false;
        }
        // インベントリに必要な数があるか確認
        int ownedCount = inventory.GetItemCount(itemName);
        if (ownedCount < requiredCount)
        {
            message =
                "配達失敗！\n" +
                itemName + "があと" +
                (requiredCount - ownedCount) + "個必要です";
            Debug.Log(message);
            return false;
        }
        int itemPoints = GetItemPoints(itemName);
        int planetPoints = GetPlanetPoints(planet.name);
        if (itemPoints <= 0 || planetPoints < 0)
        {
            message = "設定エラー：野菜または惑星名を確認してください";
            Debug.LogError(message);
            return false;
        }
        // 納品成功時だけ必要な個数を減らす
        if (!inventory.TryRemoveItems(itemName, requiredCount))
        {
            message = "配達失敗！野菜の数を確認してください";
            return false;
        }
        int distancePoints = Mathf.RoundToInt(
            Vector2.Distance(earth.position, planet.position));
        int earnedScore =
            itemPoints * requiredCount +
            planetPoints +
            distancePoints;
        totalScore += earnedScore;
        if (scoreText != null)
        {
            scoreText.text = "合計スコア：" + totalScore;
        }
        message =
            "配達成功！\n" +
            itemName + "を" + requiredCount +
            "個納品！\n獲得スコア：" + earnedScore + "点";
        Debug.Log(message);
        return true;
    }
    private int GetItemPoints(string itemName)
    {
        switch (itemName)
        {
            case "トマト": return 100;
            case "みかん": return 200;
            case "キャベツ": return 300;
            default: return 0;
        }
    }
    private int GetPlanetPoints(string planetName)
    {
        if (planetName.Contains("水星") ||
            planetName.Contains("Mercury"))
            return 300;
        if (planetName.Contains("火星") ||
            planetName.Contains("Mars"))
            return 250;
        if (planetName.Contains("土星") ||
            planetName.Contains("Saturn"))
            return 200;
        return -1;
    }
    public bool TryScoreItemHit(Transform planet, string itemName)
    {
        // 注文管理が設定されていない場合
        if (orderManager == null || planet == null)
        {
            Debug.LogError("注文管理または惑星が設定されていません");
            return false;
        }
        // 惑星が求めている野菜を取得する
        if (!orderManager.TryGetOrder(
            planet, out string requiredItem, out int requiredCount))
        {
            // 注文のない惑星なら何もしない
            return false;
        }
        // 求めている野菜と違う場合は加点しない
        if (itemName != requiredItem)
        {
            Debug.Log(
                planet.name + "は" + requiredItem +
                "を求めています。投げた野菜：" + itemName +
                "。0点！");
            return true;
        }
        // 野菜の種類と惑星の種類の点数を合計する
        int itemPoints = GetItemPoints(itemName);
        int planetPoints = GetPlanetPoints(planet.name);
        if (itemPoints <= 0 || planetPoints < 0)
        {
            Debug.LogError("野菜または惑星の点数設定を確認してください");
            return true;
        }
        int earnedScore = itemPoints + planetPoints;
        totalScore += earnedScore;
        if (scoreText != null)
        {
            scoreText.text = "合計スコア：" + totalScore;
        }
        Debug.Log(
            "配達成功！ " + planet.name + "に" + itemName +
            "が命中！ 獲得スコア：" + earnedScore + "点");
        return true;
    }
}