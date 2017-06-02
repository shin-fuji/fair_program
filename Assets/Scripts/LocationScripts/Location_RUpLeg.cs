using UnityEngine;
using System.Collections;

public class Location_RUpLeg : MonoBehaviour
{

    // World coordinates of RightLeg
    public static Vector3 location_of_RUpLeg;
    public static Vector3 rotation_of_RUpLeg;

    // Use this for initialization
    void Start()
    {
        location_of_RUpLeg = transform.position;
        rotation_of_RUpLeg = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        location_of_RUpLeg = transform.position;
        rotation_of_RUpLeg = transform.eulerAngles;
    }
}
