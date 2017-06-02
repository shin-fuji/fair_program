using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Throw or not
/// </summary>
public class ThrowOrNotScript : MonoBehaviour
{

    [SerializeField]
    Text rotation_gui;

    private bool throwornot = false;
    private string throwOrNotStr;


    GameObject refObj;


    // Use this for initialization
    void Start()
    {
        // PlayerControllerの情報を使う
        refObj = GameObject.Find("PlayerController");
    }

    // Update is called once per frame
    void Update()
    {

        // 別のオブジェクト(TestPlayer)のスクリプトを参照する場合
        PlayerControllerBehaviour pcb = refObj.GetComponent<PlayerControllerBehaviour>();
        //throwornot = pcb.throwOrNot;

        if (throwornot == true) { throwOrNotStr = "○"; } else { throwOrNotStr = "×"; };

        //rotation_gui.text = "RHand_Rotation : (" + rHandR.x.ToString() + "," + rHandR.y.ToString() + "," + rHandR.z.ToString() + ")";
        rotation_gui.text = "throw_or_not : " + throwOrNotStr;
    }
}
