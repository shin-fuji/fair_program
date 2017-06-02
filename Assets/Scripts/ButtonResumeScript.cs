using UnityEngine;
using System.Collections;

/// <summary>
/// ポーズ画面の「続ける」ボタンを押すと
/// ゲームを再開する
/// </summary>
public class ButtonResumeScript : MonoBehaviour
{
    GameObject pauseObj;
    
    void Start()
    {
        pauseObj = GameObject.Find("PausableObjects");
    }

    public void ButtonPush()
    {
        // 別のオブジェクト(PauseObjects)のスクリプトを参照
        Pausable pausable = pauseObj.GetComponent<Pausable>();
        pausable.OnUnPause();
    }
}
