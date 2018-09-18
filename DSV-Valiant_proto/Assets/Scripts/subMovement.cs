using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class subMovement : MonoBehaviour
{
    float speedX, speedY;
    public float maxSpeed, Accel, decel;
    Vector2 joy1;

    Rigidbody2D rb;

    float angle;
	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        speedX = 0;
        speedY = 0;
    }

    // Update is called once per frame
    void Update ()
    {
        Inputs();
    }

    void Inputs()
    {
        joy1.x = Input.GetAxis("LSHor");
        joy1.y = Input.GetAxis("LSVert");
        //Debug.Log(joy1);
        Debug.Log(rb.velocity);
        if (Mathf.Abs(joy1.x) > .1f)
        {
            if(speedX < maxSpeed && speedX > -maxSpeed)
                speedX -= (joy1.x * Accel * Time.deltaTime);
        }
        else
        {
            if (speedX > decel)
            {
                speedX -= (decel * Time.deltaTime);
            }
            else if (speedX < -decel)
            {
                speedX += (decel * Time.deltaTime);
            }
            if (speedX > -50f && speedX < 50f)
                speedX = 0;
        }
        if (Mathf.Abs(joy1.y) > .1f)
        {
            if (speedY < maxSpeed && speedY > -maxSpeed)
                speedY += (joy1.y * (Accel * .5f) * Time.deltaTime);
        }
        else
        {
            if (speedY > decel)
            {
                speedY -= (decel * Time.deltaTime);
            }
            else if (speedY < -decel)
            {
                speedY += (decel * Time.deltaTime);
            }
            if (speedY > -50f && speedY < 50f)
                speedY = -5f;
        }
        rb.velocity = new Vector2(speedX, speedY) * Time.deltaTime;
    }
}
