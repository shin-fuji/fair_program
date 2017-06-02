using UnityEngine;
using System.Collections;

public class Location_Legs : MonoBehaviour
{

    // World coordinates of LeftLeg
    public static Vector3 location_of_LLeg;
    public static Vector3 rotation_of_LLeg;
    // World coordinates of RightLeg
    public static Vector3 location_of_RLeg;
    public static Vector3 rotation_of_RLeg;

    // World coordinates of LeftFoot
    public static Vector3 location_of_LFoot;
    // World coordinates of RightFoot
    public static Vector3 location_of_RFoot;

    // Use this for initialization
    void Start()
    {
        location_of_LLeg = transform.position;
        rotation_of_LLeg = transform.eulerAngles;
        location_of_RLeg = transform.position;
        rotation_of_RLeg = transform.eulerAngles;

        location_of_LFoot = transform.position;
        location_of_RFoot = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        location_of_LLeg = transform.position;
        rotation_of_LLeg = transform.eulerAngles;
        location_of_RLeg = transform.position;
        rotation_of_RLeg = transform.eulerAngles;

        location_of_LFoot = transform.position;
        location_of_RFoot = transform.position;
    }
}
