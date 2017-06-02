using UnityEngine;
using System.Collections;

public class Location_LLeg : MonoBehaviour
{

    // World coordinates of LeftLeg
    public static Vector3 location_of_LLeg;
    public static Vector3 rotation_of_LLeg;

    // Use this for initialization
    void Start()
    {
        location_of_LLeg = transform.position;
        rotation_of_LLeg = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        location_of_LLeg = transform.position;
        rotation_of_LLeg = transform.eulerAngles;
    }
}
