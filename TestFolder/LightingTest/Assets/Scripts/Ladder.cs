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

	void OnTriggerStay(Collider other)
	{
        if (other.tag == "Player" && other.name == "TestPlayer" && !other.GetComponent<PlayerMovement>().lockIn)
        {
            if (Input.GetKey(KeyCode.W))
                other.GetComponent<PlayerMovement>().vecZ = climbSpeed;
            else if (Input.GetKey(KeyCode.S))
                other.GetComponent<PlayerMovement>().vecZ = -climbSpeed;
        }
        else if (other.tag == "Player" && other.name == "TestPlayer1" && !other.GetComponent<PlayerMovement1>().lockIn)
        {
            Debug.Log("here");
            if (Input.GetKey(KeyCode.I))
            {
                Debug.Log("now");
                other.GetComponent<PlayerMovement1>().vecZ = climbSpeed;
            }
            else if (Input.GetKey(KeyCode.K))
            {
                other.GetComponent<PlayerMovement1>().vecZ = -climbSpeed;
                Debug.Log("fuck");
            }
        }
        else
        {
            other.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0.2f); //This is (0,0.5) not (0,0) on account of gravity pulling the player down 1, because grav is set to 1. If Grav is changed, set this to half of new value
                                                                                //*Edit* It actually had to be 0.2f on account of it being a float. Why, 0.2 specifically, idk but it balanced out... I probably messed gravity up somewhere.
                                                                                //Your pal, Josh
        }
    }
}
