using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // コルーチンを使うために必要です

public class KY_MapSelectChange : MonoBehaviour
{
    public void Stage1()
    {
        // ボタンが押されたら、コルーチンを開始する
        StartCoroutine(DelaySceneLoad());
    }

    private IEnumerator DelaySceneLoad()
    {
        // 2秒間待つ
        yield return new WaitForSeconds(2.0f);

        // CreditScene（T_MainScene）に移行
        SceneManager.LoadScene("T_MainScene");
    }
}
