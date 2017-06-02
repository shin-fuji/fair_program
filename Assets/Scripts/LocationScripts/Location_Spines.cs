using UnityEngine;
using System.Collections;

public class Location_Spines : MonoBehaviour
{

    // World coordinates of Spine and so on
    public static Vector3 location_of_Spine1;
    public static Vector3 rotation_of_Spine1;


    // Use this for initialization
    void Start()
    {
        location_of_Spine1 = transform.position;
        rotation_of_Spine1 = transform.eulerAngles;

    }

    // Update is called once per frame
    void Update()
    {
        location_of_Spine1 = transform.position;
        rotation_of_Spine1 = transform.eulerAngles;

    }
}
