using UnityEngine;
using System.Collections;

public class foward_test2 : MonoBehaviour
{
    public GameObject player; // GameObject保存用変数、[Inspector]から設定できるように public に
    private Vector3 offset;   // オフセット保存用変数、Start() 内初期化するので private に
    // PlayerControllerの情報を取得
    GameObject playerCon;
    GameObject ImmersiveCam;
    // 進行方向を示すベクトル
    Vector3 directionVec;

    Transform playerTra;
    Vector3 camPos;

    void Start()
    {
        // FindChildは入れ子構造に対応させて複数使えるらしい
        playerCon = GameObject.Find("PlayerController");
        ImmersiveCam = GameObject.Find("ImmersiveCamera");
    }


    // Update is called once per frame
    void Update()
    {
        // GameObject座標 + カメラオフセット座標
        // これで角度固定のままGameObjectとの距離が固定される

        playerTra = playerCon.transform;
        camPos = ImmersiveCam.transform.position;


        directionVec =
            new Vector3(
                playerCon.transform.position.x - ImmersiveCam.transform.position.x,
                0,
                playerCon.transform.position.z - ImmersiveCam.transform.position.z);

        transform.position = camPos + directionVec * 2;
    }
}