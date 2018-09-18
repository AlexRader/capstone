using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	private Rigidbody2D myRigidbody;
	public float moveSpeed; //Leave this public unless you need it not to be so I can change it in editor - Your Pal, Josh. Or I guess you could just serialize the field or w/e
    public Vector2 subSpeed;
	// Use this for initialization
	void Start () 
	{
		myRigidbody = GetComponent<Rigidbody2D>();
        subSpeed = Vector2.zero;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//float horizontal = Input.GetAxis("Horizontal"); //Uses Unity Input Manager settings for WASD and Arrows

		//HandleMovement(horizontal);
	}

	void FixedUpdate()
	{
		float horizontal = Input.GetAxis("Horizontal"); //Uses Unity Input Manager settings for WASD and Arrows // Using Fixed Update for this so that movement speed isn't bound by Framerate

		HandleMovement(horizontal);
	}

	private void HandleMovement(float horizontal)//So I could use horizontal inputs for movement, I think this works for joystick too, I will test it with ps4 controller
	{
		
		myRigidbody.velocity = new Vector2 (horizontal * moveSpeed, myRigidbody.velocity.y) + subSpeed; //x = -1 or 1  y = 0 No vertical movement.
	}
}
