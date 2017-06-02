using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 店に客(Customer)を入店させるプログラム
/// 店の玄関で客を生成する
/// </summary>
public class CustomerCreator : MonoBehaviour
{

    public GameObject originalCustomer;
    public static List<GameObject> customerList = new List<GameObject>();

    public int customerNum = 3;
    
    int i = 0;


    // 客の入店間隔
    float timer = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        timer += Time.deltaTime;
        // 客の人数が customerNum 人以下のとき、5秒経過で一人入店
        // (構造上、条件節は '<')
        if (customerList.Count < customerNum && timer >= 5)
        {
            timer = 0;

            // Customerを生成
            GameObject customer = Instantiate(originalCustomer);

            // 同じオブジェクト(CustomerCreator)のスクリプトを参照
            BehaviourScriptReader b = GetComponent<BehaviourScriptReader>();
            // 生成した Customer のオブジェクトのスクリプトを参照
            NavMeshofCustomer_FairVer n = customer.GetComponent<NavMeshofCustomer_FairVer>();


            // 行動記号列ファイルの行数分だけ、Customer には behavLineList の要素を渡す
            if (i < b.behavLineList.Count)
            {
                // BehaviourScriptReader(同じCustomerCreator)からbehavLineListを得て、
                // その i 番目の記号列を customer 生成と同時に渡す
                // また、readFileOrNot も true にしておく
                n.behavLine = b.behavLineList[i];
                n.readFileOrNot = true;

                Debug.Log((i + 1) + "人目の行動記号列 : " + b.behavLineList[i]);
            }

            customerList.Add(customer);

            // 移動速度を設定
            customer.transform.localPosition = new Vector3(-1.5f, 0, 14f);
            customer.transform.localEulerAngles = new Vector3(0, 180f, 0);

            i++;

        }
    }
}
