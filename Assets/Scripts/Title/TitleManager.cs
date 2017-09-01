using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using Neuron;


public class TitleManager : MonoBehaviour {
	[SerializeField] Image Black;

    GameObject configObj;

	void Start () 
	{
		Black.gameObject.SetActive( false );
        configObj = GameObject.Find("ConfigObjects");

        // アドレス変更のための変数を初期化
        //NeuronConnection.changedAdr = false;
        //NeuronConnection.newAddress = "";
    }
    
	// GameStartボタンを押したとき 
	public void GameStartButton()
    {
        if ( Black.gameObject.activeSelf == false )
		{
			//1秒間(time)かけて値を0から１（fromからto）まで変化させる。
			//毎フレーム現在地をUpdateHandlerの引数に渡す
			Black.gameObject.SetActive( true );
			iTween.ValueTo(gameObject, iTween.Hash("from", 0, "to", 1, "time", 1f, "onupdate", "UpdateHandler"));
		}

        // フェードアウトで切り替えるとうまくいかないので、
        // ここで直接シーン遷移を行う
        //SceneManager.LoadScene("Fair");
        SceneManager.LoadScene("Fair_2");
    }

    // Configボタンを押したとき
    public void ConfigButton()
    {
        // Configメニューを開く
        Config config = configObj.GetComponent<Config>();
        config.OnConfig();
    }

    //値を受け取る
    void UpdateHandler(float value)
	{
		Black.color = new Color(0, 0, 0, value);
		if(value >= 1)
		{
            //各ゲームでシーン遷移
            //Debug.Log("シーン遷移");
            //SceneManager.LoadScene("Fair");
            //SceneManager.LoadScene("Fair_2");
            // 旧形式なので使えない
            //Application.LoadLevel("main");
        }
    }
}

