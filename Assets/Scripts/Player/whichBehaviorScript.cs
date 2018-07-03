using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// プレイヤーがどの行動をとっているか(whichBehavior)を表示する
/// </summary>
public class whichBehaviorScript : MonoBehaviour
{
    [SerializeField]
    private Text whichBehavior_gui;
    [SerializeField]
    private PlayerBehaviorText_VIVE pbt_VIVE; // [VRTK_SDKManager]->SteamVR->[CameraRig]

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("whichBehavior : " + pbt_VIVE.whichBehavior.ToString());
        // スクリプト「PlayerBehaviorText_VIVE」内の WhichBehavior 型変数「whichBehavior」を表示
        whichBehavior_gui.text = "whichBehavior : " + pbt_VIVE.whichBehavior.ToString();
    }
}
