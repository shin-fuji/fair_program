using UnityEngine;
using System.Collections;

/// <summary>
/// This script manages behaviours of the Robot(ex, which the robot is walking or not).
/// </summary>
public class PlayerControllerBehaviour : MonoBehaviour
{

    public bool walkOrNot = false;
    public bool pickUpOrNot = false;


    float randNum = 0;
    GameObject[] otherCustomer;

    private Vector3 lastPosition;
    private Vector3 lastLHandPos;
    private Vector3 lastRHandPos;
    private Vector3 lastLHandLocalRot;
    private Vector3 lastRHandLocalRot;
    private Vector3 lastLForeArmPos;
    private Vector3 lastRForeArmPos;
    private Vector3 lastLLegPos;
    private Vector3 lastRLegPos;
    private Vector3 lastLUpLegPos;
    private Vector3 lastRUpLegPos;
    

    // Awake() is executed when instances are loaded.
    void Awake()
    {
    }

    // Use this for initialization
    void Start()
    {

        // 位置・角度情報を初期化
        lastPosition = Location_Hips.location_of_Hips;
        lastLHandPos = Location_LHand.location_of_LHand;
        lastRHandPos = Location_RHand.location_of_RHand;
        lastLHandLocalRot = Location_LHand.localrot_of_LHand;
        lastRHandLocalRot = Location_RHand.localrot_of_RHand;
        lastLForeArmPos = Location_LForeArm.location_of_LForeArm;
        lastRForeArmPos = Location_RForeArm.location_of_RForeArm;
        lastLLegPos = Location_LLeg.location_of_LLeg;
        lastRLegPos = Location_RLeg.location_of_RLeg;
        lastLUpLegPos = Location_LUpLeg.location_of_LUpLeg;
        lastRUpLegPos = Location_RUpLeg.location_of_RUpLeg;
    }

    // Update is called once per frame
    void Update()
    {
        walkOrNot = WalkOrNot(lastLLegPos, lastRLegPos, lastLUpLegPos, lastRUpLegPos);


        // とりあえず左手だけで判断
        pickUpOrNot = PickUpOrNot(Location_LHand.location_of_LHand);
        
        

        // 位置・角度情報を更新
        lastLHandPos = Location_LHand.location_of_LHand;
        lastRHandPos = Location_RHand.location_of_RHand;
        lastLHandLocalRot = Location_LHand.localrot_of_LHand;
        lastRHandLocalRot = Location_RHand.localrot_of_RHand;
        lastLForeArmPos = Location_LForeArm.location_of_LForeArm;
        lastRForeArmPos = Location_RForeArm.location_of_RForeArm;
        lastLLegPos = Location_LLeg.location_of_LLeg;
        lastRLegPos = Location_RLeg.location_of_RLeg;
        lastLUpLegPos = Location_LUpLeg.location_of_LUpLeg;
        lastRUpLegPos = Location_RUpLeg.location_of_RUpLeg;
    }


    /// <summary>
    /// 歩いているか否かを判定する関数
    /// 両足の太もものベクトル間の角度を計算
    /// </summary>
    /// <param name="LLegRot"></param>
    /// <param name="RLegRot"></param>
    /// <returns></returns>
    private bool WalkOrNot(Vector3 LLegPos, Vector3 RLegPos, Vector3 LUpLegPos, Vector3 RUpLegPos)
    {
        Vector3 LLegVec = LLegPos - LUpLegPos;
        Vector3 RLegVec = RLegPos - RUpLegPos;


        //Debug.Log("<color=red>Angle = " + Vector3.Angle(LLegVec, RLegVec).ToString() + "</color>");
        //Debug.Log("<color=green>LegDis = " + VectorDistance(LLegPos, RLegPos).ToString() + "</color>");

        if (Vector3.Angle(LLegVec, RLegVec) > 25) return true;
        else return false;

    }


    /// <summary>
    /// 物を手に取っているか否かを判定する関数
    /// </summary>
    /// <param name="handPos"></param>
    /// <returns></returns>
    private bool PickUpOrNot(Vector3 handPos)
    {
        if (handPos.y > 0.6)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    /// <summary>
    /// x-z平面における二点間の距離を計算する
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <returns></returns>
    private float VectorDistance(Vector3 v1, Vector3 v2)
    {
        return (float)System.Math.Sqrt(System.Math.Pow(v1.x - v2.x, 2) + System.Math.Pow(v1.z - v2.z, 2));
    }
}
