using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerBehavior_VIVE : MonoBehaviour
{

    // 進行方向を示すベクトル
    private Vector3 directionVec;

    // コントローラー
    [SerializeField]
    private GameObject leftController;
    [SerializeField]
    private GameObject rightController;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        directionVec = transform.forward;

    }

}
