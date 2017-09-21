using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This script manages behaviours of the Robot(ex, which the robot is walking or not).
/// モデルのアニメーションを制御するためのスクリプト
/// HeadBehaviorの制御もここで行っている
/// </summary>
public class RobotBehaviourScript_Fair2Ver : MonoBehaviour
{

    // Customerのtransform情報を取得
    Transform transform;

    Animator anim;

    // head behaviour を適用するかどうか
    public bool ApplyHeadBehaviour;


    // テスト用のデフォルト客であるかどうか
    //public bool defaultCustomer;

    // 自身がどの動作をしているかを表す定数
    // others            : 0
    // walk              : 1
    // stop              : 2
    // turing            : 3
    // pick up           : 4
    // thinking          : 5
    // look around       : 6
    const int OTHERS = 0,
              WALK = 1,
              STOP = 2,
              TURNING = 3,
              PICKUP = 4,
              THINKING = 5,
              LOOKAROUND = 6;
    public int whichBehavior = WALK;

    // 特定の行動を起こすための変数
    public int doBehavior;

    // アニメーション関連の変数
    AnimatorStateInfo animInfo;
    Vector3 from;
    [SerializeField]
    private float Speed = 1;

    float randNum = 0;

    // HerdBehaviorを扱うための変数
    List<GameObject> otherCustomerList = new List<GameObject>();
    int headBehavior = 0;

    // HerdBehavior を計算するための変数
    // TimeInterval 秒ごとに HerdBehavior を計算している
    float timer = 0, TimeInterval = 0.5f;
    // 周囲を見渡す動作を行うのに必要なtime変数
    float timerLA = 0;

    // 店員の位置リスト
    private List<Vector3> clerkPos = new List<Vector3>();



    // Use this for initialization
    void Start()
    {
        transform = GetComponent<Transform>();

        // アニメーターを取得
        anim = GetComponent<Animator>();

        // 店員の位置を取得
        for (int i = 0; i < 10; i++)
        {
            clerkPos.Add(GameObject.Find("Clerks").transform.Find("Clerk" + i).transform.position);
        }
        
        Debug.Log("ApplyHeadBehaviour is " + ApplyHeadBehaviour);
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
            from = transform.forward;
        }
        if (animInfo.fullPathHash == Animator.StringToHash("Base Layer.Turning"))
        {
            // どの店の前にいるかを取得する
            NavMeshofCustomer_Fair2Ver mode = GetComponent<NavMeshofCustomer_Fair2Ver>();

            //RobotTurning(
            //    transform.position + from,
            //    transform.position + Quaternion.Euler(0, -90, 0) * from);

            // 店員のほうを向く
            //Debug.Log("mode = " + mode.mode);
            //Debug.Log("clerkPos = " + clerkPos[mode.mode]);
            //Debug.Log("pos      = " + transform.position);
            if (mode.mode <= 9)
            {
                RobotTurning(transform.position + from, clerkPos[mode.mode]);
            }

        }
        if (animInfo.fullPathHash == Animator.StringToHash("Base Layer.ToFoward"))
        {
            // どの店の前にいるかを取得する
            NavMeshofCustomer_Fair2Ver mode = GetComponent<NavMeshofCustomer_Fair2Ver>();

            //RobotTurning(
            //    transform.position + Quaternion.Euler(0, -90, 0) * from,
            //    transform.position + from);

            // 向き直る
            if (mode.mode <= 9) RobotTurning(clerkPos[mode.mode], transform.position + from);

            anim.SetBool("Turning", false);
            doBehavior = 0;
            //Debug.Log(string.Format("<color=green>ToFoward</color>"));
        }
        if (animInfo.fullPathHash == Animator.StringToHash("Base Layer.PickUp"))
        {
            whichBehavior = PICKUP;
        }
        if (animInfo.fullPathHash == Animator.StringToHash("Base Layer.Thinking"))
        {
            whichBehavior = THINKING;
        }
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
    }

    /// <summary>
    /// 指定された動作を行わせる
    /// </summary>
    /// <param name="doBehavior"></param>
    private void DoBehavior(int doBehavior)
    {
        switch (doBehavior)
        {
            case STOP:
                anim.SetTrigger("Idle");
                break;
            case PICKUP:
                if (anim.GetBool("Turning") == false)
                {
                    //Debug.Log("Turning and pick up.");
                    anim.SetBool("Turning", true);
                }
                else
                {
                    //Debug.Log("<color=green>pick up.</color>");
                    anim.SetBool("PickUp", true);
                }
                break;
            case THINKING:
                if (anim.GetBool("Turning") == false)
                {
                    //Debug.Log("Turning and Thinking.");
                    anim.SetBool("Turning", true);
                }
                else
                {
                    //Debug.Log("<color=green>Thinking.</color>");
                    anim.SetBool("Thinking", true);
                }
                break;
            case LOOKAROUND:
                //Debug.Log("lookaround");
                anim.SetBool("LookAround", true);
                break;
            default:
                break;
        }
    }


    /// <summary>
    /// 客が各売り場エリアにいる間ずっと呼ばれる
    /// ここで HeadBehavior を計算している
    /// </summary>
    /// <param name="collider"></param>
    void OnTriggerStay(Collider collider)
    {

        // 0.3秒間隔で、ほかの客を取得
        if (timer < TimeInterval) return;

        if (ApplyHeadBehaviour == true &&
            whichBehavior == WALK)
        {

            // WalkEnd は無視
            if (collider.gameObject.tag == "WalkEnd") return;

            // 他客リストの初期化
            otherCustomerList.Clear();
            //Debug.Log("OnTriggerStay(Collider " + collider.gameObject.tag + " )");

            // 同エリアのほかの客を取得し、
            // 「自分がいるエリアオブジェクト(SecSphere_○○)」と「自分自身」を削除
            otherCustomerList.AddRange(GameObject.FindGameObjectsWithTag(collider.gameObject.tag));
            otherCustomerList.Remove(collider.gameObject);
            otherCustomerList.Remove(gameObject);



            // 自身のタグを collider.gameObject.tag に設定
            gameObject.tag = collider.gameObject.tag;

            // 自分以外に客がいないなら終了
            if (otherCustomerList.Count == 0) return;

            //Debug.Log("otherCustomerList.Count == " + otherCustomerList.Count);

            //int num0 = 0;
            //foreach (var a in otherCustomerList)
            //{
            //    Debug.Log("otherCustomer[" + num0 + "] = " + a);
            //    num0++;
            //}


            int i = 0, num = 0;

            foreach (var customer in otherCustomerList)
            {
                // 他客の行動変数の総和を取る
                i += customer.GetComponent<RobotBehaviourScript_Fair2Ver>().whichBehavior;
                //Debug.Log("customer[" + num + "] == " + customer.GetComponent<RobotBehaviourScript_FairVer>().whichBehavior);
                num++;
            }

            randNum = Random.Range(0f, 1f);

            // 自身のdoBehaviorに、とるべき行動の変数を入れる
            // 行動変数を平均したものを、自身の行動変数にしている
            if (randNum > 0.7f)
            {
                doBehavior = (int)Mathf.Round((float)i / num);
                //Debug.Log("HeadBehavior == " + doBehavior);
            }

            // タイマーを初期化
            if (timer >= TimeInterval) timer = 0;
        }
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
}
