using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpOrNotVIVEScript : MonoBehaviour
{

    [SerializeField]
    private Text pickUpOrNot_gui;
    [SerializeField]
    private PlayerBehaviorText_VIVE pbt_VIVE; // [VRTK_SDKManager]->SteamVR->[CameraRig]


    private string pickUpOrNotStr;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if ((int)pbt_VIVE.whichBehavior == 4) { pickUpOrNotStr = "○"; } else { pickUpOrNotStr = "×"; };
        
        pickUpOrNot_gui.text = "pickup_or_not : " + pickUpOrNotStr;
    }
}
