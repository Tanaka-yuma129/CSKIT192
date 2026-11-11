using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // コルーチンを使うために必要です

public class KY_OneMore : MonoBehaviour
{
    //直前のシーン名を記録する静的変数（すべてのシーン間で共有される）
    public static string previousSceneName = "";

    //別のシーンから移動する際、このメソッドを使って遷移すると元のシーンを自動記録する
    public static void LoadSceneWithHistory(string nextSceneName)
    {
        previousSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(nextSceneName);
    }
    public void onemore()
    {
        // ボタンが押されたら、コルーチンを開始する
        StartCoroutine(DelaySceneLoad());
    }

    private IEnumerator DelaySceneLoad()
    {
        // 2秒間待つ
        yield return new WaitForSeconds(2.0f);

        if(!string.IsNullOrEmpty(previousSceneName))
        {

            if(KY_GameManager.Instance != null)
            {
                KY_GameManager.Instance.gametime = 120f;    //制限時間を120に戻す
                KY_GameManager.Instance.tip = 0;            //スコアを0に戻す
                KY_GameManager.Instance.evaluation = 5.0f;  //評価を5.0に戻す
            }

            SceneManager.LoadScene(previousSceneName);
        }
        else
        {
            Debug.LogWarning("前のシーンの記録がないため、戻れません。");
        }
    }
}
