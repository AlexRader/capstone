using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchLight : MonoBehaviour {

    public float curZRotation;
	public float minZRotation;
	public float maxZRotation;

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
                Debug.Log("asdasdasdox");

                curZRotation = curZRotation + increaseAngle;
            }

            if (Input.GetKey(KeyCode.E))
            {
				curZRotation = curZRotation + decreaseAngle;
            }
        }
        if (playerInRange1 == true)
        {
            if (Input.GetKey(KeyCode.U))
            {
                curZRotation = curZRotation + increaseAngle;
            }

            if (Input.GetKey(KeyCode.O))
            {
                curZRotation = curZRotation + decreaseAngle;
            }
        }
    }

    void lockedRotation()
    {
		curZRotation = Mathf.Clamp(curZRotation, minZRotation, maxZRotation);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, curZRotation);
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.tag == "Player" && collision.name == "TestPlayer")
        {
            Debug.Log("In LightBox");
            playerInRange = true;
        }
        else if (collision.tag == "Player" && collision.name == "TestPlayer1")
        {
            Debug.Log("In LightBox");
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
