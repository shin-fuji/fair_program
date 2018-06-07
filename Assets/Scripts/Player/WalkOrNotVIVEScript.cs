using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WalkOrNotVIVEScript : MonoBehaviour
{
    [SerializeField]
    private Text walkOrNot_gui;
    [SerializeField]
    private PlayerBehaviorText_VIVE pbt_VIVE; // [VRTK_SDKManager]->SteamVR->[CameraRig]

    private string walkOrNotStr;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if ((int)pbt_VIVE.whichBehavior == 1) { walkOrNotStr = "○"; } else { walkOrNotStr = "×"; };
        
        walkOrNot_gui.text = "walk_or_not : " + walkOrNotStr;
    }
}
