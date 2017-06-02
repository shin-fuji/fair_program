using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Walk or not
/// </summary>
public class WalkOrNotScript : MonoBehaviour
{

    [SerializeField]
    Text location_gui;

    private Vector3 rHandL = Location_RHand.location_of_RHand;

    private bool walkornot = false;
    private string walkOrNotStr;


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

        rHandL = Location_RHand.location_of_RHand;

        // 別のオブジェクト(TestPlayer)のスクリプトを参照する場合
        PlayerControllerBehaviour pcb = refObj.GetComponent<PlayerControllerBehaviour>();
        walkornot = pcb.walkOrNot;

        //location_gui.text = "LHand_Location : (" + rHandL.x + "," + rHandL.y + "," + rHandL.z + ")";
        //location_gui.text = "LHand_Rotation : (" + rHandR.x + "," + rHandR.y + "," + rHandR.z + ")";

        if (walkornot == true) { walkOrNotStr = "○"; } else { walkOrNotStr = "×"; };


        location_gui.text = "walk_or_not : " + walkOrNotStr;
    }
}
