﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	private Rigidbody myRigidbody;
	public float moveSpeed; //Leave this public unless you need it not to be so I can change it in editor - Your Pal, Josh. Or I guess you could just serialize the field or w/e
    public Vector3 subSpeed;
    public bool lockIn;
    GameObject target;
    public float vecZ;
    bool coll;

    // Use this for initialization
    void Start () 
	{
		myRigidbody = GetComponent<Rigidbody>();
        subSpeed = Vector3.zero;
        lockIn = false;
        target = GameObject.FindGameObjectWithTag("sub");
        vecZ = 0;
        coll = false;
    }

    // Update is called once per frame
    void Update () 
	{
		//float horizontal = Input.GetAxis("Horizontal"); //Uses Unity Input Manager settings for WASD and Arrows

		//HandleMovement(horizontal);
	}

	void FixedUpdate()
	{
		float horizontal = Input.GetAxis("Horizontal"); //Uses Unity Input Manager settings for WASD and Arrows // Using Fixed Update for this so that movement speed isn't bound by Framerate
        float vertical = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.C))
        {
            lockIn = !lockIn;
        }
        if (!lockIn)
            HandleMovement(horizontal);
        else
            target.SendMessage("SetJoy", new Vector3(horizontal, 0, vertical));
        vecZ = 0;
    }

	private void HandleMovement(float horizontal)//So I could use horizontal inputs for movement, I think this works for joystick too, I will test it with ps4 controller
	{
		myRigidbody.velocity = new Vector3 (horizontal * moveSpeed, 0, vecZ) + subSpeed; //x = -1 or 1  y = 0 No vertical movement.
	}
}
