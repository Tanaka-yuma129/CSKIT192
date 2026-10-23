using UnityEngine;
using UnityEngine.SceneManagement;

public class KY_TitleButton : MonoBehaviour
{
    public void TitleButton()
    {
        //ボタンが押されたら、TitleSceneに移行
        SceneManager.LoadScene("TitleScene");
    }
}
