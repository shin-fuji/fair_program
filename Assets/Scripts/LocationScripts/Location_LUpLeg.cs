using UnityEngine;
using System.Collections;

public class Location_LUpLeg : MonoBehaviour
{

    // World coordinates of LeftLeg
    public static Vector3 location_of_LUpLeg;
    public static Vector3 rotation_of_LUpLeg;

    // Use this for initialization
    void Start()
    {
        location_of_LUpLeg = transform.position;
        rotation_of_LUpLeg = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        location_of_LUpLeg = transform.position;
        rotation_of_LUpLeg = transform.eulerAngles;
    }
}
