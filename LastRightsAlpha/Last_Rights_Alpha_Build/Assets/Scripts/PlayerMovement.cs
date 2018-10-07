﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : playerOrder
{
    private Rigidbody2D rigidbody;
    private InputNames  inputs;
	public float 		moveSpeed;
    public int          playerNum;
    float               horizontal,
                        vertical,
                        horAim,
                        vertAim,
						lt,
                        rt;
						
    bool                lb,
                        rb;
    Vector3             direction;

    public PlayerMovement()
    {
        inputs = returnDefaultInputs();
    }


    // Use this for initialization
    void Start ()
    {
        Physics2D.IgnoreLayerCollision(8, 10);
        //Physics2D.IgnoreLayerCollision(8, 12);
        Physics2D.IgnoreLayerCollision(9, 12, false);



        rigidbody = GetComponent<Rigidbody2D>();
        setInputs(playerNum);
	}
	
	// Update is called once per frame
	void Update ()
    {
        getInputs();
        Movement();
	}

    void setInputs(int var)
    {
        inputs.horizontalMove += var.ToString();
        inputs.verticalMove += var.ToString();
        inputs.horizontalAim += var.ToString();
        inputs.verticalAim += var.ToString();
        inputs.leftBumper += var.ToString();
        inputs.rightBumper += var.ToString();
        inputs.leftTrigger += var.ToString();
        inputs.rightTrigger += var.ToString();
    }


    void getInputs()
    {
        horizontal = Input.GetAxis(inputs.horizontalMove);
        vertical = Input.GetAxis(inputs.verticalMove);
        horAim = Input.GetAxis(inputs.horizontalAim);
        vertAim = Input.GetAxis(inputs.verticalAim);
        lb = Input.GetButtonDown(inputs.leftBumper);
        rb = Input.GetButtonDown(inputs.rightBumper);
        lt = Input.GetAxis(inputs.leftTrigger);
        rt = Input.GetAxis(inputs.rightTrigger);
    }

    void Movement()
    {
        rigidbody.velocity = new Vector2(horizontal, vertical).normalized * moveSpeed;        
    }
}
