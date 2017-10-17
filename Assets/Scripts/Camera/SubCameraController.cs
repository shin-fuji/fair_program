using UnityEngine;
using System.Collections;

/// <summary>
/// Immersiveディスプレイ and ドーム型ディスプレイ用のカメラのためのスクリプト
/// 角度を固定しながら、Playerの周囲を画面に映し出せるように設定
/// </summary>
public class SubCameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
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
        transform.position = player.transform.position + offset;
        transform.rotation = rot_fixed;
    }
}