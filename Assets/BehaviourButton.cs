using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourButton : MonoBehaviour {


    [SerializeField]
    private PlayerBehaviorText_VIVE pbt_VIVE;

    /// <summary>
    /// 画面上のボタンをクリックした時に呼ばれる関数群
    /// </summary>
    public void ClickPickUpButton()
    {
        if (Pausable.pauseGame == false)
            pbt_VIVE.whichBehavior = PlayerBehaviorText_VIVE.WhichBehavior.PICKUP;
    }
    public void ClickThinkingButton()
    {
        if (Pausable.pauseGame == false)
            pbt_VIVE.whichBehavior = PlayerBehaviorText_VIVE.WhichBehavior.THINKING;
    }
    public void ClickLookAroundButton()
    {
        if (Pausable.pauseGame == false)
            pbt_VIVE.whichBehavior = PlayerBehaviorText_VIVE.WhichBehavior.LOOKAROUND;
    }
    public void ClickAppreciationButton()
    {
        if (Pausable.pauseGame == false)
            pbt_VIVE.whichBehavior = PlayerBehaviorText_VIVE.WhichBehavior.APPRECIATION;
    }
    public void ClickHandclapButton()
    {
        if (Pausable.pauseGame == false)
            pbt_VIVE.whichBehavior = PlayerBehaviorText_VIVE.WhichBehavior.HANDCLAP;
    }
    public void ClickApplauseButton()
    {
        if (Pausable.pauseGame == false)
            pbt_VIVE.whichBehavior = PlayerBehaviorText_VIVE.WhichBehavior.APPLAUSE;
    }
}
