using UnityEngine;
using UnityEngine.SceneManagement;

public class KY_Map : MonoBehaviour
{
    //MainSceneの動きを止めるためにMainをインスペクターからアタッチ
    [SerializeField] private GameObject mapRootObject;
    //マップが開いているかどうかを管理するフラグ
    private bool isInMap = false;

    void Update()
    {
        //Mキーが押されたら
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!isInMap)
            {
                //マップが開いていなければ、マップを開く
                GotoMap();
            }
            else
            {
                //マップが開いていれば、マップを閉じる
                ReturnToMap();
            }
        }
    }

    //マップへの移行
    private void GotoMap()
    {
        //マップのフラグをオンにする
        isInMap = true;

       
        if (mapRootObject != null)
         {
            //MainSceneの更新や描画を一時的に停止する
            mapRootObject.SetActive(false);
            ////MapSceneを追加ロード
            SceneManager.LoadScene("MapScene", LoadSceneMode.Additive);
          }

        Debug.Log("戦闘シーンを読み込みました（Mキー）");
    }

    //マップを閉じる時
    private void ReturnToMap()
    {
        //MapSceneの破棄
        SceneManager.UnloadSceneAsync("MapScene").completed += (op) =>
        {
            if (mapRootObject != null)
            {
                //完全にアンロードしたら、MainSceneを再開する
                mapRootObject.SetActive(true);
            }

            //マップのフラグをオフにする
            isInMap = false;
            Debug.Log("マップシーンに戻りました（Mキー）");
        };
    }

}
