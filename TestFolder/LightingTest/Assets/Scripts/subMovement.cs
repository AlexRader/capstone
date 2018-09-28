using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class subMovement : MonoBehaviour
{
    float speedX, speedZ;
    public float maxSpeed, Accel, decel;
    Vector2 joy1;

    Rigidbody rb;

    float angle;
	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        speedX = 0;
        speedZ = 0;
        joy1 = Vector2.zero;
    }

    // Update is called once per frame
    void Update ()
    {
        Inputs();
    }

    void Inputs()
    {

        //joy1.x = Input.GetAxis("LSHor");
        //joy1.y = Input.GetAxis("LSVert");
        //Debug.Log(joy1);
//        Debug.Log(rb.velocity);
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
            if (speedZ < maxSpeed && speedZ > -maxSpeed)
                speedZ += (joy1.y * (Accel * .5f) * Time.deltaTime);
        }
        else
        {
            if (speedZ > decel)
            {
                speedZ -= (decel * Time.deltaTime);
            }
            else if (speedZ < -decel)
            {
                speedZ += (decel * Time.deltaTime);
            }
            if (speedZ > -50f && speedZ < 50f)
                speedZ = -5f;
        }
        rb.velocity = new Vector3(speedX, 0, speedZ) * Time.deltaTime;
    }

    void SetJoy(Vector3 input)
    {
        joy1.x = -input.x;
        joy1.y = input.z;
    }
}
