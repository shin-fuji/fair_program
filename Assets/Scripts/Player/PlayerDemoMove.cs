using UnityEngine;
using System.Collections;

public class PlayerDemoMove : MonoBehaviour
{

    //先ほど作成したJoystick
    [SerializeField]
    private JoyStick _joystick = null;

    //移動速度
    private const float SPEED = 0.05f;

    private void Update()
    {
        Vector3 pos = transform.position;

        // 仕様上、加算方向が逆になってしまった
        pos.x -= _joystick.Position.x * SPEED;
        pos.z -= _joystick.Position.y * SPEED;

        transform.position = pos;
    }

}