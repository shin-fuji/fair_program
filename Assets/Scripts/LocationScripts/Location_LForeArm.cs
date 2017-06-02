using UnityEngine;
using System.Collections;

public class Location_LForeArm : MonoBehaviour
{

    // World coordinates of LForeArm and so on
    public static Vector3 location_of_LForeArm;
    public static Vector3 rotation_of_LForeArm;


    // Use this for initialization
    void Start()
    {
        location_of_LForeArm = transform.position;
        rotation_of_LForeArm = transform.eulerAngles;

    }

    // Update is called once per frame
    void Update()
    {
        location_of_LForeArm = transform.position;
        rotation_of_LForeArm = transform.eulerAngles;

    }
}
