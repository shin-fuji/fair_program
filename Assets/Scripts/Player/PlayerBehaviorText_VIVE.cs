using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// プレイヤー(参加者)の行動記号列をテキストファイルに書き込む(VIVE HTC使用バージョン)
/// 参考：テキストファイルを出力する - unity学習帳,
/// http://unitylab.wiki.fc2.com/wiki/%E3%83%86%E3%82%AD%E3%82%B9%E3%83%88%E3%83%95%E3%82%A1%E3%82%A4%E3%83%AB%E3%82%92%E5%87%BA%E5%8A%9B%E3%81%99%E3%82%8B
/// 
/// TODO
/// </summary>
public class PlayerBehaviorText_VIVE : MonoBehaviour
{

    public static bool walkOrNot = false;
    public static bool pickUpOrNot = false;


    /// <summary>
    /// 自身がどの動作をしているかを表す定数
    /// default           : 0
    /// walk              : 1
    /// stop              : 2
    /// turning           : 3
    /// pick up           : 4
    /// thinking          : 5
    /// look around       : 6
    /// appreciation      : 7
    /// handclap          : 8
    /// applause          : 9
    /// </summary>
    public enum WhichBehavior
    {
        DEFAULT = 0,
        WALK,
        STOP,
        TURNING,
        PICKUP,
        THINKING,
        LOOKAROUND,
        APPRECIATION,
        HANDCLAP,
        APPLAUSE
    }
    public WhichBehavior whichBehavior;


    /// <summary>
    /// 客が現在どこにいるかなどを示す定数
    /// A : わたあめ屋(WAT)
    /// B : たこ焼き屋(TA)
    /// C : 射的(SHA)
    /// D : 焼きそば屋(YA)
    /// E : 海鮮焼き屋(KAI)
    /// F : 輪投げ(WAN)
    /// G : ヨーヨー釣り(YO)
    /// H : 金魚すくい(KI)
    /// I : かき氷屋(KAK)
    /// J : リンゴ飴屋(RI)
    /// K : ファンタジー屋台1(FAN_1)
    /// L : ファンタジー屋台2(FAN_2)
    /// M : 曲がり角(COR)
    /// N : 舞台前(STA_F)
    /// O : 舞台横(STA_S)
    /// P : 右側出口(RI_EXIT)
    /// Q : 下側出口(LO_EXIT)
    /// </summary>
    const string
        WAT = "A", TA = "B", SHA = "C", YA = "D", KAI = "E",
        WAN = "F", YO = "G", KI = "H", KAK = "I", RI = "J",
        FAN_1 = "K", FAN_2 = "L", COR = "M",
        STA_F = "N", STA_S = "O",
        RI_EXIT = "P", LO_EXIT = "Q";


    // プレイヤーの行動記号列
    public List<string> playerBehavList = new List<string>();
    
    //int lastBehav = -1;
    WhichBehavior lastBehav;
    float time = 0f;


    // player 情報
    // [VRTK_SDKManager]->SteamVR->[CameraRig]->Camera (eye)
    private Transform Player;
    private Vector3 LastPlayerPos;


    // Use this for initialization
    void Start()
    {
        Player = GameObject.Find("Camera (eye)").transform;
        playerBehavList.Add(((int)WhichBehavior.DEFAULT).ToString());
        playerBehavList.Add(",");
    }



    // Update is called once per frame
    void Update()
    {

        //playerBehavList.ShowListContentsInTheDebugLog();

        // ポーズ画面でなければ
        if (Pausable.pauseGame == false)
        {

            // 行動が変わるごとに毎に行動記号を書き込んでいく
            // whichBehavior は BehaviourButton.cs にて操作している
            // lastBehav が"0"(エリア移動後初めての行動)なら，その"0"を消して記号を追加する
            if (lastBehav != whichBehavior)
            {
                if (lastBehav == WhichBehavior.DEFAULT)
                {
                    // 記号列末尾の「0,」を消す
                    int leng = playerBehavList.Count;
                    playerBehavList.RemoveAt(leng - 1);
                    playerBehavList.RemoveAt(leng - 2);
                }
                //Debug.Log("whichBehav was changed from " + lastBehav + " to " + whichBehavior);
                playerBehavList.Add(((int)whichBehavior).ToString());
                playerBehavList.Add(",");


            }
            
            lastBehav = whichBehavior;
        }

        // player の位置情報を更新
        LastPlayerPos = Player.position;
    }


    /// <summary>
    /// 客が各エリアのInterSectionに侵入したときに呼ばれる
    /// </summary>
    /// <param name="collider"></param>
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Sec_WAT") MoveTheArea(WAT);
        else if (collider.gameObject.tag == "Sec_TA") MoveTheArea(TA);
        else if (collider.gameObject.tag == "Sec_SHA") MoveTheArea(SHA);
        else if (collider.gameObject.tag == "Sec_YA") MoveTheArea(YA);
        else if (collider.gameObject.tag == "Sec_KAI") MoveTheArea(KAI);
        else if (collider.gameObject.tag == "Sec_WAN") MoveTheArea(WAN);
        else if (collider.gameObject.tag == "Sec_YO") MoveTheArea(YO);
        else if (collider.gameObject.tag == "Sec_KI") MoveTheArea(KI);
        else if (collider.gameObject.tag == "Sec_KAK") MoveTheArea(KAK);
        else if (collider.gameObject.tag == "Sec_RI") MoveTheArea(RI);
        else if (collider.gameObject.tag == "Sec_FAN_1") MoveTheArea(FAN_1);
        else if (collider.gameObject.tag == "Sec_FAN_2") MoveTheArea(FAN_2);
        else if (collider.gameObject.tag == "Sec_F_STA") MoveTheArea(STA_F);
        else if (collider.gameObject.tag == "Sec_S_STA") MoveTheArea(STA_S);
        else if (collider.gameObject.tag == "Sec_COR") MoveTheArea(COR);

    }

    /// <summary>
    /// 歩いているか否かを判定する関数
    /// 各フレーム間での位置の差が一定以上であれば，歩いているとみなす
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    private bool IsWalking(Vector3 p1, Vector3 p2)
    {
        if (VectorDistance(p1, p2) > 0.02) return true;
        else return false;
    }

    /// <summary>
    /// x-z平面における二点間の距離を計算する
    /// </summary>
    /// <param name="v1">移動後の位置</param>
    /// <param name="v2">移動前の位置</param>
    /// <returns></returns>
    private float VectorDistance(Vector3 v1, Vector3 v2)
    {
        return (float)System.Math.Sqrt(System.Math.Pow(v1.x - v2.x, 2) + System.Math.Pow(v1.z - v2.z, 2));
    }

    /// <summary>
    /// プレイヤーがエリアを移動するごとに呼ばれる
    /// 行動記号列に，移動先のエリア記号と，デフォルト記号"0"を追加する
    /// 
    /// また，lastBehav, whichBehavior を DEFAULT にリセットしておく
    /// </summary>
    /// <param name="tag">移動先のエリア記号</param>
    private void MoveTheArea(string AREA)
    {
        playerBehavList.Add(AREA);
        playerBehavList.Add(",");
        playerBehavList.Add(((int)WhichBehavior.DEFAULT).ToString());
        playerBehavList.Add(",");
        lastBehav = WhichBehavior.DEFAULT;
        whichBehavior = WhichBehavior.DEFAULT;
    }
}