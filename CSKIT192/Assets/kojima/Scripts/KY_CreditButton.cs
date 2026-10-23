using UnityEngine;
using UnityEngine.SceneManagement;

public class KY_CreditButton : MonoBehaviour
{
    public void CreditButton()
    {
        //ボタンが押されたら、CreditSceneに移行
        SceneManager.LoadScene("CreditScene");
    }
}
