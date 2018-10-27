using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovementNetworked : playerOrderNetworked
{
    private PhotonView photonView;
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

    private Vector3 targetVelocity;
    //store ref to spell casting component so it can handle spells
    SpellCasting castingRef;

    public PlayerMovementNetworked()
    {
        inputs = returnDefaultInputs();
    }

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
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
        if (photonView.isMine)
        {
            getInputs();
            Movement();
        }
        else
        {
            SmoothMove();
        }
	}
    private void SmoothMove()
    {
        rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, targetVelocity, Time.deltaTime * 5);
    }
    private void OnSerializeNetworkView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(rigidbody.velocity);
        }
        else
        {
            targetVelocity = (Vector3)stream.ReceiveNext();
        }
    }

    void setInputs(int var)
    {
        inputs.horizontalMove += var.ToString();
        inputs.verticalMove   += var.ToString();
        inputs.horizontalAim  += var.ToString();
        inputs.verticalAim    += var.ToString();
        inputs.leftBumper     += var.ToString();
        inputs.rightBumper    += var.ToString();
        inputs.leftTrigger    += var.ToString();
        inputs.rightTrigger   += var.ToString();
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

        //Vector2 myPos = new Vector2(horizontal, vertical).normalized * moveSpeed * Time.deltaTime;
        //transform.position += new Vector3(myPos.x, myPos.y, 0);
    }

    
}
