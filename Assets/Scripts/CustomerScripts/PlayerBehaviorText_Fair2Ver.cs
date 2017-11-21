using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;

/// <summary>
/// プレイヤー(参加者)の行動記号列をテキストファイルに書き込む
/// 参考：テキストファイルを出力する - unity学習帳,
/// http://unitylab.wiki.fc2.com/wiki/%E3%83%86%E3%82%AD%E3%82%B9%E3%83%88%E3%83%95%E3%82%A1%E3%82%A4%E3%83%AB%E3%82%92%E5%87%BA%E5%8A%9B%E3%81%99%E3%82%8B
/// </summary>
public class PlayerBehaviorText_Fair2Ver : MonoBehaviour
{

    // Playerのtransform情報を取得
    Transform transform;


    public static bool walkOrNot = false;
    public static bool pickUpOrNot = false;


    /// <summary>
    /// 自身がどの動作をしているかを表す定数
    /// others            : 0
    /// walk              : 1
    /// stop              : 2
    /// turning           : 3
    /// pick up           : 4
    /// thinking          : 5
    /// look around       : 6
    /// </summary>
    public enum WhichBehavior
    {
        OTHERS = 0,
        WALK,
        STOP,
        TURNING,
        PICKUP,
        THINKING,
        LOOKAROUND
    }
    public WhichBehavior whichBehavior;

    /*
    const int OTHERS = 0,
              WALK = 1,
              STOP = 2,
              TURNING = 3,
              PICKUP = 4,
              THINKING = 5,
              LOOKAROUND = 6;
    public int whichBehavior = WALK;
    */


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

    //public List<List<string>> playerBehavList = new List<List<string>>();
    //public List<string> behavArray = new List<string>();

    //int lastBehav = -1;
    WhichBehavior lastBehav;
    float time = 0f;


    // 別オブジェクトの変数などを考慮に入れる
    GameObject playerControllerObj;



    // Use this for initialization
    void Start()
    {
        transform = GetComponent<Transform>();

        // 初期化
        walkOrNot = false;
        pickUpOrNot = false;


        playerControllerObj = GameObject.Find("PlayerController");

    }



