using UnityEngine;
using System.Collections;

public class Location_RLeg : MonoBehaviour
{
    
    // World coordinates of RightLeg
    public static Vector3 location_of_RLeg;
    public static Vector3 rotation_of_RLeg;

    // Use this for initialization
    void Start()
    {
        location_of_RLeg = transform.position;
        rotation_of_RLeg = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        location_of_RLeg = transform.position;
        rotation_of_RLeg = transform.eulerAngles;
    }
}
