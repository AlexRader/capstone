using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : playerOrder
{
    public GameObject   aimObj;
    public GameObject   shot,
                        barrier,
                        circle,
                        lob;
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
    public float        rtTimer,
                        ltTimer,
                        rbTimer,
                        lbTimer,
                        rtTimerMax,
                        ltTimerMax,
                        rbTimerMax,
                        lbTimerMax;
    Vector3             direction;

    int i;
    bool circleCall;
    public bool lobCall;
    GameObject[] circleArr;
    public PlayerMovement()
    {
        inputs = returnDefaultInputs();
    }


    // Use this for initialization
    void Start ()
    {
        Physics2D.IgnoreLayerCollision(8, 10);
        Physics2D.IgnoreLayerCollision(8, 12);


        rigidbody = GetComponent<Rigidbody2D>();
        setInputs(playerNum);
        setTimers();
        i = 0;
        circleCall = false;
        lobCall = false;
        circleArr = new GameObject[3];
	}
	
	// Update is called once per frame
	void Update ()
    {
        getInputs();
        Movement();
        CallAttacks();
	}

    void setInputs(int var)
    {
        Debug.Log(GetComponent<Damage>().res);
        inputs.horizontalMove += var.ToString();
        inputs.verticalMove += var.ToString();
        inputs.horizontalAim += var.ToString();
        inputs.verticalAim += var.ToString();
        inputs.leftBumper += var.ToString();
        inputs.rightBumper += var.ToString();
        inputs.leftTrigger += var.ToString();
        inputs.rightTrigger += var.ToString();
    }

    void setTimers()
    {
        rtTimer = ltTimer = rbTimer = lbTimer = 0;
    }

    void getInputs()
    {
        if (!GetComponent<Damage>().res)
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
    }

    void Movement()
    {
        if (!circleCall)
            rigidbody.velocity = new Vector2(horizontal, vertical).normalized * moveSpeed;
        else
            rigidbody.velocity = Vector2.zero;
        aimObj.transform.position = (new Vector3(horAim, vertAim, 0).normalized * 2) + transform.position;
        
    }

    void CallAttacks()
    {
        GameObject temp;
        if (rt <= -.9f && rtTimer >= rtTimerMax)
        {
            direction = aimObj.transform.position - transform.position;
            temp = Instantiate(shot, aimObj.transform.position, Quaternion.LookRotation(direction));
            temp.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
            temp.GetComponent<Rigidbody2D>().velocity = direction * 5;
            temp.SendMessage("vSet", (Vector2)(direction * 5));
            rtTimer = 0;
        }
        else if (lt >= .9f && ltTimer >= ltTimerMax && !lobCall)
        {
            direction = aimObj.transform.position - transform.position;
            temp = Instantiate(lob, aimObj.transform.position, Quaternion.LookRotation(direction));
            temp.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
            temp.GetComponent<Rigidbody2D>().velocity = direction * 5;
            temp.SendMessage("vSet", (Vector2)(direction * 5));
            ltTimer = 0;
            lobCall = true;
        }
        else if (rb && rbTimer >= rbTimerMax)
        {
            direction = aimObj.transform.position - transform.position;
            temp = Instantiate(barrier, transform.position + direction *.3f, Quaternion.LookRotation(direction));
            temp.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
            rbTimer = 0;
        }
        else if (lb && lbTimer >= lbTimerMax && !circleCall)
        {
            lbTimer = 0;
            circleCall = true;
            StartCoroutine("CircleSpawner");
        }
        rtTimer += Time.deltaTime;
        ltTimer += Time.deltaTime;
        rbTimer += Time.deltaTime;
        lbTimer += Time.deltaTime;
    }

    IEnumerator CircleSpawner()
    {
        GameObject temp;
        yield return new WaitForSeconds(1.0f);
        if (i == 3)
        {
            Debug.Log("Explode");
            foreach (GameObject obj in circleArr)
                Destroy(obj);
            temp = Instantiate(circle, transform.position, Quaternion.identity);
            temp.transform.localScale += new Vector3((i - 1) * .5f, (i - 1) * .5f, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = -i - 1;
            temp.GetComponent<SpriteRenderer>().color = Color.black;
            temp.SendMessage("myDamage", (i + 2) * (i + 2));

            temp.SendMessage("parentObj", gameObject);

            circleArr[0] = temp;
            ++i;
            StartCoroutine("CircleSpawner");
        }
        else if (i == 4)
        {
            Destroy(circleArr[0]);
            circleCall = false;
            i = 0;
        }
        else
        {
            temp = Instantiate(circle, transform.position, Quaternion.identity);
            temp.transform.localScale += new Vector3(i * .5f, i * .5f, 0);
            if (i == 0)
                temp.GetComponent<SpriteRenderer>().color = Color.red;
            else if (i == 1)
                temp.GetComponent<SpriteRenderer>().color = Color.yellow;
            else
                temp.GetComponent<SpriteRenderer>().color = Color.green;
            temp.GetComponent<SpriteRenderer>().sortingOrder = -i - 1;
            temp.SendMessage("myDamage", (i + 1) * (i + 1));
            temp.SendMessage("parentObj", gameObject);

            circleArr[i] = temp;
            ++i;
            StartCoroutine("CircleSpawner");
        }
    }
}
