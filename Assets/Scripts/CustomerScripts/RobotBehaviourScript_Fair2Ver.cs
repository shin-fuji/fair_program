using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This script manages behaviours of the Robot(ex, which the robot is walking or not).
/// モデルのアニメーションを制御するためのスクリプト
/// HeadBehaviorの制御もここで行っている
/// 
/// TODO：
/// ・鑑賞（行動番号7）について，アニメコントローラのboolをいつfalseにするかを考える→ダンスのアニメーションが終わった時点でfalseに
/// ・手拍子のHBは，必ず伝播するようにする
/// ・ダンス鑑賞開始後，ランダムで手拍子を始めるようにする．ダンス終了後は拍手（乱数で誰かが拍手し始めるようにし，それを伝播させる）
/// 
/// </summary>
public class RobotBehaviourScript_Fair2Ver : MonoBehaviour
{

    // Customerのtransform情報を取得
    Transform transform;

    // player 情報
    GameObject player; // [VRTK_SDKManager]->SteamVR->[CameraRig]

    Animator anim;

    // head behaviour を適用するかどうか
    public bool ApplyHeadBehaviour;


    // テスト用のデフォルト客であるかどうか
    //public bool defaultCustomer;

    // 自身がどの動作をしているかを表す定数
    // default           : 0
    // walk              : 1
    // stop              : 2 
    // turing            : 3
    // pick up           : 4
    // thinking          : 5
    // look around       : 6
    // appreciation      : 7
    // handclap          : 8
    // applause          : 9
    const int DEFAULT = 0,
              WALK = 1,
              STOP = 2,
              TURNING = 3,
              PICKUP = 4,
              THINKING = 5,
              LOOKAROUND = 6,
              APPRECIATION = 7,
              HANDCLAP = 8,
              APPLAUSE = 9;
    public int whichBehavior = WALK;

    // 特定の行動を起こすための変数
    public int doBehavior;

    // アニメーション関連の変数
    public AnimatorStateInfo animInfo;
    Vector3 from;
    

    // HerdBehaviorを扱うための変数
    List<GameObject> otherCustomerList = new List<GameObject>();

    // HerdBehavior を計算するための変数
    // TimeInterval 秒ごとに HerdBehavior を計算している
    float timer = 0, TimeInterval = 0.5f;
    // 周囲を見渡す動作を行うのに必要なtime変数
    float timerLA = 0;

    // 店員の位置リスト
    List<Vector3> clerkPos = new List<Vector3>();

    // 手拍子などの効果音
    [SerializeField]
    AudioClip handclapSound, applauseSound;
    AudioSource audioSource;



    // Use this for initialization
    void Start()
    {
        transform = GetComponent<Transform>();
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        // 店員の位置を取得
        for (int i = 0; i < 10; i++)
        {
            clerkPos.Add(GameObject.Find("Clerks").transform.Find("Clerk" + i).transform.position);
        }
        
        //Debug.Log("ApplyHeadBehaviour is " + ApplyHeadBehaviour);
    }



    // Update is called once per frame
    void Update()
    {
        // タイマー
        timer += Time.deltaTime;

        transform = GetComponent<Transform>();
        animInfo = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);

        // doBehaviour 変数に従って行動する
        DoBehavior(doBehavior);



