using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField]
    private GameObject playerHip;
    [SerializeField]
    private GameObject ImmersiveCam;
    [SerializeField]
    private GameObject playerCon;
    [SerializeField]
    private PlayerControllerBehaviour pcb;
    // 進行方向を示すベクトル
    private Vector3 directionVec;
    


    // Use this for initialization
    void Start () {
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD

        // PlayerControllerの情報を使う
        refObj = GameObject.Find("PlayerController");

        // FindChildは入れ子構造に対応させて複数使えるらしい
        playerHip = transform.Find("Robot_References").
            Find("Robot_Reference").
            Find("Robot_Hips").gameObject;

        playerCon = transform.Find("PlayerController").gameObject;
        ImmersiveCam = transform.Find("ImmersiveCamera").gameObject;

=======
>>>>>>> 684eebeece1ce14769f563c1c5c9ea0928383a38
=======
>>>>>>> 684eebeece1ce14769f563c1c5c9ea0928383a38
=======
>>>>>>> 684eebeece1ce14769f563c1c5c9ea0928383a38
    }
	
	// Update is called once per frame
	void Update () {

        //// プレイヤーの角度を矢印キーで調整する
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(transform.up, -1.5f);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(transform.up, 1.5f);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += directionVec * 0.07f;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position -= directionVec * 0.07f;
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
