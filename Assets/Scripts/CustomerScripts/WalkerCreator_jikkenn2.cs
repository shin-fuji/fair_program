﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WalkerCreator_jikkenn2 : MonoBehaviour
{

    /// <summary>
    /// 歩行者を生成するプログラム
    /// </summary>
    public GameObject originalWalker;
    public static List<GameObject> walkerList = new List<GameObject>();

    public int walkerNum;

    // 客の生成時間間隔
    public float timeInterval;


    int i = 0;

    // 生成する場所を決めるランダム変数
    float randNum;
    float randPos;

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
            randPos = Random.Range(-4f, 1f);

            // Customerを生成
            GameObject walker = Instantiate(originalWalker);

            // 同じオブジェクト(CustomerCreator)のスクリプトを参照
            BehaviorScriptReader_jikkenn2 b = GetComponent<BehaviorScriptReader_jikkenn2>();
            // 生成した Customer のオブジェクトのスクリプトを参照
            NavMeshofCustomer_FairJikkenn2 n = walker.GetComponent<NavMeshofCustomer_FairJikkenn2>();


            // 行動記号列ファイルの行数分だけ、walker には behavLineList の要素を渡す
            if (i < b.behavLineList.Count)
            {
                // BehaviourScriptReader(同じCustomerCreator)からbehavLineListを得て、
                // その i 番目の記号列を customer 生成と同時に渡す
                // また、そのエージェントの ReadFile も true にしておく
                n.behavLine = b.behavLineList[i];
                n.readFileOrNot = true;

                Debug.Log((i + 1) + "人目の行動記号列 : " + b.behavLineList[i]);
            }

            walkerList.Add(walker);

            // 場所や移動方向を設定
            if (randNum < 0.5f)
            {
                walker.transform.position = new Vector3(randPos, 0, -20f);
            }
            else
            {
                walker.transform.position = new Vector3(randPos, 0, 20f);
                walker.transform.eulerAngles = new Vector3(0, 180f, 0);
            }

            i++;

        }
    }

}
