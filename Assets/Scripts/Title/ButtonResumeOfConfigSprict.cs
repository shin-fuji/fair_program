using UnityEngine;
using System.Collections;

public class ButtonResumeOfConfigSprict : MonoBehaviour
{
    GameObject configObj;

    void Start()
    {
        configObj = GameObject.Find("ConfigObjects");
    }

    public void ButtonPush()
    {
        // 「Close」ボタンが押されると、Configメニューを閉じる
        Config config = configObj.GetComponent<Config>();
        config.OnResume();
    }
}
