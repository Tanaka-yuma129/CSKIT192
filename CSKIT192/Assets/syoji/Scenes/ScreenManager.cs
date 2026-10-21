using UnityEngine;
using UnityEngine.SceneManagement;//シーン管理に必要

public class ScreenManager:MonoBehaviour
{
    //ボタンが押されたら呼び出す関数
    public void BackToPreviousScene()
    {
        //"BeforeSceneName"を戻りたい実際のシーン名に変更してください
        SceneManager.LoadScene("BeforSceneName");
    }
}
