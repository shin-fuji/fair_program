using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// pickup or not
/// </summary>
public class PickUpOrNotScript : MonoBehaviour
{

    [SerializeField] Text pickUpOrnot_gui;

    private bool pickupornot = false;
    private string pickUpOrNotStr;


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

        if (pickupornot == true) { pickUpOrNotStr = "○"; } else { pickUpOrNotStr = "×"; };
        
        pickUpOrnot_gui.text = "pickup_or_not : " + pickUpOrNotStr;
    }
}