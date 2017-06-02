using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Get the Location of Robot_Hips.
/// </summary>
public class Location_Hips : MonoBehaviour
{


    // Local coordinates of Hips
    private Vector3 local_cor;

    // World coordinates of Hips
    public static Vector3 location_of_Hips;
    public static Vector3 rotation_of_Hips;


    // Use this for initialization
    void Start()
    {
        location_of_Hips = transform.position;
        rotation_of_Hips = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        location_of_Hips = transform.position;
        rotation_of_Hips = transform.eulerAngles;
    }
}
