using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// フィールド上(NavMesh)での客の動作を制御するためのスクリプト
/// 場所データなども扱う
/// </summary>
public class NavMeshofCustomer_Fair2Ver : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent agent;
    private GameObject player;
    private List<Transform> points = new List<Transform>();

    private Animator anim;

    // 行動記号列を読み込むか
    // WalkerCreator.cs から bool 値が渡される
    public bool readFileOrNot;
    // 読み込んだ行動記号列と
    // それを扱う配列群
    public string behavLine;
    string[] behavSymbols; string behav;
    string[] areaSymbols;
    char[] SPLIT_AREA = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q' };
    char[] SPLIT_BEHAV = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
    int pointsIndex = 0;
    int pointsIndexRem;
    int pointsCount;
    int POINT_NUM = 17;

    bool walkerOrNot = false;
    

    /// <summary>
    /// 客が現在どこにいるかなどを示す定数
    /// 0 : わたあめ屋(WAT)
    /// 1 : たこ焼き屋(TA)
    /// 2 : 射的(SHA)
    /// 3 : 焼きそば屋(YA)
    /// 4 : 海鮮焼き屋(KAI)
    /// 5 : 輪投げ(WAN)
    /// 6 : ヨーヨー釣り(YO)
    /// 7 : 金魚すくい(KI)
    /// 8 : かき氷屋(KAK)
    /// 9 : リンゴ飴屋(RI)
    /// 10: ファンタジー屋台1(FAN_1)
    /// 11: ファンタジー屋台2(FAN_2)
    /// 12: 曲がり角(COR)
    /// 13: 舞台前(STA_F)
    /// 14: 舞台横(STA_S)
    /// 15: 右側出口(RI_EXIT)
    /// 16: 下側出口(LO_EXIT)
    /// </summary>
    public int mode = 9;
    const int
        WAT = 0, TA = 1, SHA = 2, YA = 3, KAI = 4,
        WAN = 5, YO = 6, KI = 7, KAK = 8, RI = 9,
        FAN_1 = 10, FAN_2 = 11, 
        COR = 12, STA_F = 13, STA_S = 14,
        RI_EXIT = 15, LO_EXIT = 16;
    // Inspector上で自身とぶつかったエリアのタグを表示
    [SerializeField]
    new string tag;

    // 特定の行動を、1エリアで何回も繰り返し行わないようにするための変数
    bool doSpecificBehavior = false;
    float randNum;
    // 「head behaviourで動作が生じるのは、1エリアにつき1回まで」を実現する変数
    //public bool herdBehavLim;

    // 客の移動に必要な乱数
    float turningRand;

    // 一定時間周回すると、自動で退場してもらう。その時間をカウントする変数
    float timer = 0;
    float timer_col = 0;

    // lookaroundのためだけの変数
    AnimatorStateInfo animInfo;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("PlayerController");

        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();



        // ファイルを読まずにランダムに歩かせる場合
        if (readFileOrNot == false)
        {
            // ただの歩行者か、店の前をうろうろするかを決める
            randNum = Random.Range(0f, 1f);

            if (randNum < 0.5f)
            // 店の前をうろうろする
            {
                // point の個数だけループを回して SetDestination に登録する
                for (int i = 0; i < POINT_NUM; i++)
                {
                    points.Add(GameObject.Find("SecSphere").transform.Find("point" + i));
                }
                agent.SetDestination(points[(int)Mathf.Round(randNum * POINT_NUM)].position);
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
            // behavLineを「動作記号列だけの配列」「エリア記号だけの配列」に分ける
            // Splitの性質から
            //   behavSymbols[0] = (空), [1] = 121...
            //   areaSymbols[0] = K, [1] = ... , ... , [n] = 
            // のように格納される
            behavSymbols = behavLine.Split(SPLIT_AREA);
            areaSymbols = behavLine.Split(SPLIT_BEHAV, System.StringSplitOptions.RemoveEmptyEntries);

            // エリア記号をcustomerの歩行ルートにセットする
            FromAreaSymbolsToPoints(areaSymbols);
            Debug.Log("points[] num = " + pointsCount);
            agent.SetDestination(points[0].position);

        }


        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(this.transform.position, player.transform.position) < 2)
        {
            agent.speed = 0.05f;
            anim.speed = 0.6f;
        }
        else
        {
            agent.speed = 1f;
            anim.speed = 0.75f;
        }

        Debug.Log(agent.speed);

        animInfo = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);

        // タイマー
        timer += Time.deltaTime;
        timer_col += Time.deltaTime;

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
                // 歩くルートを決める乱数を生成
                turningRand = Random.Range(0f, 1f);

                // 一定時間経過で、出口(WalkEnd)まで自動で歩いてもらう
                if (timer > 240)
                {
                    if (turningRand > 0.5f)
                        agent.SetDestination(points[RI_EXIT].position);
                    else
                        agent.SetDestination(points[LO_EXIT].position);
                    return;
                }
                // 他の客との衝突でスピードがおかしくなって進まなくなったときは、
                // 一定時間で強制的に退場
                if (timer_col > 60)
                {
                    WalkerCreator.walkerList.Remove(gameObject);
                    Destroy(gameObject);
                }


                switch (mode)
                {
                    case WAT:
                        if (turningRand < 0.5f)
                            agent.SetDestination(points[TA].position);
                        else
                            agent.SetDestination(points[STA_S].position);
                        break;
                    case TA:
                        if (turningRand < 0.5f)
                            agent.SetDestination(points[WAT].position);
                        else
                            agent.SetDestination(points[SHA].position);
                        break;
                    case SHA:
                        if (turningRand < 0.5f)
                            agent.SetDestination(points[TA].position);
                        else
                            agent.SetDestination(points[YA].position);
                        break;
                    case YA:
                        if (turningRand < 0.5f)
                            agent.SetDestination(points[SHA].position);
                        else
                            agent.SetDestination(points[KAI].position);
                        break;
                    case KAI:
                        if (turningRand < 0.5f)
                            agent.SetDestination(points[YA].position);
                        else
                            agent.SetDestination(points[WAN].position);
                        break;
                    case WAN:
                        if (turningRand < 0.5f)
                            agent.SetDestination(points[KAI].position);
                        else
                            agent.SetDestination(points[YO].position);
                        break;
                    case YO:
                        if (turningRand < 0.5f)
                            agent.SetDestination(points[WAN].position);
                        else
                            agent.SetDestination(points[KI].position);
                        break;
                    case KI:
                        if (turningRand < 0.5f)
                            agent.SetDestination(points[YO].position);
                        else
                            agent.SetDestination(points[KAK].position);
                        break;
                    case KAK:
                        if (turningRand < 0.5f)
                            agent.SetDestination(points[KI].position);
                        else
                            agent.SetDestination(points[RI].position);
                        break;
                    case RI:
                        if (turningRand < 0.5f)
                            agent.SetDestination(points[KAK].position);
                        else
                            agent.SetDestination(points[FAN_1].position);
                        break;
                    case FAN_1:
                        if (turningRand < 0.5f)
                            agent.SetDestination(points[RI].position);
                        else
                            agent.SetDestination(points[FAN_2].position);
                        break;
                    case FAN_2:
                        if (turningRand < 0.5f)
                            agent.SetDestination(points[FAN_1].position);
                        else
                            agent.SetDestination(points[STA_F].position);
                        break;
                    case STA_F:
                        if (turningRand < 0.2f)
                            agent.SetDestination(points[STA_S].position);
                        else
                            agent.SetDestination(points[FAN_1].position);
                        break;
                    case STA_S:
                        if (turningRand < 0.2f)
                            agent.SetDestination(points[STA_F].position);
                        else
                            agent.SetDestination(points[TA].position);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                // 行動記号列ファイルを読み込んでいるなら
                // areaSymbolsの順番に各エリアを周回する
                pointsIndexRem = pointsIndex % pointsCount;
                agent.SetDestination(points[pointsIndexRem].position);

                pointsIndex = (pointsIndex + 1) % pointsCount;


                behav = behavSymbols[pointsIndexRem];

                // doSpecificBehavior初期化
                // 特定の行動記号がなければ下の if 節で即座に false になる
                doSpecificBehavior = true;
            }
        }


        // 行動記号列内に特定の行動記号があれば、その行動を一定の確率で起こす
        if (readFileOrNot == true && doSpecificBehavior == true)
        {

            // Stop 記号がある場合
            if (behav.Contains("2"))
            {

                // 同じオブジェクト(Customer)のスクリプトを参照
                RobotBehaviourScript_FairVer r = GetComponent<RobotBehaviourScript_FairVer>();

                r.doBehavior = 2;
                doSpecificBehavior = false;

            }
            // PickUp 記号がある場合
            if (behav.Contains("4"))
            {
                randNum = Random.Range(0f, 1f);


                // 同じオブジェクト(Customer)のスクリプトを参照
                RobotBehaviourScript_FairVer r = GetComponent<RobotBehaviourScript_FairVer>();

                r.doBehavior = 4;
                doSpecificBehavior = false;

            }
            // Thinking 記号がある場合
            if (behav.Contains("5"))
            {

                randNum = Random.Range(0f, 1f);

                // 同じオブジェクト(Customer)のスクリプトを参照
                RobotBehaviourScript_FairVer r = GetComponent<RobotBehaviourScript_FairVer>();

                r.doBehavior = 5;
                doSpecificBehavior = false;

            }
            // lookaround 記号がある場合
            if (behav.Contains("6"))
            {

                randNum = Random.Range(0f, 1f);

                // 同じオブジェクト(Customer)のスクリプトを参照
                RobotBehaviourScript_FairVer r = GetComponent<RobotBehaviourScript_FairVer>();

                r.doBehavior = 6;
                doSpecificBehavior = false;

            }
            else
            {
                // 特定の行動記号がなければ、
                // 次のエリアへ移動するまでこの if 節は実行しない
                doSpecificBehavior = false;
            }
        }

    }

    /// <summary>
    /// エリアにぶつかったときに呼ばれる
    /// </summary>
    /// <param name="collider"></param>
    void OnTriggerEnter(Collider collider)
    {
        tag = collider.tag;

        // timer_col を初期化
        timer_col = 0;
        // herdBehavLim を初期化
        //herdBehavLim = true;

        // 各セクションにぶつかるごとに mode を切り替える
        switch (tag)
        {
            case "Sec_WAT":
                mode = WAT;
                break;
            case "Sec_TA":
                mode = TA;
                break;
            case "Sec_SHA":
                mode = SHA;
                break;
            case "Sec_YA":
                mode = YA;
                break;
            case "Sec_KAI":
                mode = KAI;
                break;
            case "Sec_WAN":
                mode = WAN;
                break;
            case "Sec_YO":
                mode = YO;
                break;
            case "Sec_KI":
                mode = KI;
                break;
            case "Sec_KAK":
                mode = KAK;
                break;
            case "Sec_RI":
                mode = RI;
                break;
            case "Sec_FAN_1":
                mode = FAN_1;
                break;
            case "Sec_FAN_2":
                mode = FAN_2;
                break;
            case "Sec_F_STA":
                mode = STA_F;
                break;
            case "Sec_S_STA":
                mode = STA_S;
                break;
            case "Sec_COR":
                // もしただの歩行者で左方向に歩いていれば、CORで下方向に転回して出口へ
                // そうでなければ右方向に転回して出口へ
                if (walkerOrNot)
                {
                    if(Vector3.Angle(transform.forward, new Vector3(1,0,0)) < 45)
                        agent.SetDestination(GameObject.Find("SecSphere").transform.Find("point" + LO_EXIT).position);
                    else
                        agent.SetDestination(GameObject.Find("SecSphere").transform.Find("point" + RI_EXIT).position);
                }
                mode = COR;
                break;
            // 道路の端まで来てしまえば、その歩行者を消す
            case "WalkEnd":
                // 別のオブジェクト(Sphere)のスクリプトを参照する場合
                // 静的変数は「クラス名.変数名」とすれば直接値の読み書きが可能
                WalkerCreator.walkerList.Remove(gameObject);
                Destroy(gameObject);
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
        

        // Turning is true なら、いったん動きを止める
        if (anim.GetBool("Turning") == true ||
            anim.GetBool("PickUp") == true ||
            anim.GetBool("Thinking") == true ||
            animInfo.fullPathHash == Animator.StringToHash("Base Layer.LookAround"))
        {
            agent.velocity = Vector3.zero;
            //agent.updatePosition = false;
            agent.updateRotation = false;
            agent.Stop();
        }
        else
        {
            //agent.updatePosition = true;
            agent.updateRotation = true;
            agent.Resume();
        }
    }

    void FromAreaSymbolsToPoints(string[] areaSymbols)
    {
        foreach (var areaSymbol in areaSymbols)
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

}
