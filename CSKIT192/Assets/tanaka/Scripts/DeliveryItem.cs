using UnityEngine;
// 野菜Prefabに付けるコンポーネント
public class DeliveryItem : MonoBehaviour
{
    public string itemName = "トマト";
    public DeliveryScoreCalculator scoreCalculator;
    public DeliveryInventory inventory;
    // 通常の衝突で呼び出される
    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandlePlanetHit(collision.transform);
    }
    // Is Triggerが有効な場合にも対応
    private void OnTriggerEnter2D(Collider2D other)
    {
        HandlePlanetHit(other.transform);
    }
    private void HandlePlanetHit(Transform hitObject)
    {
        if (scoreCalculator == null)
        {
            Debug.LogError("DeliveryScoreCalculatorが設定されていません");
            return;
        }
        // 惑星に当たった場合だけ処理する
        bool hitPlanet =
            scoreCalculator.TryScoreItemHit(hitObject, itemName);
        if (hitPlanet)
        {
            // 正解でも不正解でも、当たった野菜を消す
            Destroy(gameObject);
        }
    }
}