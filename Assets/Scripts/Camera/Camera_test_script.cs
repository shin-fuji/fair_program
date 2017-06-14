using UnityEngine;
using System.Collections;

public class Camera_test_script : MonoBehaviour
{ 
    public GameObject player; // GameObject保存用変数、[Inspector]から設定できるように public に
    private Vector3 offset;   // カメラオフセット保存用変数、Start() 内初期化するので private に
    private Quaternion rot_fixed; // カメラの角度は固定したい

    void Start()
    {
        offset = transform.position - player.transform.position; // スタート時の、playerとの相対座標を保存
        rot_fixed = transform.rotation; // スタート時のカメラの角度を保存
    }

    void LateUpdate() // LateUpdate()はすべてオブジェクトの Update() を実行後に呼ばれる
    {
        // GameObject座標 + カメラオフセット座標
        // これで角度固定のままGameObjectとの距離が固定される
        transform.position = new Vector3(
            player.transform.position.x - player.transform.forward.x * 2,
            transform.position.y,
            player.transform.position.z - player.transform.forward.z * 2
            );
        //transform.rotation = rot_fixed;
    }
}