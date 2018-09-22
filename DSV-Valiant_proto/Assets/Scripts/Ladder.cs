using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour {

	public float climbSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "Player" && Input.GetKey (KeyCode.W)) {
			//float vertical = Input.GetAxis ("Vertical");
			other.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, climbSpeed);
		} else if (other.tag == "Player" && Input.GetKey (KeyCode.S)) {
			other.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, -climbSpeed);
		} else 
		{
			other.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0,0.2f); //This is (0,0.5) not (0,0) on account of gravity pulling the player down 1, because grav is set to 1. If Grav is changed, set this to half of new value
			//*Edit* It actually had to be 0.2f on account of it being a float. Why, 0.2 specifically, idk but it balanced out... I probably messed gravity up somewhere.
			//Your pal, Josh
		}
	}
}
