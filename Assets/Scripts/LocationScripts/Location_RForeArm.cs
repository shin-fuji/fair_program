using UnityEngine;
using System.Collections;

public class Location_RForeArm : MonoBehaviour
{

    // World coordinates of RForeArm and so on
    public static Vector3 location_of_RForeArm;
    public static Vector3 rotation_of_RForeArm;


    // Use this for initialization
    void Start()
    {
        location_of_RForeArm = transform.position;
        rotation_of_RForeArm = transform.eulerAngles;

    }

    // Update is called once per frame
    void Update()
    {
        location_of_RForeArm = transform.position;
        rotation_of_RForeArm = transform.eulerAngles;

    }
}
