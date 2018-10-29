using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MyCommonConst;

public class WalkerCreator : MonoBehaviour {

    /// <summary>
    /// 歩行者を生成するプログラム
    /// </summary>
    [SerializeField] GameObject originalWalker;
    [SerializeField] BehaviourScriptReader bsr;
    public static List<GameObject> walkerList = new List<GameObject>();

    [Header("Customerに関する変数")]

    [Tooltip("フィールド内に存在するCustomerの人数の上限")]
    public int walkerNum;
    [Tooltip("客の生成時間間隔")]
    public float timeInterval;

    [Tooltip("客の服装(マテリアル)")]
    [SerializeField]
    Material liamMat, liamMat_butai, liamMat_passerby;

    int customerGroup = MyConst.GROUP_SHOPPING;


    // ※jikkenn2用　camJikkenn2番目以外の客のカメラコンポーネントを無効にするのに必要な変数
    //public int camJikkenn2;


    int i = 0;

    // 生成する場所・向きを決めるランダム変数
    Vector3 randPos;
    Quaternion randDir;

    // 生成する時間間隔
    float timer = 0;


    // Use this for initialization
    void Start()
    {
        //Debug.Log("walkerList.Count = " + walkerList.Count);
    }

    // Update is called once per frame
    void Update()
    {


        timer += Time.deltaTime;
        // 客の人数が walkerNum 人以下のとき、timeInterval秒経過で一人生成
        // (構造上、条件節は '<')
        if (walkerList.Count < walkerNum && timer >= timeInterval)
        {
            timer = 0;

            if (i < bsr.behavLineList.Count)
                customerGroup = bsr.behavLineList[i].WhichCustomerGroup();

            // Customerを生成

            // Instantiateする場所を指定(Fair2_Ver)
            if (Random.Range(0f, 1f) < 0.5f)
            {
                // 下側出口から出現
                randPos = new Vector3(Random.Range(5f, 8f), 0.1f, 18f);
                randDir = Quaternion.Euler(new Vector3(0, 180f, 0));
            }
            else
            {
                // 右側出口から出現
                randPos = new Vector3(-24f, 0.1f, Random.Range(-1f, 1f));
                randDir = Quaternion.Euler(new Vector3(0, 90f, 0));
            }

            // 位置を指定したうえでInstantiate
            GameObject walker = Instantiate(originalWalker, randPos, randDir) as GameObject;

            // 実験2で必要になる
            //if(i != camJikkenn2) walker.transform.FindChild("Camera").GetComponent<Camera>().enabled = false;
            
            // 生成した Customer のオブジェクトのスクリプトを参照
            NavMeshofCustomer_Fair2Ver nmc = walker.GetComponent<NavMeshofCustomer_Fair2Ver>();
            RobotBehaviourScript_Fair2Ver rbs = walker.GetComponent<RobotBehaviourScript_Fair2Ver>();


            // 行動記号列ファイルの行数分だけ、walker には behavLineList の要素を渡す
            if (i < bsr.behavLineList.Count)
            {
                Debug.Log("b.behavLineList.Count = " + bsr.behavLineList.Count);
                // BehaviourScriptReader(同じCustomerCreator)からbehavLineListを得て、
                // その i 番目の記号列を customer 生成と同時に渡す
                // また、そのエージェントの ReadFile も true にしておく
                nmc.behavLine = bsr.behavLineList[i];
                nmc.readFileOrNot = true;
                rbs.customerGroup = customerGroup;

                Debug.Log((i + 1) + "人目の行動記号列 : " + bsr.behavLineList[i]);
            }
            else
                rbs.customerGroup = MyConst.GROUP_PASSERBY;

            // マテリアル変更
            ReplaceMaterial(walker, rbs.customerGroup, liamMat, liamMat_butai, liamMat_passerby);

            walkerList.Add(walker);

            i++;

        }
    }

    private void ReplaceMaterial(GameObject walker, int customerGroup, Material liamMat, Material liamMat_butai, Material liamMat_passerby)
    {
        if (customerGroup == MyConst.GROUP_SHOPPING)
            walker.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<Renderer>().material = liamMat;
        else if (customerGroup == MyConst.GROUP_BUTAI)
            walker.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<Renderer>().material = liamMat_butai;
        else if (customerGroup == MyConst.GROUP_PASSERBY)
            walker.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<Renderer>().material = liamMat_passerby;
    }

}
