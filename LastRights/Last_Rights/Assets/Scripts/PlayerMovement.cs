using System.Collections;
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
						lt,
                        rt;
						
    bool                lb,
                        rb;
    public PlayerMovement()
    {
        inputs = returnDefaultInputs();
    }


    // Use this for initialization
    void Start ()
    {
		rigidbody = GetComponent<Rigidbody2D>();
		Debug.Log(playerNum);
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
        lb         = Input.GetButtonDown(inputs.leftBumper);
        rb         = Input.GetButtonDown(inputs.rightBumper);
        lt         = Input.GetAxis(inputs.leftTrigger);
        rt         = Input.GetAxis(inputs.rightTrigger);
		if (lb)
			Debug.Log(gameObject.name);
		if (rb)
			Debug.Log(gameObject.name);
		if (lt == 1)
			Debug.Log(gameObject.name);
		if (rt == 1)
			Debug.Log(gameObject.name);
    }

    void Movement()
    {
		rigidbody.velocity = new Vector2(horizontal, vertical).normalized * moveSpeed;
    }
}
