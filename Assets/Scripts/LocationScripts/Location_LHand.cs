using UnityEngine;
using System.Collections;

public class Location_LHand : MonoBehaviour {

    // World coordinates of LeftHand
    public static Vector3 location_of_LHand;
    public static Vector3 rotation_of_LHand;
    public static Vector3 localrot_of_LHand;

    // Use this for initialization
    void Start () {

        location_of_LHand = transform.position;
        rotation_of_LHand = transform.eulerAngles;
        localrot_of_LHand = transform.localEulerAngles;
    }
	
	// Update is called once per frame
	void Update () {

        location_of_LHand = transform.position;
        rotation_of_LHand = transform.eulerAngles;
        localrot_of_LHand = transform.localEulerAngles;
    }
}
