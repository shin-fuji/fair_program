using UnityEngine;
using System.Collections;

/// <summary>
/// ここでPlayerの向いている方向を計算し、ほかのプログラムで使いまわしている
/// </summary>
public class fowardTest : MonoBehaviour
{
    public GameObject player; // GameObject保存用変数、[Inspector]から設定できるように public に
    private Vector3 offset;   // オフセット保存用変数、Start() 内初期化するので private に
    // Playerの体の各位置の情報を取得
    [SerializeField]
    private GameObject playerHip;
    [SerializeField]
    private GameObject playerLShld;
    [SerializeField]
    private GameObject playerRShld;
    Vector3 playerHipPos;
    Vector3 playerLShldPos;
    Vector3 playerRShldPos;
    [SerializeField]
    private GameObject Cam;

    // 腰と両肩の位置を外積して得られたベクトル
    // このベクトルの向きは、必ずニューロンの向いている前方向と一致する
    Vector3 vecA, vecB, crossVec;

    Transform playerTra;
    Vector3 camPos;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // GameObject座標 + カメラオフセット座標
        // これで角度固定のままGameObjectとの距離が固定される

        //playerTra = playerHip.transform;

        playerHipPos = playerHip.transform.position;
        playerLShldPos = playerLShld.transform.position;
        playerRShldPos = playerRShld.transform.position;

        //Debug.Log("playerLShldPos = " + playerLShldPos);
        //Debug.Log("playerRShldPos = " + playerRShldPos);

        vecA = playerLShldPos - playerHipPos;
        vecB = playerRShldPos - playerHipPos;

        camPos = Cam.transform.position;

        // 外積の計算
        crossVec = new Vector3(
                   vecA.y * vecB.z - vecA.z * vecB.y,
                   vecA.z * vecB.x - vecA.x * vecB.z,
                   vecA.x * vecB.y - vecA.y * vecB.x);

        crossVec.Normalize();
        
        transform.position =
            new Vector3(
                camPos.x - crossVec.x * 1.4f,
                camPos.y - 0.74f,
                camPos.z - crossVec.z * 1.4f);
        //transform.position =
        //    new Vector3(
        //        camPos.x + playerHip.transform.forward.x * 1.4f,
        //        camPos.y - 0.74f,
        //        camPos.z + playerHip.transform.forward.z * 1.4f);

        //Quaternion.LookRotation(transform.position - camPos);
    }
}