    // Update is called once per frame
    void Update()
    {
        // 別のオブジェクトのスクリプトを参照
        PlayerControllerBehaviour playerControllerBehav = playerControllerObj.GetComponent<PlayerControllerBehaviour>();


        // ポーズ画面でなければ
        if (Pausable.pauseGame == false)
        {
            walkOrNot = playerControllerBehav.walkOrNot;

            // 1秒以上停止していたらwhichBehavior = STOPに
            if (walkOrNot == true)
            {
                whichBehavior = WhichBehavior.WALK;
                time = 0;
            }
            else if (walkOrNot == false)
            {
                time += Time.deltaTime;
                if (time > 1.0f)
                {
                    whichBehavior = WhichBehavior.STOP;
                    time = 0;
                }
            }

            // その他の細かい動作は
            // キー入力で判定する
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Debug.Log("Alpha4(PICKUP) is pushed.");
                whichBehavior = WhichBehavior.PICKUP;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                Debug.Log("Alpha5(THINKING) is pushed.");
                whichBehavior = WhichBehavior.THINKING;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                Debug.Log("Alpha6(LOOKAROUND) is pushed.");
                whichBehavior = WhichBehavior.LOOKAROUND;
            }

            // 行動が変わるごとに毎に行動記号を書き込んでいく
            if (lastBehav != whichBehavior)
            {
                //Debug.Log("whichBehav was changed from " + lastBehav + " to " + whichBehavior);
                playerBehavList.Add(whichBehavior.ToString());
            }


            lastBehav = whichBehavior;
        }
    }


    /// <summary>
    /// 客が各エリアのInterSectionに侵入したときに呼ばれる
    /// </summary>
    /// <param name="collider"></param>
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Sec_WAT")
        {
            //sw.WriteLine(WAT);
            // エリアごとにbehavArrayを区切ってplayerBehavListに
            // playerBehavListにつないでいく
            playerBehavList.Add(WAT);
            playerBehavList.Add(WhichBehavior.WALK.ToString());
            //playerBehavList.Add(behavArray);
            //behavArray.Clear();
            //behavArray.Add(WAT);
            //Debug.Log("Enter WAT, " + WAT);
        }
        else if (collider.gameObject.tag == "Sec_TA")
        {
            //sw.WriteLine(TA);
            playerBehavList.Add(TA);
            playerBehavList.Add(WhichBehavior.WALK.ToString());
            //Debug.Log("Enter TA, " + TA);
        }
        else if (collider.gameObject.tag == "Sec_SHA")
        {
            //sw.WriteLine(SHA);
            playerBehavList.Add(SHA);
            playerBehavList.Add(WhichBehavior.WALK.ToString());
            //Debug.Log("Enter SHA, " + SHA);
        }
        else if (collider.gameObject.tag == "Sec_YA")
        {
            //sw.WriteLine(YA);
            playerBehavList.Add(YA);
            playerBehavList.Add(WhichBehavior.WALK.ToString());
            //Debug.Log("Enter YA, " + YA);
        }
        else if (collider.gameObject.tag == "Sec_KAI")
        {
            //sw.WriteLine(KAI);
            playerBehavList.Add(KAI);
            playerBehavList.Add(WhichBehavior.WALK.ToString());
            //Debug.Log("Enter KAI, " + KAI);
        }
        else if (collider.gameObject.tag == "Sec_WAN")
        {
            //sw.WriteLine(WAN);
            playerBehavList.Add(WAN);
            playerBehavList.Add(WhichBehavior.WALK.ToString());
            //Debug.Log("Enter WAN, " + WAN);
        }
        else if (collider.gameObject.tag == "Sec_YO")
        {
            //sw.WriteLine(YO);
            playerBehavList.Add(YO);
            playerBehavList.Add(WhichBehavior.WALK.ToString());
            //Debug.Log("Enter YO, " + YO);
        }
        else if (collider.gameObject.tag == "Sec_KI")
        {
            //sw.WriteLine(KI);
            playerBehavList.Add(KI);
            playerBehavList.Add(WhichBehavior.WALK.ToString());
            //Debug.Log("Enter KI, " + KI);
        }
        else if (collider.gameObject.tag == "Sec_KAK")
        {
            //sw.WriteLine(KAK);
            playerBehavList.Add(KAK);
            playerBehavList.Add(WhichBehavior.WALK.ToString());
            //Debug.Log("Enter KAK, " + KAK);
        }
        else if (collider.gameObject.tag == "Sec_RI")
        {
            //sw.WriteLine(RI);
            playerBehavList.Add(RI);
            playerBehavList.Add(WhichBehavior.WALK.ToString());
            //Debug.Log("Enter RI");
        }
        else if (collider.gameObject.tag == "Sec_FAN_1")
        {
            playerBehavList.Add(FAN_1);
            playerBehavList.Add(WhichBehavior.WALK.ToString());
        }
        else if (collider.gameObject.tag == "Sec_FAN_2")
        {
            playerBehavList.Add(FAN_2);
            playerBehavList.Add(WhichBehavior.WALK.ToString());
        }
        else if (collider.gameObject.tag == "Sec_F_STA")
        {
            playerBehavList.Add(STA_F);
            playerBehavList.Add(WhichBehavior.WALK.ToString());
        }
        else if (collider.gameObject.tag == "Sec_S_STA")
        {
            playerBehavList.Add(STA_S);
            playerBehavList.Add(WhichBehavior.WALK.ToString());
        }
        else if (collider.gameObject.tag == "Sec_COR")
        {
            playerBehavList.Add(COR);
            playerBehavList.Add(WhichBehavior.WALK.ToString());
        }
    }
}


