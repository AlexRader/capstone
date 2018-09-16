using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScr : MonoBehaviour
{
    public float mSpeed = 50;
    Rigidbody rb;
    Camera mainCamera;
    Vector3 velocity;
	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 mousePos = mainCamera.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.transform.position.y));
        transform.LookAt(mousePos + Vector3.up * transform.position.y);

        velocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0,Input.GetAxisRaw("Vertical")).normalized * mSpeed;
	}
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }
}
