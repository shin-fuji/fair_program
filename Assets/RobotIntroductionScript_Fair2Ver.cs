﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotIntroductionScript_Fair2Ver : MonoBehaviour {

    [SerializeField]
    TextController dialogObject;

    Animator anim;


	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        if (!dialogObject.IsCompleteLine)
            anim.SetBool("isTalking", true);
        else
            anim.SetBool("isTalking", false);
    }
}
