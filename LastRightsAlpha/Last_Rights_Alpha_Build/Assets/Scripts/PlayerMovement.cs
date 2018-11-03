using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct info
{
    public float horizontal,
                 vertical,
                 lt,
                 rt;
    public bool  lb,
                 rb;
    void Init()
    {
        horizontal = vertical = lt = rt = 0;
        lb = rb = false;
    }
    public void SetVars(float h, float y, float trigR, float trigL, bool l, bool r)
    {
        horizontal = h;
        vertical = y;
        lt = trigL;
        rt = trigR;
        lb = l;
        rb = r;
    }
}
public class PlayerMovement : playerOrder
{
    Damage myDamage;

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

    //store ref to spell casting component so it can handle spells
    SpellCasting castingRef;

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



        castingRef = GetComponent<SpellCasting>();
        rigidbody = GetComponent<Rigidbody2D>();
        setInputs(playerNum);

        myDamage = GetComponent<Damage>();
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
        inputs.horizontalAim    += var.ToString();
        inputs.verticalAim      += var.ToString();
        inputs.leftBumper       += var.ToString();
        inputs.rightBumper      += var.ToString();
        inputs.leftTrigger      += var.ToString();
        inputs.rightTrigger     += var.ToString();
    }


    void getInputs()
    {
        if (myDamage.hp > 0 && !myDamage.res)
        { 
            horizontal = Input.GetAxis(inputs.horizontalMove);
            vertical = Input.GetAxis(inputs.verticalMove);

            horAim = Input.GetAxis(inputs.horizontalAim);
            vertAim = Input.GetAxis(inputs.verticalAim);

            lb = Input.GetButtonDown(inputs.leftBumper);
            rb = Input.GetButtonDown(inputs.rightBumper);
            lt = Input.GetAxis(inputs.leftTrigger);
            rt = Input.GetAxis(inputs.rightTrigger);


            castingRef.passedInfo.SetVars(horAim, vertAim, rt, lt, lb, rb);
        }
        else
            castingRef.passedInfo.SetVars(0, 0, 0, 0, false, false);
    }

    void Movement()
    {
        rigidbody.velocity = new Vector2(horizontal, vertical).normalized * moveSpeed;        
    }

    
}