        if (animInfo.fullPathHash == Animator.StringToHash("Base Layer.Walk"))
        {
            // アニメーション遷移のタイミングの関係でPickUp, Thinkingの初期化はここで行う
            anim.SetBool("PickUp", false);
            anim.SetBool("Thinking", false);

            // LookAroundTimer
            timerLA = 0;

            whichBehavior = WALK;
            from = transform.forward;  // 今向いている方向
        }
        if (animInfo.fullPathHash == Animator.StringToHash("Base Layer.Turning"))
        {
            // どの店の前にいるかを取得し，その店の店員の方向へ向く
            NavMeshofCustomer_Fair2Ver mode = GetComponent<NavMeshofCustomer_Fair2Ver>();
            if (mode.mode <= 9) RobotTurning(transform.position + from, clerkPos[mode.mode]);

            // Turningから遷移しない問題，とりあえず，臨時でThinkingをtrueにしている
            if (anim.GetBool("PickUp") == false && anim.GetBool("Thinking") == false) anim.SetBool("Thinking", true);
        }
        if (animInfo.fullPathHash == Animator.StringToHash("Base Layer.ToFoward"))
        {
            // どの店の前にいるかを取得し，その店の店員の方向から元の方向へ向く
            NavMeshofCustomer_Fair2Ver mode = GetComponent<NavMeshofCustomer_Fair2Ver>();
            if (mode.mode <= 9) RobotTurning(clerkPos[mode.mode], transform.position + from);

            anim.SetBool("Turning", false);
            doBehavior = WALK;
        }
        if (animInfo.fullPathHash == Animator.StringToHash("Base Layer.PickUp")) { whichBehavior = PICKUP; }
        if (animInfo.fullPathHash == Animator.StringToHash("Base Layer.Thinking")) { whichBehavior = THINKING; }
        if (animInfo.fullPathHash == Animator.StringToHash("Base Layer.LookAround"))
        {
            timerLA += Time.deltaTime;
            if (timerLA > 0.2f && timerLA < 1.5f)
            {
                whichBehavior = LOOKAROUND;
                RobotTurning(
                    transform.position + from,
                    transform.position + Quaternion.Euler(0, 20, 0) * from);
            }
            else if (timerLA < 4)
                RobotTurning(
                    transform.position + Quaternion.Euler(0, 20, 0) * from,
                    transform.position + Quaternion.Euler(0, -20, 0) * from);
            else if (timerLA < 6)
            {
                RobotTurning(
                    transform.position + Quaternion.Euler(0, -20, 0) * from,
                    transform.position + from);
                doBehavior = WALK;
            }

            anim.SetBool("LookAround", false);
            anim.SetBool("Turning", false);
        }

        if (animInfo.fullPathHash == Animator.StringToHash("Base Layer.Appreciation"))
        {

            // 舞台の方向へ向かせる
            RobotTurning(transform.position + from, GameObject.Find("butai").transform.position);

            // ここで，拍手・手拍子状態なら，乱数をつかって一定確率でそのアニメーションフェーズに移るようにする
            if (Random.Range(0f, 1f) > 0.995f)
            {
                if (doBehavior == HANDCLAP)
                {
                    anim.SetBool("Handclap", true);
                    if (!audioSource.isPlaying) audioSource.PlayOneShot(handclapSound); Debug.Log("handclap");
                }
                else if (doBehavior == APPLAUSE)
                {
                    anim.SetBool("Applause", true);
                    if (!audioSource.isPlaying) audioSource.PlayOneShot(applauseSound);
                }
                whichBehavior = APPRECIATION;
            }
        }
        if (animInfo.fullPathHash == Animator.StringToHash("Base Layer.Handclap"))
        {
            whichBehavior = HANDCLAP;
            anim.SetBool("Handclap", false);
        }
        if (animInfo.fullPathHash == Animator.StringToHash("Base Layer.Applause"))
        {
            whichBehavior = APPLAUSE;
            anim.SetBool("Applause", false);
        }



