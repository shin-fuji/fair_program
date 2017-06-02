using UnityEngine;
using System.Collections;

public class Location_RHand : MonoBehaviour {

    // World coordinates of RightHand
    public static Vector3 location_of_RHand;
    public static Vector3 rotation_of_RHand;
    public static Vector3 localrot_of_RHand;

    // Use this for initialization
    void Start () {

        location_of_RHand = transform.position;
        rotation_of_RHand = transform.eulerAngles;
        localrot_of_RHand = transform.localEulerAngles;
    }
	
	// Update is called once per frame
	void Update () {

        location_of_RHand = transform.position;
        rotation_of_RHand = transform.eulerAngles;
        localrot_of_RHand = transform.localEulerAngles;
    }
}
