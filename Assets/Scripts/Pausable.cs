using UnityEngine;
using System.Collections;
// 追加
using UnityEngine.SceneManagement;

public class Pausable : MonoBehaviour
{

    public GameObject player;
    public GameObject OnPanel;
    public GameObject OnFinishPanel;
    public static bool pauseGame = false;
    public static bool finishGame = false;
    public float TimeLimit;
    float timer = 0;

    void Start()
    {
        OnUnPause();
    }

    public void Update()
    {
        // 4分間で自動的に終了するように設定
        if(pauseGame == false && finishGame == false) timer += Time.deltaTime;

        //if (timer > TimeLimit) OnFinish();


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseGame = !pauseGame;

            if (pauseGame == true)
            {
                OnPause();
            }
            else
            {
                OnUnPause();
            }
        }
    }

    public void OnPause()
    {
        OnPanel.SetActive(true);        // PanelMenuをtrueにする
        Time.timeScale = 0;
        pauseGame = true;

        //Cursor.lockState = CursorLockMode.None;     // 標準モード
        //Cursor.visible = true;    // カーソル表示
    }

    public void OnUnPause()
    {
        OnPanel.SetActive(false);       // PanelMenuをfalseにする
        OnFinishPanel.SetActive(false);       // FinishPanelをfalseにする
        Time.timeScale = 1;
        pauseGame = false;

        //Cursor.lockState = CursorLockMode.Locked;   // 中央にロック
        //Cursor.visible = false;     // カーソル非表示
    }

    public void OnFinish()
    {
        OnFinishPanel.SetActive(true);        // FinishPanelをtrueにする
        Time.timeScale = 0;
        finishGame = false;
    }
    

    public void OnResume()
    {
        OnUnPause();
    }
}