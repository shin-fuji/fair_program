using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using System.Text.RegularExpressions;
using MyCommonConst;    // 自作ネームスペースは，同じディレクトリに入れないとusingが使えない

/// <summary>
/// フィールド上(NavMesh)での客の動作を制御するためのスクリプト
/// 場所データなども扱う
/// 
/// TODO
/// ・現在，拍手を行動番号9に振り分けているが，その必要があるかどうか考える．ダンス終了後に必ず拍手させるという手もある．
/// 
/// </summary>
public class NavMeshofCustomer_Fair2Ver : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject player;
    private List<Transform> points = new List<Transform>();

    private Animator anim;

    public bool readFileOrNot;  // 行動記号列を読み込むか否か．WalkerCreator.cs から bool 値が渡される
    public string behavLine;    // 読み込んだ行動記号列と，それを扱う配列群
    string[] behavSymbols0;
    List<List<string>> behavSymbolList = new List<List<string>>();
    List<string> behavSymbols = new List<string>();
    List<string> areaSymbolList = new List<string>();
    int pointsIndex = 0;
    int pointsIndexRem;
    int pointsCount;
    int POINT_NUM = 17;

    bool walkerOrNot = false;


    public int mode = 9;
    
    
    bool doSpecificBehavior = false;    // 特定の行動を、1エリアで何回も繰り返し行わないようにするための変数
    float randNum;
    
    //public bool herdBehavLim;           // 「head behaviourで動作が生じるのは、1エリアにつき1回まで」を実現する変数

    float turningRand;  // 客の移動に必要な乱数
    float timer = 0;    // 一定時間周回すると、自動で退場してもらう。その時間をカウントする変数

    AnimatorStateInfo animInfo; // lookaroundのための変数

    RobotBehaviourScript_Fair2Ver r;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("PlayerController");

        agent = GetComponent<NavMeshAgent>();

        // 同じオブジェクト(Customer)のスクリプトを参照
        r = GetComponent<RobotBehaviourScript_Fair2Ver>();



        // ファイルを読まずにランダムに歩かせる場合
        if (readFileOrNot == false)
        {

            if (Random.Range(0f, 1f) < 0.5f)
            // 店の前をうろうろする
            {
                // point の個数だけループを回して SetDestination に登録する
                for (int i = 0; i < POINT_NUM; i++)
                {
                    points.Add(GameObject.Find("SecSphere").transform.Find("point" + i));
                }
                agent.SetDestination(points[Mathf.RoundToInt(randNum * POINT_NUM)].position);
            }
            else
            // ただの歩行者
            {
                walkerOrNot = true;
            }

        }
        // ファイルを読んで行動記号列に従って歩かせるとき
        else
        {
            // 配列，リストは勝手に参照渡しになるわけではない
            SplitBehavLine(behavLine, ref behavSymbolList, ref areaSymbolList); 
            //foreach (var behavSymbols in behavSymbolList.Select((v, i) => new { v, i }))
            //{
            //    Debug.Log(behavSymbols.i + "番目の行動記号列：");
            //    behavSymbols.v.ShowListContentsInTheDebugLog();
            //}
            //areaSymbolList.ShowListContentsInTheDebugLog();

            // エリア記号をcustomerの歩行ルートにセットする
            FromAreaSymbolsToPoints(areaSymbolList);

            agent.SetDestination(points[0].position);

        }


        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        animInfo = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);

        // タイマー
        timer += Time.deltaTime;

        SetAnim();

        if (walkerOrNot == true)
        {
            transform.Translate(transform.forward * Time.deltaTime, Space.World);//前に移動
            return;
        }



        if (agent.remainingDistance < agent.stoppingDistance)
        {
            //if (agent.pathStatus != NavMeshPathStatus.PathInvalid) return;
            // 行動記号列ファイルを読み込んでいないなら
            // ランダムに行動を決める
            if (readFileOrNot == false)
            {

                switch (mode)
                {
                    case MyConst.WAT:
                        SetDestination(timer, MyConst.TA, MyConst.STA_S);
                        //if (turningRand < 0.5f)
                        //    agent.SetDestination(points[TA].position);
                        //else
                        //    agent.SetDestination(points[STA_S].position);
                        break;
                    case MyConst.TA:
                        SetDestination(timer, MyConst.WAT, MyConst.SHA);
                        break;
                    case MyConst.SHA:
                        SetDestination(timer, MyConst.TA, MyConst.YA);
                        break;
                    case MyConst.YA:
                        SetDestination(timer, MyConst.SHA, MyConst.KAI);
                        break;
                    case MyConst.KAI:
                        SetDestination(timer, MyConst.YA, MyConst.WAN);
                        break;
                    case MyConst.WAN:
                        SetDestination(timer, MyConst.KAI, MyConst.YO);
                        break;
                    case MyConst.YO:
                        SetDestination(timer, MyConst.WAN, MyConst.KI);
                        break;
                    case MyConst.KI:
                        SetDestination(timer, MyConst.YO, MyConst.KAK);
                        break;
                    case MyConst.KAK:
                        SetDestination(timer, MyConst.KI, MyConst.RI);
                        break;
                    case MyConst.RI:
                        SetDestination(timer, MyConst.KAK, MyConst.FAN_1);
                        break;
                    case MyConst.FAN_1:
                        SetDestination(timer, MyConst.RI, MyConst.FAN_2);
                        break;
                    case MyConst.FAN_2:
                        SetDestination(timer, MyConst.FAN_1, MyConst.STA_F);
                        break;
                    case MyConst.COR:
                        SetDestination(timer, MyConst.YO, MyConst.KI);
                        break;
                    case MyConst.STA_F:
                        SetDestination(timer, MyConst.STA_S, MyConst.FAN_1);
                        break;
                    case MyConst.STA_S:
                        SetDestination(timer, MyConst.STA_F, MyConst.TA);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                // 行動記号列ファイルを読み込んでいるなら
                // areaSymbols0の順番に各エリアを周回する
                pointsIndexRem = pointsIndex % pointsCount;
                agent.SetDestination(points[pointsIndexRem].position);
                //Debug.Log("次の目的地：" + areaSymbolList[pointsIndexRem]);

                pointsIndex = (pointsIndex + 1) % pointsCount;
                
                behavSymbols = behavSymbolList[pointsIndexRem];
                //Debug.Log("次の行動記号列：");
                //behavSymbols.ShowListContentsInTheDebugLog();

                // doSpecificBehavior初期化
                // 特定の行動記号がなければ下の if 節で即座に false になる
                doSpecificBehavior = true;
            }
        }


        // 行動記号列内に特定の行動記号があれば、その行動を起こす
        if (readFileOrNot == true && doSpecificBehavior == true)
        {

            // 行動記号列内の行動を行う
            StartCoroutine(IsBehaviorDone(behavSymbols));

            // 特定の行動記号がなければ、
            // 次のエリアへ移動するまでこの if 節は実行しない
            doSpecificBehavior = false;
        }

        if (agent.pathStatus != NavMeshPathStatus.PathInvalid)
        {
            if (r.animInfo.fullPathHash == Animator.StringToHash("Base Layer.Walk"))
            {
                agent.isStopped = false;
            }
            else
            {
                // 行動を行うときは移動させない
                agent.isStopped = true;
            }
        }

    }

    /// <summary>
    /// エリアにぶつかったときに呼ばれる
    /// </summary>
    /// <param name="collider"></param>
    void OnTriggerEnter(Collider collider)
    {
        string tag = collider.tag;
        
        // herdBehavLim を初期化
        //herdBehavLim = true;

        // 各セクションにぶつかるごとに mode を切り替える
        switch (tag)
        {
            case "Sec_WAT":
                mode = MyConst.WAT;
                break;
            case "Sec_TA":
                mode = MyConst.TA;
                break;
            case "Sec_SHA":
                mode = MyConst.SHA;
                break;
            case "Sec_YA":
                mode = MyConst.YA;
                break;
            case "Sec_KAI":
                mode = MyConst.KAI;
                break;
            case "Sec_WAN":
                mode = MyConst.WAN;
                break;
            case "Sec_YO":
                mode = MyConst.YO;
                break;
            case "Sec_KI":
                mode = MyConst.KI;
                break;
            case "Sec_KAK":
                mode = MyConst.KAK;
                break;
            case "Sec_RI":
                mode = MyConst.RI;
                break;
            case "Sec_FAN_1":
                mode = MyConst.FAN_1;
                break;
            case "Sec_FAN_2":
                mode = MyConst.FAN_2;
                break;
            case "Sec_F_STA":
                mode = MyConst.STA_F;
                break;
            case "Sec_S_STA":
                mode = MyConst.STA_S;
                break;
            case "Sec_COR":
                // もしただの歩行者で左方向に歩いていれば、CORで下方向に転回して出口へ
                // そうでなければ右方向に転回して出口へ
                if (walkerOrNot)
                {
                    if(Vector3.Angle(transform.forward, new Vector3(1,0,0)) < 45)
                        agent.SetDestination(GameObject.Find("SecSphere").transform.Find("point" + MyConst.LO_EXIT).position);
                    else
                        agent.SetDestination(GameObject.Find("SecSphere").transform.Find("point" + MyConst.RI_EXIT).position);
                }
                mode = MyConst.COR;
                break;
            case "WalkEnd":
                CustomerLeaves(gameObject);
                break;
            default:
                break;
        }
    }

    void SetAnim()
    {
        if (agent.pathStatus != UnityEngine.AI.NavMeshPathStatus.PathInvalid)
        {
            return;
        }


        // Turning is true または Appreciation is true なら、いったん動きを止める
        if (anim.GetBool("Turning") == true ||
            anim.GetBool("PickUp") == true ||
            anim.GetBool("Thinking") == true ||
            animInfo.fullPathHash == Animator.StringToHash("Base Layer.LookAround") ||
            anim.GetBool("Appreciation") == true)
        {
            agent.velocity = Vector3.zero;
            //agent.updatePosition = false;
            agent.updateRotation = false;
            agent.isStopped = true;
        }
        else
        {
            //agent.updatePosition = true;
            agent.updateRotation = true;
            agent.isStopped = false;
        }
    }

    void FromAreaSymbolsToPoints(List<string> areaSymbolList)
    {
        foreach (var areaSymbol in areaSymbolList)
        {
            switch (areaSymbol)
            {
                case "A":
                    points.Add(GameObject.Find("SecSphere").transform.Find("point0"));
                    break;
                case "B":
                    points.Add(GameObject.Find("SecSphere").transform.Find("point1"));
                    break;
                case "C":
                    points.Add(GameObject.Find("SecSphere").transform.Find("point2"));
                    break;
                case "D":
                    points.Add(GameObject.Find("SecSphere").transform.Find("point3"));
                    break;
                case "E":
                    points.Add(GameObject.Find("SecSphere").transform.Find("point4"));
                    break;
                case "F":
                    points.Add(GameObject.Find("SecSphere").transform.Find("point5"));
                    break;
                case "G":
                    points.Add(GameObject.Find("SecSphere").transform.Find("point6"));
                    break;
                case "H":
                    points.Add(GameObject.Find("SecSphere").transform.Find("point7"));
                    break;
                case "I":
                    points.Add(GameObject.Find("SecSphere").transform.Find("point8"));
                    break;
                case "J":
                    points.Add(GameObject.Find("SecSphere").transform.Find("point9"));
                    break;
                case "K":
                    points.Add(GameObject.Find("SecSphere").transform.Find("point10"));
                    break;
                case "L":
                    points.Add(GameObject.Find("SecSphere").transform.Find("point11"));
                    break;
                case "M":
                    points.Add(GameObject.Find("SecSphere").transform.Find("point12"));
                    break;
                case "N":
                    points.Add(GameObject.Find("SecSphere").transform.Find("point13"));
                    break;
                case "O":
                    points.Add(GameObject.Find("SecSphere").transform.Find("point14"));
                    break;
                case "P":
                    points.Add(GameObject.Find("SecSphere").transform.Find("point15"));
                    break;
                case "Q":
                    points.Add(GameObject.Find("SecSphere").transform.Find("point16"));
                    break;
                default:
                    //Debug.Log("FromAreaSymbolsToPoints Error : 予期しない文字が areaSymbols に存在 → " + areaSymbol);
                    break;
            }
        }
        // 周回するべき座標の個数をカウント
        pointsCount = points.Count;
    }

    /// <summary>
    /// 入場客を退場させる(消す)関数
    /// 
    /// 別のオブジェクト(Sphere)のスクリプトを参照する場合
    /// 静的変数は「クラス名.変数名」とすれば直接値の読み書きが可能
    /// </summary>
    /// <param name="customer">退場させたい入場客</param>
    void CustomerLeaves(GameObject customer)
    {
        WalkerCreator.walkerList.Remove(customer);
        Destroy(customer);
    }

    /// <summary>
    /// 動き回る入場客に対して，ランダムに行き先を設定する関数
    /// </summary>
    /// <param name="timer">タイマー</param>
    /// <param name="DES0">移動先を示す定数0</param>
    /// <param name="DES1">移動先を示す定数1</param>
    void SetDestination(float timer, int DES0, int DES1)
    {
        // 歩くルートを決める乱数を生成
        turningRand = Random.Range(0f, 1f);

        // 一定時間経過で、出口(WalkEnd)まで自動で歩いてもらう
        if (timer > 240)
        {
            if (turningRand > 0.5f)
                agent.SetDestination(points[MyConst.RI_EXIT].position);
            else
                agent.SetDestination(points[MyConst.LO_EXIT].position);
            return;
        }


        if (DES0 == MyConst.STA_F || DES0 == MyConst.STA_S)
        {
            if (turningRand < 0.2f)
                agent.SetDestination(points[DES0].position);
            else
                agent.SetDestination(points[DES1].position);
        }
        else
        {
            if (turningRand < 0.5f)
                agent.SetDestination(points[DES0].position);
            else
                agent.SetDestination(points[DES1].position);
        }
    }


    /// <summary>
    /// 読み込んだ行動記号列を，「場所記号」「行動記号」に分け，
    /// それぞれ別のstring型行列に格納する
    /// 
    /// 具体例
    /// 
    /// behavLine = 0,A,12,3,7,B,5,2,10,AC,0,AB,3,H,... の場合，
    /// behavSymbolList[0] = list{0}, [1] = List{12, 3, 7}, [2] = List{5, 2, 10}, ...
    /// areaSymbolList[0] = A, [1] = B , ... 
    /// のように格納される
    /// </summary>
    /// <param name="behavLine">読み込んだ行動記号列</param>
    /// <param name="behavSymbolList">行動記号のリスト(実際にはリストのリスト)</param>
    /// <param name="areaSymbolList">場所記号のリスト</param>
    void SplitBehavLine(string behavLine, ref List<List<string>> behavSymbolList, ref List<string> areaSymbolList)
    {
        int bSLindex = 0;
        
        string[] behavLineSymbolArr = behavLine.Split(',');
        //Debugger.Array(behavLineSymbolArr);

        // behavSymbolList の bSLindex 番目にアクセスするために，new でメモリを確保する．
        behavSymbolList.Add(new List<string>());

        foreach (var symbol in behavLineSymbolArr)
        {
            if (IsBehavSymbol(symbol))
            {
                behavSymbolList[bSLindex].Add(symbol);
            }
            else if (IsAreaSymbol(symbol))
            {
                areaSymbolList.Add(symbol);
                behavSymbolList.Add(new List<string>());
                bSLindex++;
            }
        }
    }


    /// <summary>
    /// 文字列が行動記号かどうか(半角数字かどうか)を判別する
    /// 
    /// 行動記号列なら true，そうでなければ false を返す
    /// </summary>
    /// <param name="target">判別すべき文字列</param>
    /// <returns></returns>
    static bool IsBehavSymbol(string target)
    {
        return new Regex("^[0-9]+$").IsMatch(target);
    }

    /// <summary>
    /// 文字列が場所記号かどうか(大文字の半角アルファベットかどうか)を判別する
    /// 
    /// 場所記号列なら true，そうでなければ false を返す
    /// </summary>
    /// <param name="target">判別すべき文字列</param>
    /// <returns></returns>
    static bool IsAreaSymbol(string target)
    {
        return new Regex("^[A-Z]+$").IsMatch(target);
    }


    /// <summary>
    /// 行動記号列を読み，順番に行動を行わせる関数
    /// </summary>
    /// <returns></returns>
    IEnumerator IsBehaviorDone(List<string> behavSymbols)
    {
        foreach (var behavSymbol in behavSymbols)
        {
            // 行動記号を int に変換
            int behavSymInt = int.Parse(behavSymbol);

            //Debug.Log("bs is " + behavSymInt);

            // Default 記号がある場合，移動間でどの動作も行わない
            if (behavSymInt == 0) break;

            // ここに，一秒ごとに行動状態を確認するコルーチンを入れる
            // WALK に戻っていれば次の行動へ
            while (true)
            {
                // 0～1秒毎に行動確認
                if (r.whichBehavior == 1) break;
                yield return new WaitForSeconds(1f);
            }
            r.doBehavior = behavSymInt;
            yield return new WaitForSeconds(1f);
            //Debug.Log("r.doBehavior = " + behavSymInt);
        }
    }

}
