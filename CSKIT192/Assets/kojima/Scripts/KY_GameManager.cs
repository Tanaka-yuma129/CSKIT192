using UnityEngine;

public class KY_GameManager : MonoBehaviour
{
    //ほかのスクリプトから「KY_GameManager instance～」でアクセスできるようにする（シングルトン）
    public static KY_GameManager Instance {  get; private set; }

    //持ち越したいデータ
    public float gametime = 120f;            //制限時間
    public int tip = 0;                      //スコア
    public float evaluation = 5.0f;          //評価

    void Awake()
    {
        //ゲーム中にKY_GameManagerが一つだけ存在するように初期化する
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);//シーンが切り替わっても破壊しない(常駐させる)
        }
        else
        {
            Destroy(gameObject);//既に存在しているなら重複なので消す
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gametime > 0)
        {
            //制限時間を減らし続ける
            gametime -= Time.deltaTime;
        }
    }

    //スコアを増やす関数(ほかのスクリプトから呼ぶ用)
    public void AddTip(int points)
    {
        tip += points;
    }

    //評価を変更させる関数
    public void ChangeEvaluation(float amount)
    {
        evaluation += amount;

        evaluation = Mathf.Clamp(evaluation, 0.0f, 5.0f);
    }
}
