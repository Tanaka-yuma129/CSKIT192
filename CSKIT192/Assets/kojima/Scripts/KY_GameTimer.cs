using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class KY_GameTimer : MonoBehaviour
{
    public TextMeshProUGUI TimerText; //タイマー用テキスト
    [SerializeField]private float gametimer = 120f; //ゲーム時間(秒)

    // Update is called once per frame
    void Update()
    {
        //ゲーム時間のカウントダウン
        if (gametimer > 0)
        {
            gametimer -= Time.deltaTime;
        }
        else
        {
            gametimer = 0;
            SceneManager.LoadScene("ResultScene"); //シーン指定:ResultScene
        }

        int minutes = Mathf.FloorToInt(gametimer / 60f);
        int seconds = Mathf.FloorToInt(gametimer % 60f);

        TimerText.text = string.Format("残り時間:{0:0}:{1:00}", minutes, seconds);
    }
}
