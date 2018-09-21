using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float movespeed = 6;

    Rigidbody myRB;
    Camera viewCam;
    Vector3 velocity;
	// Use this for initialization
	void Start ()
    {
        myRB = GetComponent<Rigidbody>();
        viewCam = Camera.main;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 mousePos = viewCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, viewCam.transform.position.y));

        transform.LookAt(mousePos + Vector3.up * transform.position.y);
        velocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized * movespeed;
	}

    void FixedUpdate()
    {
        myRB.MovePosition(myRB.position + velocity * Time.fixedDeltaTime);
    }
}
