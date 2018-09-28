using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchLight : MonoBehaviour {

    public float curYRotation;
	public float minYRotation;
	public float maxYRotation;

	public float increaseAngle;
	public float decreaseAngle;


    public bool playerInRange, playerInRange1;
    //public float zRotation = 10.0F;
   
    // Use this for initialization
    void Start () {

        //transform.eulerAngles = new Vector3(0, 0, curZRotation);
        // transform.rotation = Quaternion.Euler(0, 0, 0);
        playerInRange = false;
        playerInRange1 = false;
    }
	
	// Update is called once per frame
	void Update ()
    {

        lockedRotation();
	//	curZRotation = (curZRotation > 180) ? curZRotation - 360 : curZRotation;


    }

    private void FixedUpdate()
    {
        if (playerInRange == true)
        {
            if (Input.GetKey(KeyCode.Q))
            {
				curYRotation = curYRotation + increaseAngle;
            }

            if (Input.GetKey(KeyCode.E))
            {
				curYRotation = curYRotation + decreaseAngle;
            }
        }
        if (playerInRange1 == true)
        {
            if (Input.GetKey(KeyCode.U))
            {
                curYRotation = curYRotation + increaseAngle;
            }

            if (Input.GetKey(KeyCode.O))
            {
                curYRotation = curYRotation + decreaseAngle;
            }
        }
    }

    void lockedRotation()
    {
		curYRotation = Mathf.Clamp(curYRotation, minYRotation, maxYRotation);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, -curYRotation, transform.localEulerAngles.z);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.name == "TestPlayer")
        {
           // Debug.Log("In LightBox");
            playerInRange = true;
        }
        else if (collision.tag == "Player" && collision.name == "TestPlayer1")
        {
            // Debug.Log("In LightBox");
            playerInRange1 = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Player" && collision.name == "TestPlayer")
        {
            //Debug.Log("Exiting LightBox");
            playerInRange = false;
        }
        if (collision.tag == "Player" && collision.name == "TestPlayer1")
        {
            //Debug.Log("Exiting LightBox");
            playerInRange1 = false;
        }
    }

}
