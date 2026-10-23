using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ItemSlotUI : MonoBehaviour
{
    [System.Serializable]
    public struct ItemSlot
    {
        public Image iconImage;         // アイコンのImage
        public TextMeshProUGUI countText; // 個数を表示するテキスト
        [HideInInspector] public int count; // 現在の個数
    }
    [Header("3つのアイテムスロット")]
    public ItemSlot[] slots = new ItemSlot[3];
    [Header("スコア設定")]
    public int score = 0; // 現在のスコア
    void Start()
    {
        UpdateUI();
    }
    // 指定したスロットのアイテムを増やす（例: index 0 = 荷物A）
    public void AddItem(int slotIndex, int amount = 1)
    {
        if (slotIndex >= 0 && slotIndex < slots.Length)
        {
            slots[slotIndex].count += amount;
            UpdateUI();
        }
    }
    // 指定したスロットのアイテムを減らす
    public void RemoveItem(int slotIndex, int amount = 1)
    {
        if (slotIndex >= 0 && slotIndex < slots.Length)
        {
            slots[slotIndex].count -= amount;
            if (slots[slotIndex].count < 0) slots[slotIndex].count = 0;
            UpdateUI();
        }
    }
    // 画面の表示を更新する
    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            // アイコン画像を常にハッキリ表示
            if (slots[i].iconImage != null)
            {
                Color c = slots[i].iconImage.color;
                c.a = 1.0f;
                slots[i].iconImage.color = c;
            }
            // 個数テキストの表示を「x 0」「x 1」の形で一律セット
            if (slots[i].countText != null)
            {
                slots[i].countText.text = "x " + slots[i].count;
            }
        }
    }
    // ==========================================
    // ▼ ここから下が今回新しく繋げた「配達成功・スコア獲得」の処理です！
    // ==========================================
    // 惑星に荷物が当たって配達が成功したときに呼び出す関数
    // usedSlotIndex: 何番目のスロットの荷物を使ったか (0, 1, 2)
    // earnScore: この惑星にもらえたスコア (例: 100)
    public void OnDeliverySuccess(int usedSlotIndex, int earnScore)
    {
        // 1. 使った荷物をスロットから1つ減らす
        RemoveItem(usedSlotIndex, 1);
        // 2. スコアを増やす
        score += earnScore;
        Debug.Log("配達成功！ 現在のスコア: " + score);
    }
}
public class DeliveryManager : MonoBehaviour
{
    // ▼【重要】staticをつけることで、シーンが切り替わってもデータが残ります！
    public static int finalScore = 0;
    [Header("ゲームの設定")]
    public float timeRemaining = 120f; // 制限時間（2分 ＝ 120秒）
    public int scorePerDelivery = 100; // 1回配達したときのスコア
    private int currentScore = 0;
    private bool isGameOver = false;
    void Update()
    {
        if (isGameOver) return;
        // タイマーを減算
        timeRemaining -= Time.deltaTime;
        // 制限時間が0になったらゲーム終了
        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            EndGame();
        }
    }
    // 配達物を届ける処理（配達成功時にこれを呼ぶ）
    public void DeliverPackage()
    {
        if (isGameOver) return;
        currentScore += scorePerDelivery;
        // 画面のスコア表示などを更新する処理をここに書く
    }
    // ゲーム終了処理
    void EndGame()
    {
        isGameOver = true;
        // 獲得したスコアを静的変数（static）に保存
        finalScore = currentScore;
        // 終了画面のシーンに移動（作成したシーン名に合わせて書き換えてください）
        SceneManager.LoadScene("GameOverScene");
    }
}

public class ResultManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // スコア表示用テキスト
    public TextMeshProUGUI rankText;  // ★評価表示用テキスト
    void Start()
    {
        // 前のシーンから保存されたスコアを取得
        int score = DeliveryManager.finalScore;
        // スコアを画面に表示
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
        // ★評価を計算して画面に表示
        if (rankText != null)
        {
            rankText.text = EvaluateStars(score);
        }
    }
    string EvaluateStars(int score)
    {
        if (score >= 1000) return "★★★★★ (5.0)";
        if (score >= 900) return "★★★★⯨ (4.5)"; // ★4.5
        if (score >= 800) return "★★★★☆ (4.0)";
        if (score >= 700) return "★★★⯨☆ (3.5)"; // ★3.5
                                                // ...以下同様に増やせます
        return "☆☆☆☆☆ (0.0)";
    }
}