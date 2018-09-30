using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : playerOrder
{
    private InputNames  inputs;
    public int          playerNum;
    float               horizontal,
                        vertical;
    bool                lb,
                        rb,
                        lt,
                        rt;
    public PlayerMovement()
    {
        inputs = returnDefaultInputs();
        setInputs(playerNum);
    }


    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        getInputs();
        Movement();
	}

    void setInputs(int var)
    {
        inputs.horizontalMove   += var.ToString();
        inputs.verticalMove     += var.ToString();
        inputs.leftBumper       += var.ToString();
        inputs.rightBumper      += var.ToString();
        inputs.leftTrigger      += var.ToString();
        inputs.rightTrigger     += var.ToString();
    }

    void getInputs()
    {
        horizontal = Input.GetAxis(inputs.horizontalMove);
        vertical   = Input.GetAxis(inputs.verticalMove);
        lb         = Input.GetButton(inputs.leftBumper);
        rb         = Input.GetButton(inputs.rightBumper);
        lt         = Input.GetButton(inputs.leftTrigger);
        rt         = Input.GetButton(inputs.rightTrigger);
    }

    void Movement()
    {

    }
}
