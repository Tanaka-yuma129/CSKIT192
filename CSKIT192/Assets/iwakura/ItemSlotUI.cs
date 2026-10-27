using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.SceneManagement; // シーン切り替えに必要
using UnityEngine.UI; // 通常のTextを使うために必要
public class ResultManager : MonoBehaviour
{
    [Header("画面上のテキストUI")]
    public Text scoreText;      // 今回のスコアを表示するテキスト
    public Text highScoreText;  // ハイスコアを表示するテキスト
    public Text rankText;       // 評価（★）を表示するテキスト
    void Start()
    {
        // テスト用（DeliveryManager）
        int score = 500;
        // int score = DeliveryManager.finalScore; // 連携できたらこっちに戻す
        // ハイスコアの計算と保存処理
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }
        // スコアを画面に表示
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
        // ハイスコアを画面に表示
        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + highScore;
        }
        // 評価（★）を計算して表示
        if (rankText != null)
        {
            rankText.text = EvaluateStars(score);
        }
    }
    void Update()
    {
        // Enterキーが押されたらタイトルに戻る
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            ReturnToTitle();
        }
        // 「Rキー」が押されたらリトライ
        if (Input.GetKeyDown(KeyCode.R))
        {
            RetryGame();
        }
    }
    public void ReturnToTitle()
    {
        SceneManager.LoadScene("TitleScene"); // タイトルシーンの名前に変更してください
    }
    public void RetryGame()
    {
        SceneManager.LoadScene("GameScene"); // ゲームシーンの名前に変更してください
    }
    string EvaluateStars(int score)
    {
        if (score >= 1000) return "★★★★★ (5.0)";
        else if (score >= 800) return "★★★★☆ (4.0)";
        else if (score >= 600) return "★★★☆☆ (3.0)";
        else if (score >= 400) return "★★☆☆☆ (2.0)";
        else if (score >= 200) return "★☆☆☆☆ (1.0)";
        else return "☆☆☆☆☆ (0.0)";
    }

}