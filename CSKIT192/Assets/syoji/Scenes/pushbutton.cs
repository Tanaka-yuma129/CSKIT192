using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pushbutton:MonoBehaviour
{
    //ボタンが押されたときに呼び出される関数
    public void change_button()
    {
        //SceneManager.LoadScene("after");
        //Debug.Log("プッシュ");
        this.transform.Rotate(0, 0, 45);
    }
}
