using UnityEngine;
using System.Collections;

public class ButtonChangeAdr : MonoBehaviour
{
    GameObject configObj;

    void Start()
    {
        configObj = GameObject.Find("ConfigObjects");
    }

    public void ButtonPush()
    {
        // 「Change」ボタンが押されると、Addressを変更する関数OnChange()が呼ばれる
        Config config = configObj.GetComponent<Config>();
        config.OnChange();
    }
}
