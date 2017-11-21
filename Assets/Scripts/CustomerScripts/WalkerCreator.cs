using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WalkerCreator : MonoBehaviour {

    /// <summary>
    /// 歩行者を生成するプログラム
    /// </summary>
    public GameObject originalWalker;
    public static List<GameObject> walkerList = new List<GameObject>();

    [Header("Customerに関する変数")]

    [Tooltip("フィールド内に存在するCustomerの人数の上限")]
    public int walkerNum;
    [Tooltip("客の生成時間間隔")]
    public float timeInterval;

    // ※jikkenn2用　camJikkenn2番目以外の客のカメラコンポーネントを無効にするのに必要な変数
    public int camJikkenn2;


    int i = 0;

    // 生成する場所・向きを決めるランダム変数
    float randNum;
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

            randNum = Random.Range(0f, 1f);

            // Customerを生成

            // Instantiateする場所を指定(Fair2_Ver)
            if (randNum < 0.5f)
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

            // 同じオブジェクト(WalkerCreator)のスクリプトを参照
            BehaviourScriptReader b = GetComponent<BehaviourScriptReader>();
            // 生成した Customer のオブジェクトのスクリプトを参照
            NavMeshofCustomer_Fair2Ver n = walker.GetComponent<NavMeshofCustomer_Fair2Ver>();


            // 行動記号列ファイルの行数分だけ、walker には behavLineList の要素を渡す
            if (i < b.behavLineList.Count)
            {
                Debug.Log("b.behavLineList.Count = " + b.behavLineList.Count);
                // BehaviourScriptReader(同じCustomerCreator)からbehavLineListを得て、
                // その i 番目の記号列を customer 生成と同時に渡す
                // また、そのエージェントの ReadFile も true にしておく
                n.behavLine = b.behavLineList[i];
                n.readFileOrNot = true;

                Debug.Log((i + 1) + "人目の行動記号列 : " + b.behavLineList[i]);
            }

            walkerList.Add(walker);

            i++;

        }
    }

}
