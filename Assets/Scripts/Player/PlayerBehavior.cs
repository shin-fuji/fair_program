using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour
{
    // PlayerのHipの情報を取得
    GameObject playerHip;
    // ImmersiveCamの情報を取得
    GameObject ImmersiveCam;
    // 方向指示器PlayerControllerの情報を取得
    GameObject playerCon;
    // 進行方向を示すベクトル
    Vector3 directionVec;

    GameObject refObj;




    // Use this for initialization
    void Start () {

        // PlayerControllerの情報を使う
        refObj = GameObject.Find("PlayerController");

        // FindChildは入れ子構造に対応させて複数使えるらしい
        playerHip = transform.FindChild("Robot_References").
            FindChild("Robot_Reference").
            FindChild("Robot_Hips").gameObject;

        playerCon = transform.FindChild("PlayerController").gameObject;
        ImmersiveCam = transform.FindChild("ImmersiveCamera").gameObject;

    }
	
	// Update is called once per frame
	void Update () {

        PlayerControllerBehaviour pcb = refObj.GetComponent<PlayerControllerBehaviour>();

        //// プレイヤーの角度を矢印キーで調整する
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(transform.up, -0.5f);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(transform.up, 0.5f);
        }


        directionVec =
            new Vector3(
                playerCon.transform.position.x - ImmersiveCam.transform.position.x,
                0,
                playerCon.transform.position.z - ImmersiveCam.transform.position.z);

        if (pcb.walkOrNot)
        {
            // Transrateだと、なぜか矢印キーで方向調整したときにずれる
            //transform.Translate(playerHip.transform.forward * Speed * Time.deltaTime);//前に移動
            //transform.Translate(directionVec * Speed * Time.deltaTime);//前に移動
            transform.position += directionVec * 0.07f;
        }
    }
}
