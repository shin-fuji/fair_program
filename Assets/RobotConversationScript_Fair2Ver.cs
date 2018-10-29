using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mebiustos.HeadLookControllerHelper;

public class RobotConversationScript_Fair2Ver : MonoBehaviour {


    // アニメーション関連の変数
    [SerializeField] GameObject companion;  //話し相手となるエージェント
    Animator anim, animOfCompanion;
    float timer = 0, timerSpeaking = 0, timerPointing = 0;
    float changeTLTime = 0;

    AnimatorStateInfo animInfo;
    AnimatorStateInfo animInfoOfCompanion;
    int _stateConvListner, _stateConvPointingL, _stateConvPointingR;

    float talkOrListen;    // 最初に，自身が話し手か聞き手かをこの変数の大小を比べて決める
    float talkOrListenOfCompanion;

    HeadLookHelperConv hlhc;
    HeadLookController hlc;

    float pointingRandNum;

    public float TalkOrListen
    {
        get { return talkOrListen; }
        set { }
    }

    private void Awake()
    {
        talkOrListen = Random.Range(0f, 100f);
        changeTLTime = 10;

        hlhc = GetComponent<HeadLookHelperConv>();
        hlc = GetComponent<HeadLookController>();

        anim = GetComponent<Animator>();
        animOfCompanion = companion.GetComponent<Animator>();
        anim.SetBool("isPointingL", false);
        anim.SetBool("isPointingR", false);

        _stateConvListner = Animator.StringToHash("Base Layer.ConvListner");
        _stateConvPointingL = Animator.StringToHash("Base Layer.ConvPointingL");
        _stateConvPointingR = Animator.StringToHash("Base Layer.ConvPointingR");
    }

    // Use this for initialization
    void Start () {


        talkOrListenOfCompanion = companion.GetComponent<RobotConversationScript_Fair2Ver>().TalkOrListen;


        // どちらが話し手・聞き手かを決める
        if (talkOrListen > talkOrListenOfCompanion)
        {
            anim.SetTrigger("ConvSpeaker");
            anim.SetBool("isSpeaking", true);
        }
        else
        {
            anim.SetTrigger("ConvListener");
            anim.SetBool("isSpeaking", false);
        }
    }
	
	// Update is called once per frame
	void Update () {

        hlhc = GetComponent<HeadLookHelperConv>();
        hlc = GetComponent<HeadLookController>();

        animInfo = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
        animInfoOfCompanion = companion.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);


        // 15秒ごとに話し手・聞き手を切り替える．指差し時はカウントしない
        if(!anim.GetBool("isPointingL") && !anim.GetBool("isPointingR") &&
           !animOfCompanion.GetBool("isPointingL") && !animOfCompanion.GetBool("isPointingR"))
            timer += Time.deltaTime;


        if (timer >= changeTLTime)
        {
            anim.SetBool("isSpeaking", !anim.GetBool("isSpeaking"));
            timer = 0;
        }

        ///
        /// 自分が聞き手なら
        ///
        // 相手が指差しの状態にHeadLookHelperをActiveにする
        if (!anim.GetBool("isSpeaking"))
        {
            if (animOfCompanion.GetBool("isPointingL"))
            {
                //hlhc.PointingLorR = 0;
                if (hlhc != null) hlhc.enabled = true;
                if (hlc != null) hlc.enabled = true;
            }
            else if (animOfCompanion.GetBool("isPointingR"))
            {
                //hlhc.PointingLorR = 1;
                if (hlhc != null) hlhc.enabled = true;
                if (hlc != null) hlc.enabled = true;
            }
            else
            {
                //Debug.Log("hlhc,hlc == false");
                if (hlhc != null) hlhc.enabled = false;
                if (hlc != null) hlc.enabled = false;
            }

            return;
        }


        ///
        /// 自分が話し手なら
        ///

        if (hlhc != null) hlhc.enabled = false;
        if (hlc != null) hlc.enabled = false;

        // 指差しをしていなければ…
        if (!anim.GetBool("isPointingL") && !anim.GetBool("isPointingR"))
        {
            timerSpeaking += Time.deltaTime;

            //Debug.Log("pointingRandNum = " + pointingRandNum);
            if (timerSpeaking > 3f)
            {
                timerSpeaking = 0;
                pointingRandNum = Random.Range(0f, 1f);

                if (pointingRandNum <= 0.5f)
                {
                    anim.SetBool("isPointingL", true);
                    anim.SetBool("isPointingR", false);
                }
                else if (pointingRandNum > 0.5f)
                {
                    anim.SetBool("isPointingL", false);
                    anim.SetBool("isPointingR", true);
                }
            }
        }
        // 自分が指差しをしていれば…
        else if(anim.GetBool("isPointingL") || anim.GetBool("isPointingR"))
        {
            timerPointing += Time.deltaTime;
            
            // 一定時間後にConvSpeakerへ
            if(timerPointing > 5f)
            {
                anim.SetBool("isPointingL", false);
                anim.SetBool("isPointingR", false);
                timerPointing = 0;
            }
        }




        
    }
}
