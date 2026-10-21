using UnityEngine;
using TMPro; // TextMesh Proを使うために必要

public class TimerController:MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;//UIテキストの紐付け用
    [SerializeField] private float totalTime = 60.0f;//制限時間（秒）

    private float seconds;

    void Start()
    {
        seconds = totalTime;
    }
    void Update()
    {
     //タイムが０より大きい間、毎フレーム時間を減算
     if (seconds > 0.0f)
        {
            seconds -= Time.deltaTime;

            //マイナスにならないようにクランプ（固定）
            if (seconds < 0.0f) seconds = 0.0f;
        }
        else
        {
            //ここにタイムアップ時の処理（例：ゲームオーバーシーンへの遷移など）を書く
            Debug.Log("a");
        }

        //表示形式を「分・秒」に整形してUIに反映（例:　01：23）
        int minutes = (int)seconds / 60;
        int displaySeconds = (int)seconds % 60;
        timerText.text = string.Format("{0:00}:{1:00}", minutes, displaySeconds);
    }

}