        // 歩いているときだけ視線移動をするように
        if (whichBehavior == WALK)
        {
            if (GetComponent<HeadLookController>() != null)
                GetComponent<HeadLookController>().enabled = true;
        }
        else
        {
            if (GetComponent<HeadLookController>() != null)
                GetComponent<HeadLookController>().enabled = false;
        }
    }

    /// <summary>
    /// 指定された動作を行わせる
    /// </summary>
    /// <param name="doBehavior">行わせたい行動の番号</param>
    private void DoBehavior(int doBehavior)
    {
        switch (doBehavior)
        {
            case PICKUP:
                if (anim.GetBool("Turning") == false)
                {
                    anim.SetBool("Turning", true);
                    anim.SetBool("PickUp", true);
                }
                else
                {
                    anim.SetBool("PickUp", true);
                }
                break;
            case THINKING:
                if (anim.GetBool("Turning") == false)
                {
                    anim.SetBool("Turning", true);
                    anim.SetBool("Thinking", true);
                }
                else
                {
                    anim.SetBool("Thinking", true);
                }
                break;
            case LOOKAROUND:
                anim.SetBool("LookAround", true);
                break;
            case APPRECIATION:
                anim.SetBool("Appreciation", true);
                break;
            case HANDCLAP:
                anim.SetBool("Appreciation", true); // 一旦，Appreciation状態へ移行する
                break;
            case APPLAUSE:
                anim.SetBool("Appreciation", true); // 一旦，Appreciation状態へ移行する
                break;
            default:
                break;
        }
    }


    /// <summary>
    /// HeadBehavior を計算
    /// 客が各売り場エリアにいる間ずっと呼ばれる
    /// </summary>
    /// <param name="collider"></param>
    void OnTriggerStay(Collider collider)
    {

        // TimeInterval 秒間隔で、ほかの客を取得
        if (timer < TimeInterval) return;
        else
        {
            if (ApplyHeadBehaviour == true && whichBehavior == WALK)
            {

                // WalkEnd は無視
                if (collider.gameObject.tag == "WalkEnd") return;

                // 自身のタグを collider.gameObject.tag に設定
                gameObject.tag = collider.gameObject.tag;

                // player が近くにいれば，優先的にHBが発動する
                if (VectorDistance(transform.position, player.transform.position) < 3)
                {
                    //Debug.Log(transform.name + " is near the player!");
                    HerdBehavior((int)player.GetComponent<PlayerBehaviorText_VIVE>().whichBehavior);

                    return;
                }


                // 他客リストの初期化
                otherCustomerList.Clear();
                //Debug.Log("OnTriggerStay(Collider " + collider.gameObject.tag + " )");

                // 同エリアのほかの客を取得し、
                // 「自分がいるエリアオブジェクト(SecSphere_○○)」と「自分自身」を削除
                otherCustomerList.AddRange(GameObject.FindGameObjectsWithTag(collider.gameObject.tag));
                otherCustomerList.Remove(collider.gameObject);
                otherCustomerList.Remove(gameObject);


                // 自分以外に客がいないなら終了
                if (otherCustomerList.Count == 0) return;


                int tmp0 = 0, tmp1 = 0, num = 0;

                foreach (var customer in otherCustomerList)
                {
                    tmp0 = customer.GetComponent<RobotBehaviourScript_Fair2Ver>().whichBehavior;

                    // 舞台袖で，手拍子か拍手をしているキャラがいれば，それを伝播させる
                    if ((collider.gameObject.tag == "Sec_F_STA" || collider.gameObject.tag == "Sec_S_STA") &&
                        (tmp0 == HANDCLAP || tmp0 == APPLAUSE))
                    {
                        HerdBehavior(tmp0);
                        return;
                    }

                    // 他客の行動変数の総和を取る
                    tmp1 += tmp0;
                    //Debug.Log("customer[" + num + "] == " + customer.GetComponent<RobotBehaviourScript_FairVer>().whichBehavior);
                    //num++;
                }

                // 行動変数を平均したものを、自身の行動変数にしている
                if (Random.Range(0f, 1f) > 0.7f) HerdBehavior(Mathf.RoundToInt((float)tmp1 / num));

            }

            if (timer > TimeInterval) timer = 0;
        }
    }


    /// <summary>
    /// 行動変数を受け取り，HBの処理を行う
    /// </summary>
    /// <param name="hb">周囲のキャラの行動変数</param>
    void HerdBehavior(int hb)
    {
        // HBによってつられる際に，thinking→pickup，pickup→thinking
        // というように，行動が変化するようにする
        // (「買い物」概念の伝播)
        float randNumHb = Random.Range(0f, 1f);
        if (hb == PICKUP)
            hb = Mathf.RoundToInt(hb + randNumHb);
        else if (hb == THINKING)
            hb = Mathf.RoundToInt(hb - randNumHb);

        doBehavior = hb;

        // タイマーを初期化
        timer = 0;
    }



    /// <summary>
    ///  LookRotation を使用して回転、x および z の角度をゼロにし、Slerpで現在の回転とターゲットの間を補間する。
    ///  Slerp(開始時の回転、終了時の回転、 0-1 間の値で開始あるいは終了にどれだけ近いかを示す)
    ///  関数の結果は、値が 0 なら開始時点の回転と、1 なら終了時点の回転と、0.5 なら二つの回転の中間値と等しくなる。
    /// <param name="from">現在の位置情報</param>
    /// <param name="to">振り向かせるターゲットの位置情報</param>
    void RobotTurning(Vector3 from, Vector3 to)
    {
        // 滑らかに曲がる

        var newRotation = Quaternion.LookRotation(to - from).eulerAngles;
        newRotation.x = 0;
        newRotation.z = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(newRotation), Time.deltaTime * 1.6f);


        // 急に方向転換
        //transform.LookAt(new Vector3(to.x, from.y, to.z));

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
}
